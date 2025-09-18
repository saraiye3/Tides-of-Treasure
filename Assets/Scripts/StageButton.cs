using UnityEngine;
using UnityEngine.SceneManagement;

public class StageButton : MonoBehaviour
{
    public int stageIndex = 1;                 // 1,2,3...
    public BoatUIMovement boatMover;
    public bool isFinalStage = false;          
    public string sceneToLoad;                 

    bool clicked = false;
    public bool requireFinalStageToSail = true;

    public bool requireDoneToSail = true;

    public void OnStageClicked()
    {
        if (clicked) return;
        if (boatMover == null) { Debug.LogError("[StageButton] boatMover missing"); return; }

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
    public void OnTreasureButtonClicked()
    {
        if (boatMover == null) { Debug.LogError("[StageButton] boatMover missing"); return; }

        if (!AllowTreasure())
        {
            Debug.Log("[StageButton] Treasure blocked (need Done or final stage).");
            return;
        }

        boatMover.MoveToTreasure();

        PlayerPrefs.DeleteKey("TreasureUnlocked");
        PlayerPrefs.Save();
    }

    bool AllowTreasure()
    {
        bool allow = true;

        if (requireDoneToSail)
            allow &= PlayerPrefs.GetInt("TreasureUnlocked", 0) == 1;

        if (requireFinalStageToSail && boatMover != null)
            allow &= boatMover.CurrentStageIndex >= boatMover.FinalStageIndex;

        return allow;
    }

}
