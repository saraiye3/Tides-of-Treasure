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
    public TMP_Text shuffleText;

    // Bomb Booster UI Elements
    [Header("Bomb Booster UI")]
    public UnityEngine.UI.Button bombBoosterButton;
    public TMP_Text bombBoosterCountText;
    public GameObject bombBoosterPanel;

    private int starIdx = 0;

    private void Start()
    {
        // Setup bomb booster button
        if (bombBoosterButton != null)
        {
            bombBoosterButton.onClick.AddListener(OnBombBoosterClicked);
        }

        // Update booster display
        UpdateBombBoosterDisplay(BombBoosterManager.GetBombBoosterCount());

        // Original star setup
        for (int i = 0; i < stars.Length; i++)
        {
            if (i == starIdx)
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

    public void SetTarget(int target)
    {
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
            remainingSubtext.text = "moves";
            targetSubtext.text = "target score";
        }
        else if (type == Level.LevelType.OBSTACLE)
        {
            remainingSubtext.text = "moves";
            targetSubtext.text = "keys remaining";
        }
        else if (type == Level.LevelType.TIMER)
        {
            remainingSubtext.text = "timer";
            targetSubtext.text = "target score";
        }
    }

    public void OnGameWin(int score)
    {
        gameOver.ShowWin(score, starIdx);
    }

    public void OnGameLose()
    {
        if (level is LevelMoves movesLevel && !movesLevel.HammerUsed)
        {
            hammer.ShowHammer();
            movesLevel.HammerUsed = true;
        }
        else
            gameOver.ShowLose();
    }

    // Bomb Booster Functions
    public void UpdateBombBoosterDisplay(int count)
    {
        if (bombBoosterCountText != null)
        {
            bombBoosterCountText.text = count.ToString();
        }

        if (bombBoosterButton != null)
        {
            bombBoosterButton.interactable = (count > 0);

            var colors = bombBoosterButton.colors;
            colors.normalColor = (count > 0) ? Color.white : Color.gray;
            bombBoosterButton.colors = colors;
        }

        // Show/hide panel if available
        if (bombBoosterPanel != null)
        {
            bombBoosterPanel.SetActive(true);
        }
    }

    public void OnBombBoosterClicked()
    {
        Debug.Log("Bomb Booster button clicked!");

        // Check if boosters are available
        if (!BombBoosterManager.HasBombBoosters())
        {
            Debug.Log("No bomb boosters available!");
            return;
        }

        // Try to use booster
        if (BombBoosterManager.UseBombBooster())
        {
            // Activate bomb mode in Grid
            if (level != null && level.grid != null)
            {
                level.grid.EnableBombBoosterMode();
            }
            else
            {
                Debug.LogError("Grid reference missing in HUD!");
            }

            // Update display
            UpdateBombBoosterDisplay(BombBoosterManager.GetBombBoosterCount());
        }
    }

    // Called when player earns a new bomb booster
    public void OnBombBoosterEarned()
    {
        Debug.Log("Bomb Booster Earned!");
        UpdateBombBoosterDisplay(BombBoosterManager.GetBombBoosterCount());

    }

    public void OnReturnClicked()
    {
        SoundManager.instance.musicSource.Stop();
        SceneManager.LoadScene("menuScene");

    }

    public void ShowShuffleMessage()
    {
        shuffleText.gameObject.SetActive(true);
        Debug.Log("no possible moves");
        StartCoroutine(HideShuffleMessage());
    }

    public IEnumerator HideShuffleMessage()
    {
        yield return new WaitForSeconds(1.5f);
        shuffleText.gameObject.SetActive(false);
    }
}