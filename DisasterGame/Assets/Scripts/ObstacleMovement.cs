using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    [Header("Scale Settings")]
    [SerializeField] private float minHeight = 1f; // Tinggi minimum tembok
    [SerializeField] private float maxHeight = 4f; // Tinggi maksimum tembok
    [SerializeField] private float scaleSpeed = 2f; // Kecepatan perubahan skala

    private bool isScalingUp = true; // Menentukan arah perubahan skala
    private Vector3 originalScale; // Skala awal tembok
    private Vector3 originalPosition; // Posisi awal tembok

    void Start()
    {
        // Simpan skala awal dan posisi awal
        originalScale = transform.localScale;
        originalPosition = transform.position;
    }

    void Update()
    {
        ScaleObstacle();
    }

    private void ScaleObstacle()
    {
        // Ambil skala Y saat ini
        float currentHeight = transform.localScale.y;

        // Ubah skala Y (tinggi)
        if (isScalingUp)
        {
            currentHeight += scaleSpeed * Time.deltaTime;

            // Jika tinggi mencapai maksimum, ubah arah menjadi mengecil
            if (currentHeight >= maxHeight)
            {
                currentHeight = maxHeight;
                isScalingUp = false;
            }
        }
        else
        {
            currentHeight -= scaleSpeed * Time.deltaTime;

            // Jika tinggi mencapai minimum, ubah arah menjadi membesar
            if (currentHeight <= minHeight)
            {
                currentHeight = minHeight;
                isScalingUp = true;
            }
        }

        // Terapkan perubahan skala
        transform.localScale = new Vector3(originalScale.x, currentHeight, originalScale.z);

        // Sesuaikan posisi agar dasar tembok tetap di tempatnya
        float heightDifference = (currentHeight - originalScale.y) / 2;
        transform.position = new Vector3(
            originalPosition.x,
            originalPosition.y + heightDifference,
            originalPosition.z
        );
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Jika tembok mengenai player
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.HandlePlayerHit(); // Kurangi health player
        }
    }
}
