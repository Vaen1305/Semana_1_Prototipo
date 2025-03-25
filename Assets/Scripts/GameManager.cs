using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Configuración")]
    [SerializeField] private GameObject foodPrefab;
    [SerializeField] private Vector2 limits = new Vector2(8, 4);
    [SerializeField] private LayerMask obstacleLayer;
    [SerializeField] private UI_Manager uiManager;

    public void GenerateFood()
    {
        Vector2 spawnPos;
        bool validPosition = false;
        int maxAttempts = 100;

        do
        {
            spawnPos = new Vector2(
                Mathf.Round(Random.Range(-limits.x, limits.x)),
                Mathf.Round(Random.Range(-limits.y, limits.y))
            );

            validPosition = !Physics2D.OverlapCircle(spawnPos, 0.4f, obstacleLayer);
            maxAttempts--;
        } while (!validPosition && maxAttempts > 0);

        if (validPosition)
        {
            Instantiate(foodPrefab, spawnPos, Quaternion.identity);
        }
    }

    public void EndGame()
    {
        Time.timeScale = 0;
        uiManager.ShowGameOver();
    }
}
