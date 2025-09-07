using UnityEngine;
using System.Collections;

public class Level : MonoBehaviour 
{
    public enum LevelType
    {
        TIMER,
        OBSTACLE,
        MOVES
    };

    public Grid grid;
    public HUD hud;
    public int score1Star;
    public int score2Star;
    public int score3Star;

    protected LevelType type;
    protected int currentScore;

    public LevelType Type { get { return type; } }


    protected bool didWin;

    private void Start()
    {
        hud.SetScore(currentScore);
        
    }

    private void Update()
    {
        
    }

    public virtual void GameWin()
    {
        //Debug.Log("You win!");
        //hud.OnGameWin(currentScore);
        grid.GameOver();
        didWin = true;
        StartCoroutine(WaitForGridFill());
    }

    public virtual void GameLose()
    {
        //Debug.Log("You lose.");
        //hud.OnGameLose();
        grid.GameOver();
        didWin = false;
        StartCoroutine(WaitForGridFill());

    }

    public virtual void OnMove()
    {
        
    }

    public virtual void OnPieceCleared(GamePiece piece)
    {
        currentScore += piece.score;
        Debug.Log("Score: " + currentScore);
        hud.SetScore(currentScore);
    }

    protected virtual IEnumerator WaitForGridFill()
    {
        while (grid.IsFilling)
        {
            yield return 0;

        }
        if (didWin)
        {
            hud.OnGameWin(currentScore);
        }
        else
        {
            hud.OnGameLose();
        }
    }
}
