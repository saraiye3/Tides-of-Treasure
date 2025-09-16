using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class StageGate : MonoBehaviour
{
    Button btn;
    StageButton sb;

    void Awake()
    {
        btn = GetComponent<Button>();
        sb = GetComponent<StageButton>();
    }

    void OnEnable()
    {
        // אם אין StageButton על אותו אובייקט – ננעל ליתר ביטחון
        int idx = (sb != null) ? sb.stageIndex : 9999;
        bool unlocked = LevelProgress.IsLevelUnlocked(idx);
        if (btn) btn.interactable = unlocked;

        Debug.Log($"[StageGate] stageIndex={idx}, unlocked={unlocked}");
    }
}
