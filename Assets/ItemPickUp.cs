using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public ItemData itemData; // Slot buat masukin resep item (Apel/Medkit)

    // Fungsi ini dipanggil sama Player pas tekan E
    public void PickUp()
    {
        if (itemData != null)
        {
            // Cari script Inventory di Player, lalu tambahin item ini
            Inventory playerInventory = FindObjectOfType<Inventory>();
            
            if (playerInventory != null)
            {
                playerInventory.AddItem(itemData);
                Debug.Log("Berhasil ambil: " + itemData.itemName);
                Destroy(gameObject); // Hapus item dari dunia
            }
        }
    }
}