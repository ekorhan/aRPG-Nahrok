using System.Collections.Generic;
using UnityEngine;
using System; // Action (event) için bu satır gerekli!

public class InventoryManager : MonoBehaviour
{
    // Singleton pattern: Bu script'e oyunun her yerinden kolayca erişmemizi sağlar.
    public static InventoryManager Instance { get; private set; }

    // Envanterdeki eşyaların listesi
    public List<BaseItem> items = new List<BaseItem>();

    // Envanter değiştiğinde tetiklenecek olay.
    public event Action OnInventoryChanged;
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
        OnInventoryChanged?.Invoke();
    }

    public void RemoveItem(BaseItem item)
    {
        if (items.Contains(item))
        {
            items.Remove(item);
            Debug.Log(item.itemName + " envanterden silindi.");
            OnInventoryChanged?.Invoke();
        }
    }
    public void UseFirstConsumable(BaseCharacter user)
    {
        BaseItem itemToUse = null;

        // Envanterdeki eşyalar arasında dön.
        foreach (BaseItem item in items)
        {
            // Türü "Consumable" olan ilk eşyayı bul.
            if (item.type == ItemType.Consumable)
            {
                itemToUse = item;
                break; // İlkini bulduktan sonra döngüden çık.
            }
        }

        // Kullanılacak bir eşya bulunduysa...
        if (itemToUse != null)
        {
            // 1. Eşyanın kendi "Use" metodunu çağır (örneğin iksirin can vermesi).
            itemToUse.Use(user);

            // 2. Kullandıktan sonra eşyayı envanterden sil.
            RemoveItem(itemToUse);
        }
        else
        {
            Debug.Log("Kullanılacak tüketilebilir eşya bulunamadı.");
        }
    }
    public void EquipItem(EquipmentItem item, BaseCharacter user)
    {
        // Karakterin ekipman yöneticisine ulaş ve eşyayı giydir.
        CharacterEquipment characterEquipment = user.GetComponent<CharacterEquipment>();
        if (characterEquipment != null)
        {
            characterEquipment.Equip(item);
            // Eşya giyildiği için envanterden kaldır.
            RemoveItem(item);
        }
    }
}