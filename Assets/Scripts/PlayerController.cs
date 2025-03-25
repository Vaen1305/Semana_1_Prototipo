using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    [Header("Configuración")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private Vector2 direction = Vector2.right;
    [SerializeField] private GameObject bodyPrefab;
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private GameManager gameManager;

    private List<Transform> bodySegments = new List<Transform>();
    private float moveTimer;

    private void Start()
    {
        if (gameManager == null)
        {
            gameManager = FindObjectOfType<GameManager>();
            if (gameManager == null)
            {
                Debug.LogError("GameManager no encontrado en la escena.");
            }
        }

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
        Vector2 prevPosition = transform.position;
        transform.position = new Vector2(
            Mathf.Round(transform.position.x + direction.x),
            Mathf.Round(transform.position.y + direction.y)
        );

        for (int i = bodySegments.Count - 1; i > 0; i--)
        {
            bodySegments[i].position = bodySegments[i - 1].position;
        }

        if (bodySegments.Count > 1)
        {
            bodySegments[1].position = prevPosition;
        }
    }

    private void GrowSnake()
    {
        Transform lastSegment = bodySegments[bodySegments.Count - 1];

        Vector2 newPosition = lastSegment.position - (Vector3)direction;

        GameObject newSegment = Instantiate(bodyPrefab, newPosition, Quaternion.identity);

        bodySegments.Add(newSegment.transform);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Food"))
        {
            scoreManager.UpdateScore(1);
            Destroy(other.gameObject);
            GrowSnake();

            if (gameManager != null)
            {
                StartCoroutine(GenerateFoodWithDelay());
            }
        }
        else if (other.CompareTag("Wall") || other.CompareTag("Body"))
        {
            if (gameManager != null)
            {
                gameManager.EndGame();
            }
            else
            {
                Debug.LogError("GameManager no está asignado. No se puede llamar a EndGame().");
            }
        }
    }

    private IEnumerator GenerateFoodWithDelay()
    {
        yield return new WaitForSeconds(0.1f);
        if (gameManager != null) gameManager.GenerateFood();
    }
}
