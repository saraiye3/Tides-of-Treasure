using UnityEngine;
using UnityEngine.SceneManagement;

public class StageButton : MonoBehaviour
{
    public RectTransform waypoint;        // destination (1/2/3)
    public BoatUIMovement boatMover;
    public bool isFinalStage = false;     // then the boat move automaticliy - only for level 3
    public string sceneToLoad; //conection to level scene

    bool clicked = false;

    public void OnStageClicked()
    {
        if (clicked) return;
        if (boatMover == null || waypoint == null)
        {
            Debug.LogError("[StageButton] missing refs");
            return;
        }
        clicked = true;

        if (!isFinalStage)
        {
            boatMover.MoveToWaypoint(waypoint, () =>
            {
                LoadSceneIfSet();
            });
        }
        else
        {
            boatMover.MoveToWaypoint(waypoint, () =>
            {
                boatMover.StartCoroutine(SailToTreasureThenLoad());
            });
        }
    }

    System.Collections.IEnumerator SailToTreasureThenLoad()
    {
        yield return new WaitForSeconds(0.25f);

        boatMover.MoveToWaypoint(boatMover.treasureWaypoint, () =>
        {
            boatMover.StartCoroutine(LoadAfterDelay(0.2f));
        });
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
