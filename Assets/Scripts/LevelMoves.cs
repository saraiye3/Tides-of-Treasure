using UnityEngine;

public class LevelMoves : Level
{
    public int numMoves;
    public int targetScore;

    private int movesUsed = 0;
    private bool hammerUsed = false;

    public bool HammerUsed
    {
        get { return hammerUsed; }
        set { hammerUsed = value; }
    }

    private void Start()
    {
        type = LevelType.MOVES;
        hud.SetLevelType(type);
        hud.SetScore(currentScore);
        hud.SetTarget(targetScore);
        hud.SetRemaining(numMoves);
    }

    private void Update()
    {
        
    }

    public override void OnMove()
    {
        movesUsed++;
        hud.SetRemaining(Mathf.Max(numMoves - movesUsed, 0));
        if (numMoves - movesUsed <= 0) //If no moves left
        {
            if (currentScore >= targetScore)
                GameWin();
            else
                GameLose();
        }
    }
}
