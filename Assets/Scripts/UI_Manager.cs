using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UI_Manager : SingletonPersistent<UI_Manager>
{
    [Header("UI Elements")]
    [SerializeField] private TMP_Text CurrentScoreText;
    [SerializeField] private TMP_Text HighScoreText;
    [SerializeField] private GameObject gameOverPanel;

    public void UpdateText(int currentScore)
    {
        CurrentScoreText.text = $"SCORE: {currentScore}";
    }

    public void UpdateHighScore(int highScore)
    {
        HighScoreText.text = $"HI-SCORE: {highScore}";
    }

    public void ShowGameOver()
    {
        Time.timeScale = 0;
        gameOverPanel.SetActive(true);
    }

    public void ResetGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        gameOverPanel.SetActive(false);
    }
}