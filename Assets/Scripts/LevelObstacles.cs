using UnityEngine;

public class LevelObstacles : Level
{
    public int numMoves;
    public Grid.PieceType[] obstacleTypes;

    private int movesUsed = 0;
    private int numOfObstaclesLeft;

    private void Start()
    {
        type = LevelType.OBSTACLE;

        for (int i = 0; i < obstacleTypes.Length; i++)
        {
            numOfObstaclesLeft += grid.GetPiecesOfType(obstacleTypes[i]).Count;
        }
        hud.SetLevelType(type);
        hud.SetScore(currentScore);
        hud.SetTarget(numOfObstaclesLeft);
        hud.SetRemaining(numMoves);
    }

    private void Update()
    {
        
    }

    public override void OnMove()
    {
        movesUsed++;
        hud.SetRemaining(numMoves - movesUsed);

        if(numMoves - movesUsed == 0 && numOfObstaclesLeft > 0)
            GameLose();
    }

    public override void OnPieceCleared(GamePiece piece)
    {
        //base.OnPieceCleared(piece);

        for(int i = 0;i < obstacleTypes.Length;i++)
        {
            if (obstacleTypes[i] == piece.Type)
            {
                numOfObstaclesLeft--;
                hud.SetTarget(numOfObstaclesLeft);

                if (numOfObstaclesLeft == 0)
                {
                    currentScore += 1000 * (numMoves - movesUsed); //1000 pts for each move left
                    hud.SetScore(currentScore);
                    GameWin();
                }
            }
        }
    }
}
