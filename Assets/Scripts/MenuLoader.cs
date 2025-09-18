using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuLoader : MonoBehaviour
{
    private const string TreasureKey = "TreasureUnlocked";

    // ניווט רגיל לתפריט – בלי לפתוח את אפשרות האוצר
    public void LoadMenu()
    {
        SceneManager.LoadScene("menuScene");
    }

    // לקרוא רק מכפתור ה-Done (אחרי ניצחון)
    public void LoadMenuAfterWin()
    {
        PlayerPrefs.SetInt(TreasureKey, 1); // מסמן שסיימנו שלב ומותר לשוט לאוצר
        PlayerPrefs.Save();
        SceneManager.LoadScene("menuScene");
    }
}
