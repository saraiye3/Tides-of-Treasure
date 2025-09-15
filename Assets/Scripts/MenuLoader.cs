using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuLoader : MonoBehaviour
{
    public void LoadMenu()
    {
        Debug.Log("UnlockedLevels = " + LevelProgress.GetUnlockedLevels());

        SceneManager.LoadScene("menuScene");
    }
}
