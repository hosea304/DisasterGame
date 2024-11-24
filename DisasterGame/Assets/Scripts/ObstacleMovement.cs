using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    [Header("Scale Settings")]
    [SerializeField] private float minScale = 1f;
    [SerializeField] private float maxScale = 4f;
    [SerializeField] private float scaleSpeed = 2f;

    [Header("References")]
    [SerializeField] private GameManager gameManager;

    private bool isScalingUp = true;
    private Vector3 originalScale;
    private Vector3 originalPosition;

    void Start()
    {
        originalScale = transform.localScale;
        originalPosition = transform.position;
    }

    void Update()
    {
        ScaleObstacle();
    }

    void ScaleObstacle()
    {
        // Get current Y scale
        float currentYScale = transform.localScale.y;

        // Calculate new scale
        if (isScalingUp)
        {
            currentYScale += scaleSpeed * Time.deltaTime;
            if (currentYScale >= maxScale)
            {
                currentYScale = maxScale;
                isScalingUp = false;
            }
        }
        else
        {
            currentYScale -= scaleSpeed * Time.deltaTime;
            if (currentYScale <= minScale)
            {
                currentYScale = minScale;
                isScalingUp = true;
            }
        }

        // Apply new scale
        transform.localScale = new Vector3(originalScale.x, currentYScale, originalScale.z);

        // Adjust position to keep bottom fixed
        float heightDifference = (currentYScale - originalScale.y) / 2;
        transform.position = new Vector3(
            originalPosition.x,
            originalPosition.y + heightDifference,
            originalPosition.z
        );
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && gameManager != null)
        {
            gameManager.HandlePlayerHit();
        }
    }
}