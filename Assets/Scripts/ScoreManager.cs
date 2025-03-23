using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private UI_Manager ui_Manager;
    public int CurrentScore { get; private set; }
    public int HighScore { get; private set; }

    public static ScoreManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;

        LoadHighScore();
    }

    public void UpdateScore(int points)
    {
        CurrentScore += points;

        if (CurrentScore > HighScore)
        {
            HighScore = CurrentScore;
            SaveHighScore();
        }

        ui_Manager.UpdateText(CurrentScore);
    }

    private void LoadHighScore()
    {
        HighScore = PlayerPrefs.GetInt("HighScore", 0);
        ui_Manager.UpdateHighScore(HighScore);
    }

    private void SaveHighScore()
    {
        PlayerPrefs.SetInt("HighScore", HighScore);
        ui_Manager.UpdateHighScore(HighScore);
    }
}