using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuLoader : MonoBehaviour
{
    public void LoadMenu()
    {
        SceneManager.LoadScene("menuScene");
    }
}
