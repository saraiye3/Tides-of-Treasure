using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class GameOver : MonoBehaviour
{
    public GameObject screenParent;
    public GameObject scoreParent;
    public TMP_Text loseText;
    public TMP_Text scoreText;
    public UnityEngine.UI.Image[] stars;
    public Animator animator;

    public int currentStageIndex = 1;

    private void Start()
    {
        screenParent.SetActive(false);

        for (int i = 0; i < stars.Length; i++)
        {
            stars[i].enabled = false;
        }
    }

    public void ShowLose()
    {
        screenParent.SetActive(true);
        scoreParent.SetActive(false);

        if (animator)
        {
            animator.Play("GameOverShow");
        }
    }

    public void ShowWin(int score, int starCount)
    {
        screenParent.SetActive(true);
        loseText.enabled = false;

        scoreText.text = score.ToString();
        scoreText.enabled = false;

        if (animator)
        {
            animator.Play("GameOverShow");
        }

        StartCoroutine(ShowWinCoroutine(starCount));
    }

    private IEnumerator ShowWinCoroutine(int starCount)
    {
        yield return new WaitForSeconds(0.5f);

        if (starCount < stars.Length)
        {
            for (int i = 0; i <= starCount; i++)
            {
                stars[i].enabled = true;

                if (i > 0)
                {
                    stars[i - 1].enabled = false;
                }

                yield return new WaitForSeconds(0.5f);
            }
        }

        scoreText.enabled = true;
    }


    public void OnReplayClicked()
    {
        string currentSceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        UnityEngine.SceneManagement.SceneManager.LoadScene(currentSceneName);
    }

    public void OnDoneClicked()
    {
        int last = PlayerPrefs.GetInt("LastPlayedStage", 1); // 1-based מהכפתור במפה
        LevelProgress.UnlockUpTo(last + 1);                  // פותח את השלב הבא
        UnityEngine.SceneManagement.SceneManager.LoadScene("menuScene");
    }
}