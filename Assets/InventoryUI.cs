using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    public GameObject inventoryPanel; // Panel Tas
    public Transform slotParent; // Tempat naro slot
    public GameObject slotPrefab; // Template slot (nanti kita bikin)

    private Inventory playerInventory;
    private bool isOpen = false;

    void Start()
    {
        playerInventory = FindObjectOfType<Inventory>();
        inventoryPanel.SetActive(false); // Sembunyiin pas awal
    }

    void Update()
    {
        // Buka tutup tas pakai Tab
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            isOpen = !isOpen;
            inventoryPanel.SetActive(isOpen);
            
            if (isOpen) UpdateUI(); // Refresh tampilan pas dibuka
        }
    }

    void UpdateUI()
    {
        // Hapus slot lama
        foreach (Transform child in slotParent)
        {
            Destroy(child.gameObject);
        }

        // Bikin slot baru sesuai isi tas
        foreach (ItemData item in playerInventory.items)
        {
            GameObject newSlot = Instantiate(slotPrefab, slotParent);
            
            // Set gambar ikon
            newSlot.transform.Find("Icon").GetComponent<Image>().sprite = item.icon;
            
            // Set tombol pakai
            Button btn = newSlot.transform.Find("UseButton").GetComponent<Button>();
            int index = playerInventory.items.IndexOf(item); // Simpan index
            btn.onClick.AddListener(() => {
                playerInventory.UseItem(index);
                UpdateUI(); // Refresh setelah dipakai
            });
        }
    }
}