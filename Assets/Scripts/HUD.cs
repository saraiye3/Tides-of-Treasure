using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HUD : MonoBehaviour
{
    public Hammer hammer;
    public Level level;
    public GameOver gameOver;
    public TMP_Text remainingText;
    public TMP_Text remainingSubtext;
    public TMP_Text targetText;
    public TMP_Text targetSubtext;
    public TMP_Text scoreText;
    public UnityEngine.UI.Image[] stars;

    private int starIdx = 0;

    private void Start()
    {
        for (int i = 0; i < stars.Length; i++)
        {
            if ( i== starIdx)
            {
                stars[i].enabled = true;

            }
            else
            {
                stars[i].enabled = false;
            }
        }
    }

    private void Update()
    {
        
    }

    public void SetScore(int score)
    {
        scoreText.text = score.ToString();

        int visibleStar = 0;

        if (score >= level.score1Star && score < level.score2Star)
        {
            visibleStar = 1;
        }
        else if (score >= level.score2Star && score < level.score3Star)
        {
            visibleStar = 2;
        }
        else if (score >= level.score3Star)
        {
            visibleStar = 3;
        }

        for (int i = 0; i < stars.Length; i++)
        {
            if (i == visibleStar)
            {
                stars[i].enabled = true;
            }
            else
            {
                stars[i].enabled = false;
            }

        }

        starIdx = visibleStar;
    }

    public void SetTarget(int target) {
        targetText.text = target.ToString();
    }

    public void SetRemaining(int remaining)
    {
        remainingText.text = remaining.ToString();
    }

    public void SetRemaining(string remaining)
    {
        remainingText.text = remaining;
    }

    public void SetLevelType(Level.LevelType type)
    {
        if (type == Level.LevelType.MOVES)
        {
            remainingSubtext.text = "moves remaining";
            targetSubtext.text = "target score";
        }
        else if (type == Level.LevelType.OBSTACLE)
        {
            remainingSubtext.text = "moves remaining";
            targetSubtext.text = "keys remaining";
        }
        else if (type == Level.LevelType.TIMER)
        {
            remainingSubtext.text = "time remaining";
            targetSubtext.text = "target score";
        }
    }

    public void OnGameWin(int score)
    {
        gameOver.ShowWin(score, starIdx);
    }

    public void OnGameLose()
    {
        if(level is LevelMoves movesLevel && !movesLevel.HammerUsed)
        {
            hammer.ShowHammer();
            movesLevel.HammerUsed = true;
        }
        else
            gameOver.ShowLose();
    }

    public void OnReturnClicked()
    {
        SceneManager.LoadScene("menuScene");
    }
}
