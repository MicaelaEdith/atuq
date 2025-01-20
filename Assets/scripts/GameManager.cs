using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private bool gameOver = false;
    private string txt_gameOverTime = "You were too slow, try again.";
    private string txt_gameOverStars = "You collected too many stars, try again.";
    private string txt_winner = "100 points at the perfect timing, congratulations!";

    public int points = 0;

    [SerializeField]
    private TextMeshProUGUI txtPoints;
    [SerializeField]
    private TextMeshProUGUI txt_gameOverTitle;
    [SerializeField]
    private TextMeshProUGUI txt_gameOver;
    [SerializeField]
    private TextMeshProUGUI txt_btn_gameOver;
    [SerializeField]
    private GameObject GameUI;
    [SerializeField]
    private GameObject GameOverUI;
    private UnityEngine.UI.Image gameOverImage;
    private bool winner = false;

    private void Awake()
    {
        Instance = this;
        GameTimer.Instance.StartTimer(130f);

        gameOverImage = GameOverUI.GetComponent<UnityEngine.UI.Image>();
        if (gameOverImage != null)
        {
            Color color = gameOverImage.color;
            color.a = 0;
            gameOverImage.color = color;
        }

    }
    private void Start()
    {
        SoundManager.Instance.PlayMusic();
        
    }

    private void Update()
    {
        GameUI.SetActive(!gameOver);
        GameOverUI.SetActive(gameOver);
    }

    public void AddPoints(int starPoints)
    {
        points += starPoints;

        if (GameTimer.Instance.timeRemaining < 31) GameTimer.Instance.timeRemaining += 2;
        if (points < 0) points = 0;
        UpdatePointsUI();
    }

    private void UpdatePointsUI()
    {
        txtPoints.text = "Points: " + points;
    }

    public void Winner()
    {
        SoundManager.Instance.PlaySFX("win");
        winner = true;
        GameOver();
    }

    public void GameOver()
    {
        SoundManager.Instance.StopMusic();
        if (!winner) SoundManager.Instance.PlaySFX("lose");
        StartCoroutine(GameOverCoroutine());
    }

    private IEnumerator GameOverCoroutine()
    {
        Time.timeScale = 0f;

        float duration = 2f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime;
            if (gameOverImage != null)
            {
                Color color = gameOverImage.color;
                color.a = Mathf.Clamp01(elapsed / duration);
                gameOverImage.color = color;
            }
            yield return null;
        }

        Time.timeScale = 0f;

        gameOver = true;
        if (!winner)
        {
            if (points > 100) txt_gameOver.text = txt_gameOverStars;
            else txt_gameOver.text = txt_gameOverTime;

            txt_btn_gameOver.text = "Try Again!";
            txt_gameOverTitle.text = "Game Over";
        }
        else
        {
            txt_gameOver.text = txt_winner;
            txt_gameOverTitle.text = "Winner!";
            txt_btn_gameOver.text = "Play Again!";
        }
    }

    public void PlayAgain()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("game");
    }
}
