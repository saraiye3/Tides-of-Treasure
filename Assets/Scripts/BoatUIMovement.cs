using UnityEngine;
using System.Collections;
using System;

public class BoatUIMovement : MonoBehaviour
{
    [Header("Refs")]
    public RectTransform boat;                 
    public RectTransform treasureWaypoint;     

    [Header("Motion")]
    public float moveDuration = 1.2f;// boat movment time (from a to b )
    public AnimationCurve ease = AnimationCurve.EaseInOut(0, 0, 1, 1); // boat rate

    bool isMoving = false;

    
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
        MoveToWaypoint(treasureWaypoint, null);
    }

    IEnumerator MoveRoutine(Vector2 target, Action onArrive)
    {
        if (isMoving) yield break;
        isMoving = true;

        Vector2 start = boat.anchoredPosition;
        float t = 0f;

        // boat rotation
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
}
