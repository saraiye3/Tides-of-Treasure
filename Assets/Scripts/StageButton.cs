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

        if (!LevelProgress.IsLevelUnlocked(stageIndex)) return;

        // boat movement roles
        if (stageIndex != boatMover.CurrentStageIndex + 1) return;

        clicked = true;

        if (!isFinalStage)
        {
            boatMover.MoveToStage(stageIndex, LoadSceneIfSet);
        }
        else
        {
            boatMover.MoveToStage(stageIndex, () =>
            {
                // final stage -> move to tresure
                boatMover.MoveToTreasure();
                StartCoroutine(LoadAfterDelay(0.2f));
            });
        }
    }

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
