using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Configuración")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private Vector2 direction = Vector2.right;
    [SerializeField] private GameObject bodyPrefab;
    [SerializeField] private ScoreManager scoreManager;

    private List<Transform> bodySegments = new List<Transform>();
    private float moveTimer;

    private void Start()
    {
        bodySegments.Add(transform);
    }

    private void Update()
    {
        HandleInput();
    }

    private void FixedUpdate()
    {
        moveTimer += Time.fixedDeltaTime;
        if (moveTimer >= 1f / speed)
        {
            moveTimer = 0f;
            Move();
        }
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.W) && direction != Vector2.down)
            direction = Vector2.up;
        else if (Input.GetKeyDown(KeyCode.S) && direction != Vector2.up)
            direction = Vector2.down;
        else if (Input.GetKeyDown(KeyCode.A) && direction != Vector2.right)
            direction = Vector2.left;
        else if (Input.GetKeyDown(KeyCode.D) && direction != Vector2.left)
            direction = Vector2.right;
    }

    private void Move()
    {
        transform.position = new Vector2(
            Mathf.Round(transform.position.x + direction.x),
            Mathf.Round(transform.position.y + direction.y)
        );

        for (int i = bodySegments.Count - 1; i > 0; --i)
        {
            bodySegments[i].position = bodySegments[i - 1].position;
        }
    }

    private void GrowSnake()
    {
        GameObject newSegment = Instantiate(bodyPrefab);
        bodySegments.Add(newSegment.transform);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Food"))
        {
            scoreManager.UpdateScore(10);
            Destroy(other.gameObject);
            GrowSnake();
            GameManager.Instance.GenerateFood();
        }
        else if (other.CompareTag("Wall") || other.CompareTag("Body"))
        {
            GameManager.Instance.EndGame();
        }
    }
}