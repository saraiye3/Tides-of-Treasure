using UnityEngine;

public class LevelMoves : Level
{
    public int numMoves;
    public int targetScore;

    private int movesUsed = 0;

    private void Start()
    {
        type = LevelType.MOVES;

        Debug.Log("Number of moves: " + numMoves + "Target score:" + targetScore);
    }

    private void Update()
    {
        
    }

    public override void OnMove()
    {
        movesUsed++;
        Debug.Log("Moves remaining:" + (numMoves - movesUsed));

        if (numMoves - movesUsed == 0) //If no moves left
        {
            if (currentScore >= targetScore)
                GameWin();
            else
                GameLose();
        }
            
        

    }


}
