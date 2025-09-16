using UnityEngine;
using UnityEngine.SceneManagement;

public class StageButton : MonoBehaviour
{
    public int stageIndex = 1;                 // 1,2,3...
    public BoatUIMovement boatMover;
    public bool isFinalStage = false;          
    public string sceneToLoad;                 

    bool clicked = false;
    public void OnStageClicked()
    {
        if (clicked) return;
        if (boatMover == null) { Debug.LogError("[StageButton] boatMover missing"); return; }

        // אם ה-Boat עובד 0-based, החליפי ל: int current = boatMover.CurrentStageIndex + 1;
        int current = boatMover.CurrentStageIndex;
        bool isSameLevel = (stageIndex == current);

        Debug.Log($"[StageButton] clicked stage={stageIndex}, boat at={current}, unlocked={LevelProgress.IsLevelUnlocked(stageIndex)}");

        if (!LevelProgress.IsLevelUnlocked(stageIndex) && !isSameLevel)
            return;

        clicked = true;

        if (isSameLevel)
        {
            PlayerPrefs.SetInt("LastPlayedStage", stageIndex);
            PlayerPrefs.Save();
            LoadSceneIfSet();
            return;
        }

        boatMover.MoveToStage(stageIndex, () =>
        {
            PlayerPrefs.SetInt("LastPlayedStage", stageIndex);
            PlayerPrefs.Save();
            LoadSceneIfSet();
        });
    }


    void OnEnable() { clicked = false; } // שלא ייתקע בין טעינות





    System.Collections.IEnumerator LoadAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        LoadSceneIfSet();
    }

    void LoadSceneIfSet()
    {
        if (!string.IsNullOrEmpty(sceneToLoad))
            SceneManager.LoadScene(sceneToLoad);
    }
}
