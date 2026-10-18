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
            // 1. Cek kalau yang kena itu PINTU
            InteractableDoor door = hit.collider.GetComponent<InteractableDoor>();
            if (door != null)
            {
                if (promptText != null) promptText.text = "[ E ] Buka / Tutup";
                if (Input.GetKeyDown(KeyCode.E)) door.Interact();
                return;
            }

            // 2. Cek kalau yang kena itu ITEM
            ItemPickup item = hit.collider.GetComponent<ItemPickup>();
            if (item != null)
            {
                if (promptText != null) promptText.text = "[ E ] Ambil " + item.itemData.itemName;
                if (Input.GetKeyDown(KeyCode.E)) item.PickUp();
                return;
            }
        }

        // Kalau nggak kena apa-apa
        if (promptText != null) promptText.text = "";
    }
}