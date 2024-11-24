using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Game Settings")]
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private Transform playerSpawnPoint;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text healthText;

    private int currentHealth;
    private int currentScore;
    private Transform playerTransform;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        currentHealth = maxHealth;
        currentScore = 0;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        UpdateUI();
    }

    public void HandlePlayerHit()
    {
        currentHealth--;
        UpdateUI();

        if (currentHealth <= 0)
        {
            GameOver();
        }
        else
        {
            RespawnPlayer();
        }
    }

    public void AddScore(int points)
    {
        currentScore += points;
        UpdateUI();
    }

    void UpdateUI()
    {
        if (scoreText != null) scoreText.text = "Score: " + currentScore;
        if (healthText != null) healthText.text = "Health: " + currentHealth;
    }

    void RespawnPlayer()
    {
        if (playerTransform != null && playerSpawnPoint != null)
        {
            playerTransform.position = playerSpawnPoint.position;
            playerTransform.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }

    void GameOver()
    {
        // Handle game over (e.g., show game over screen, restart level)
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}