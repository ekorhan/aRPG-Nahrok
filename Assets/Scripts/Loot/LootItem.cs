using UnityEngine;

public class LootItem : MonoBehaviour
{
    private int goldAmount = 0;
    private BaseItem item;

    public void SetGoldValue(int amount)
    {
        goldAmount = amount;
    }

    public void SetItem(BaseItem itemToSet)
    {
        item = itemToSet;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Bize çarpan nesne oyuncu mu?
        Debug.Log("LootItem OnTriggerEnter: " + other.name);
        if (other.CompareTag("Player"))
        {
            Debug.Log("LootItem OnTriggerEnter: Player detected");
            // Eğer altın varsa, altını oyuncunun PlayerStats script'ine ekle.
            if (goldAmount > 0)
            {
                // GetComponent null dönerse hata vermemesi için '?' kullanıyoruz.
                other.GetComponent<PlayerStats>()?.AddGold(goldAmount);
            }

            // Eğer eşya varsa, onu InventoryManager'a ekle.
            if (item != null)
            {
                InventoryManager.Instance.AddItem(item);
            }

            // Loot toplandığı için bu nesneyi yok et.
            Destroy(gameObject);
        }
    }
}