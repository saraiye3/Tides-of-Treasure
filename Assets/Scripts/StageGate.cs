using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class StageGate : MonoBehaviour
{
    [Header("Config")]
    public int stageIndex = 1; // 1,2,3...

    Button btn;

    void Awake() { btn = GetComponent<Button>(); }

    void OnEnable()
    {
        bool unlocked = LevelProgress.IsLevelUnlocked(stageIndex);
        if (btn) btn.interactable = unlocked;  // locked ? cant push the button
    }
}
