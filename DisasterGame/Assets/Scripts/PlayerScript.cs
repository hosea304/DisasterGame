using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [Header("Jump Settings")]
    [SerializeField] private float jumpForce = 7f; // Kekuatan lompatan
    private bool isGrounded = false; // Apakah player menyentuh lantai?

    private Rigidbody rb;

    void Start()
    {
        // Ambil komponen Rigidbody
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Input untuk melompat
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
    }

    private void Jump()
    {
        // Tambahkan gaya vertikal untuk melompat
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isGrounded = false; // Hanya bisa melompat sekali sebelum mendarat lagi
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Jika menyentuh platform, set isGrounded menjadi true
        if (collision.gameObject.CompareTag("Platform"))
        {
            isGrounded = true;
        }
    }
}
