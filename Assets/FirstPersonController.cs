using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    public CharacterController controller;
    public Transform playerCamera;

    public float walkSpeed = 5f;
    public float runSpeed = 8f;
    public float mouseSensitivity = 100f;
    public float gravity = -9.81f;
    public float jumpHeight = 1.5f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;
    float xRotation = 0f;

    // Variabel buat nyambungin ke PlayerStats
    private PlayerStats stats;

    void Start()
    {
        // Kunci kursor mouse ke tengah layar
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Ambil komponen PlayerStats dari objek yang sama
        stats = GetComponent<PlayerStats>();
    }

    void Update()
    {
        // --- 1. CEK TANAH ---
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // --- 2. LIHAT SEKITAR (MOUSE) ---
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Kamera lihat atas/bawah
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Badan muter kiri/kanan
        transform.Rotate(Vector3.up * mouseX);

        // --- 3. GERAK (WASD) ---
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        
        // --- LOGIKA BARU: CEK STAMINA BUAT LARI ---
        float currentSpeed = walkSpeed; // Defaultnya jalan biasa
        
        // Kalau pencet Shift DAN staminanya lebih dari 10, baru boleh lari
        if (Input.GetKey(KeyCode.LeftShift) && stats != null && stats.stamina > 10f)
        {
            currentSpeed = runSpeed;
        }

        controller.Move(move * currentSpeed * Time.deltaTime);

        // --- 4. LOMPAT & GRAVITASI ---
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}