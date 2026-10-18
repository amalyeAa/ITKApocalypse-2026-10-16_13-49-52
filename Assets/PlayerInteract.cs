using UnityEngine;
using TMPro;

public class PlayerInteract : MonoBehaviour
{
    public float interactDistance = 3f;
    public Transform playerCamera;
    public TextMeshProUGUI promptText;

    void Update()
    {
        Ray ray = new Ray(playerCamera.position, playerCamera.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactDistance))
        {
            InteractableDoor door = hit.collider.GetComponent<InteractableDoor>();

            if (door != null)
            {
                // Tampilkan teks terus selama ngadepin pintu
                if (promptText != null) promptText.text = "[ E ] Interact"; 

                // Kalau pencet E, panggil fungsi Interact (yang bisa buka & tutup)
                if (Input.GetKeyDown(KeyCode.E))
                {
                    door.Interact();
                }
                return; 
            }
        }
        
        // Sembunyiin kalau nggak ngadepin pintu
        if (promptText != null) promptText.text = "";
    }
}