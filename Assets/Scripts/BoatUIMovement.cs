using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Events;

public class BoatUIMovement : MonoBehaviour
{
    [Header("Refs")]
    public RectTransform boat;
    public RectTransform treasureWaypoint;

    [SerializeField] private int finalStageIndex = 3; // 1-based: אחרי שלב 3 CurrentStageIndex == 3
    [SerializeField] private bool autoSailToTreasureWhenAtFinal = true;


    [Header("Path (ordered)")]
    // NEW: waypoints per stage in order (index 0 = Stage 1, index 1 = Stage 2, ...)
    public RectTransform[] stageWaypoints;

    [Header("Motion")]
    public float moveDuration = 1.2f; // boat movment time (from a to b )  <-- kept your comment
    public AnimationCurve ease = AnimationCurve.EaseInOut(0, 0, 1, 1); // boat rate  <-- kept your comment

    public UnityEvent onTreasureArrived;

    // NEW: current stage the boat is aligned to on the map:
    // 0 = before Stage 1, 1..N = at stage N waypoint
    public int CurrentStageIndex { get; private set; } = 0;


    bool isMoving = false;

    Vector2 defaultPos; // for first-time fallback

    void Awake()
    {
        if (boat == null) boat = GetComponent<RectTransform>();
        defaultPos = boat != null ? boat.anchoredPosition : Vector2.zero;
    }

    void Start()
    {
        if (BoatState.TryLoad(out var pos, out var stage))
        {
            CurrentStageIndex = Mathf.Clamp(stage, 0, stageWaypoints != null ? stageWaypoints.Length : 0);
            if (boat != null) boat.anchoredPosition = pos;
        }
        else
        {
            CurrentStageIndex = 0;
            if (boat != null) boat.anchoredPosition = defaultPos;
        }

        // NEW: אם כבר בשלב הסופי – לשוט אוטומטית לתיבה
        if (autoSailToTreasureWhenAtFinal && CurrentStageIndex >= finalStageIndex && treasureWaypoint != null)
        {
            StartCoroutine(AutoSailToTreasureNextFrame());
        }
    }

    private IEnumerator AutoSailToTreasureNextFrame()
    {
        yield return null;
        MoveToTreasure();
    }

    // -----------------------------------------------------------------------
    // STRICT stage movement API (use this for level buttons)
    // Moves ONLY from N to N+1. Ignores any non-consecutive or invalid requests.
    // -----------------------------------------------------------------------
    public void MoveToStage(int targetStageIndex, Action onArrive = null)
    {
        if (stageWaypoints == null || stageWaypoints.Length == 0) { Debug.LogError("[BoatUIMovement] stageWaypoints not set"); return; }
        if (targetStageIndex < 1 || targetStageIndex > stageWaypoints.Length) return;
        if (targetStageIndex == CurrentStageIndex) { onArrive?.Invoke(); return; }

        var wp = stageWaypoints[targetStageIndex - 1];
        if (wp == null) { Debug.LogError("[BoatUIMovement] waypoint is NULL for stage " + targetStageIndex); return; }

        StartCoroutine(MoveThenCommit(wp.anchoredPosition, targetStageIndex, onArrive));
    }


    IEnumerator MoveThenCommit(Vector2 target, int newStageIndex, Action onArrive)
    {
        yield return MoveRoutine(target, null);
        CurrentStageIndex = newStageIndex;
        if (boat != null) BoatState.Save(boat.anchoredPosition, CurrentStageIndex);
        onArrive?.Invoke();
    }

    // -----------------------------------------------------------------------
    // Free-form movement API (keep using this for cinematic moves like treasure)
    // NOTE: This DOES NOT enforce stage order and does NOT change CurrentStageIndex.
    // -----------------------------------------------------------------------
    public void MoveToWaypoint(RectTransform waypoint, Action onArrive = null)
    {
        if (boat == null) { Debug.LogError("[BoatUIMovement] boat is NULL"); return; }
        if (waypoint == null) { Debug.LogError("[BoatUIMovement] waypoint is NULL"); return; }
        if (!gameObject.activeInHierarchy) { Debug.LogError("[BoatUIMovement] holder not active"); return; }

        StartCoroutine(MoveRoutine(waypoint.anchoredPosition, onArrive));
    }

    public void MoveToTreasure()
    {
        if (treasureWaypoint == null) { Debug.LogError("[BoatUIMovement] treasureWaypoint is NULL"); return; }
        MoveToWaypoint(treasureWaypoint, () =>
        {
            // Optional: persist boat position after cinematic move
            if (boat != null) BoatState.Save(boat.anchoredPosition, CurrentStageIndex);
            onTreasureArrived?.Invoke();
        });
        

    }

    IEnumerator MoveRoutine(Vector2 target, Action onArrive)
    {
        if (isMoving) yield break;
        isMoving = true;

        Vector2 start = boat.anchoredPosition;
        float t = 0f;

        // boat rotation  <-- kept your comment
        float dir = Mathf.Sign(target.x - start.x);
        var s = boat.localScale;
        boat.localScale = new Vector3(Mathf.Abs(s.x) * (dir < 0 ? -1 : 1), s.y, s.z);

        while (t < 1f)
        {
            t += Time.deltaTime / moveDuration;
            float k = ease.Evaluate(Mathf.Clamp01(t));
            boat.anchoredPosition = Vector2.LerpUnclamped(start, target, k);
            yield return null;
        }

        boat.anchoredPosition = target;
        isMoving = false;

        onArrive?.Invoke();

    }
    public void ResetBoatToStart(bool save = true)
    {
        CurrentStageIndex = 0;
        if (boat != null) boat.anchoredPosition = defaultPos;
        if (save && boat != null) BoatState.Save(boat.anchoredPosition, CurrentStageIndex);
    }

    public void ReloadBoatFromSavedState()
    {
        if (BoatState.TryLoad(out var pos, out var stage))
        {
            CurrentStageIndex = Mathf.Clamp(stage, 0, stageWaypoints != null ? stageWaypoints.Length : 0);
            if (boat != null) boat.anchoredPosition = pos;
        }
        else
        {
            CurrentStageIndex = 0;
            if (boat != null) boat.anchoredPosition = defaultPos;
        }
    }
}
