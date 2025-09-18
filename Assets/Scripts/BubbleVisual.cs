using UnityEngine;

public class BubbleVisual : MonoBehaviour
{
    // גרפי הבועה לשלושת השלבים
    [Header("Bubble Sprites")]
    public Sprite normalSprite;    // בועה שלמה – מצב התחלתי
    public Sprite cracked1Sprite;  // אחרי פגיעה אחת
    public Sprite cracked2Sprite;  // אחרי שתי פגיעות

    private SpriteRenderer sr;

    private void Awake()
    {
        // לוקחים את ה-SpriteRenderer שעל אותו prefab
        sr = GetComponent<SpriteRenderer>();

        // מוודאים שבהתחלה מוצגת הבועה הרגילה
        if (normalSprite != null)
            sr.sprite = normalSprite;
    }

    /// <summary>
    /// מחליף תמונה בהתאם למספר הפגיעות
    /// </summary>
    public void UpdateVisual(int hits)
    {
        if (hits == 1 && cracked1Sprite != null)
        {
            sr.sprite = cracked1Sprite;   // אחרי פגיעה אחת
        }
        else if (hits == 2 && cracked2Sprite != null)
        {
            sr.sprite = cracked2Sprite;   // אחרי שתי פגיעות
        }
        // אם hits >=3 – הקוד ב-Grid כבר ינקה את הבועה לגמרי
    }
}
