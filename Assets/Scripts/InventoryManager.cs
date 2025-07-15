using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    // Singleton pattern: Bu script'e oyunun her yerinden kolayca erişmemizi sağlar.
    public static InventoryManager Instance { get; private set; }

    // Envanterdeki eşyaların listesi
    public List<BaseItem> items = new List<BaseItem>();

    private void Awake()
    {
        // Singleton kurulumu
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Sahneler arasında envanterin korunmasını sağlar.
        }
    }

    public void AddItem(BaseItem item)
    {
        items.Add(item);
        Debug.Log(item.itemName + " envantere eklendi!");
        // TODO: Envanter arayüzünü (UI) güncelle.
    }

    public void RemoveItem(BaseItem item)
    {
        if (items.Contains(item))
        {
            items.Remove(item);
            Debug.Log(item.itemName + " envanterden silindi.");
            // TODO: Envanter arayüzünü (UI) güncelle.
        }
    }
}