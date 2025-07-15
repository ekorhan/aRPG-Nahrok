using UnityEngine;

public class LootItem : MonoBehaviour
{
    // Bu değişkene, item'ı yaratan canavar değer atayacak.
    private int goldAmount = 0; 
    private BaseItem item;

    // Dışarıdan bu loot'un değerini atamak için kullanılacak metot.
    public void SetLootValue(int amount)
    {
        goldAmount = amount;
    }

    public void SetItem(BaseItem itemToSet)
    {
        item = itemToSet;
        // TODO: Eşyanın ikonuna göre yerdeki görünüşünü değiştirebiliriz.
    }

    // "Trigger" olarak ayarlanmış bir collider'a başka bir collider girdiğinde bu metot tetiklenir.
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Eğer altın varsa, altını ekle.
            if (goldAmount > 0)
            {
                other.GetComponent<PlayerStats>()?.AddGold(goldAmount);
            }

            // Eğer eşya varsa, envantere ekle.
            if (item != null)
            {
                InventoryManager.Instance.AddItem(item);
            }
            
            Destroy(gameObject);
        }
    }
}