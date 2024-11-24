using UnityEngine;

public class PlatformController : MonoBehaviour
{
    [Header("Rotation Settings")]
    [SerializeField] private float rotationSpeed = 50f; // Kecepatan rotasi platform

    void Update()
    {
        // Input rotasi platform
        if (Input.GetKey(KeyCode.A))
        {
            // Rotasi ke kiri
            transform.Rotate(Vector3.down * rotationSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            // Rotasi ke kanan
            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        }
    }
}
