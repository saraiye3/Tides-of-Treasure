using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuLoader : MonoBehaviour
{
    private const string TreasureKey = "TreasureUnlocked";

    public void LoadMenu()
    {
        SceneManager.LoadScene("menuScene");
    }

    public void LoadMenuAfterWin()
    {
        PlayerPrefs.SetInt(TreasureKey, 1); 
        PlayerPrefs.Save();
        SceneManager.LoadScene("menuScene");
    }
}
