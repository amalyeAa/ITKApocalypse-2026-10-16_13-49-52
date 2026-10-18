using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // List buat nyimpen item yang diambil
    public List<ItemData> items = new List<ItemData>();

    public void AddItem(ItemData item)
    {
        items.Add(item);
        Debug.Log("Item masuk tas! Total item: " + items.Count);
    }

    // Fungsi buat makan item (dipanggil nanti di UI)
    public void UseItem(int index)
    {
        if (index < items.Count)
        {
            ItemData item = items[index];
            PlayerStats stats = GetComponent<PlayerStats>();

            if (item.type == ItemType.Food)
            {
                stats.Eat(item.effectAmount);
            }
            else if (item.type == ItemType.Medicine)
            {
                stats.Heal(item.effectAmount);
            }

            items.RemoveAt(index); // Hapus dari tas setelah dipakai
            Debug.Log("Item dipakai dan dihapus dari tas.");
        }
    }
}