using System.Runtime.CompilerServices;
using UnityEngine;

public class LevelTimer : Level
{
    public int timeInSeconds;
    public int targetScore;

    private float timer;
    private bool timeOut = false;

    private void Start()
    {
        type = LevelType.TIMER;

        Debug.Log("Time: " + timeInSeconds + " seconds. Target score: " +  targetScore);
    }

    private void Update()
    {
        if(!timeOut)
        {
            timer += Time.deltaTime;

            if (timeInSeconds - timer <= 0)
            {
                if (currentScore >= targetScore)
                    GameWin();
                else
                    GameLose();

                timeOut = true;
            }
        }
    }
}
