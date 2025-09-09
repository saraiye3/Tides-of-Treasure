using UnityEngine;
using UnityEngine.SceneManagement;

public class StageButton : MonoBehaviour
{
    public int stageIndex = 1;                 // 1,2,3...
    public BoatUIMovement boatMover;
    public bool isFinalStage = false;          // לשלב האחרון
    public string sceneToLoad;                 // שם סצנת הלבל

    bool clicked = false;

    public void OnStageClicked()
    {
        if (clicked) return;
        if (boatMover == null) { Debug.LogError("[StageButton] boatMover missing"); return; }

        // 1) השלב חייב להיות פתוח לפי ההתקדמות
        if (!LevelProgress.IsLevelUnlocked(stageIndex)) return;

        // 2) מותר לשוט *רק* לשלב הבא ברצף (מהמיקום הנוכחי של הסירה)
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
                // הגעה לשלב האחרון → הפלגה לאוצר ואז טעינה
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
