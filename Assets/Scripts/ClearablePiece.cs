using UnityEngine;
using System.Collections;

public class ClearablePiece : MonoBehaviour
{
    //animation var (we need to used them later)
    public AnimationClip clearAnimation;

    private bool isBeingCleared = false;

    public bool IsBeingCleared { get { return isBeingCleared; } }

    //piece acsses
    protected GamePiece piece;

    void Awake()
    {
        piece = GetComponent<GamePiece>(); 
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Clear()
    {
        isBeingCleared = true;
        StartCoroutine(ClearCoroutine());
    }


    //this func(coroutine type) plays a "clear" animation on the object,
    //waits for the animation to finish, and then destroys the object.
    //Using IEnumerator and yield allows the function to pause during the animation instead of executing everything immediately.

    private IEnumerator ClearCoroutine()
    {
        Animator animator = GetComponent<Animator>();

        if (animator)
        {
            animator.Play(clearAnimation.name);

            yield return new WaitForSeconds(clearAnimation.length);
            Destroy(gameObject);
        }
    }
}
