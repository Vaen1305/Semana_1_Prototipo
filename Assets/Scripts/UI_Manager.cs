using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_Manager : MonoBehaviour
{
    public static UI_Manager Instance { get; private set; }

    [Header("UI Elements")]
    [SerializeField] private TMP_Text CurrentScoreText;
    [SerializeField] private TMP_Text HighScoreText;
    [SerializeField] private GameObject gameOverPanel;

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(gameObject);
        else Instance = this;
    }

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
        gameOverPanel.SetActive(true);
    }
}