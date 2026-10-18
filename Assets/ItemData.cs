using UnityEngine;

// [CreateAssetMenu] bikin kita bisa bikin item baru lewat klik kanan di Project
[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public ItemType type;
    public float effectAmount; // Berapa banyak Hunger/Health yang dipulihkan
    public Sprite icon; // Gambar buat di UI nanti
}

public enum ItemType
{
    Food,   // Makanan (nambah Hunger)
    Medicine // Obat (nambah Health)
}