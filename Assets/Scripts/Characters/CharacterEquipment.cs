using System.Collections.Generic;
using UnityEngine;
using System; // Action (event) için gerekli!

public class CharacterEquipment : MonoBehaviour
{
    public event Action OnEquipmentChanged;
    private Dictionary<EquipmentSlot, EquipmentItem> equippedItems = new Dictionary<EquipmentSlot, EquipmentItem>();

    public void Equip(EquipmentItem newItem)
    {
        EquipmentSlot slot = newItem.equipSlot;

        // Bu yuvadaki eski eşyayı envantere geri gönder (şimdilik sadece çıkarıyoruz).
        if (equippedItems.ContainsKey(slot))
        {
            Unequip(slot);
        }

        equippedItems[slot] = newItem;
        Debug.Log(newItem.itemName + " giyildi. (" + slot.ToString() + ")");
        // Ekipman değişti, olayı tetikle!
        OnEquipmentChanged?.Invoke();
    }

    public void Unequip(EquipmentSlot slot)
    {
        if (equippedItems.Remove(slot, out EquipmentItem oldItem))
        {
            Debug.Log(oldItem.itemName + " çıkarıldı.");
            // Eşyayı envantere geri ekle (şimdilik envanter doluysa kaybolabilir, sonra düzelteceğiz)
            InventoryManager.Instance.AddItem(oldItem);

            // Ekipman değişti, olayı tetikle!
            OnEquipmentChanged?.Invoke();
        }
    }
    public EquipmentItem GetItemInSlot(EquipmentSlot slot)
    {
        if (equippedItems.TryGetValue(slot, out EquipmentItem item))
        {
            return item;
        }
        return null;
    }
    // İstenen stat için toplam bonusu hesaplayan YENİ metot.
    public int GetStatBonus(StatType stat)
    {
        int bonus = 0;
        Debug.Log("equippedItems.Values: " + equippedItems.Values.Count);
        foreach (EquipmentItem item in equippedItems.Values)
        {
            Debug.Log("Eşya: " + item.itemName + ", Stat: " + stat.ToString());
            switch (stat)
            {
                case StatType.Strength:
                    bonus += item.strengthBonus;
                    break;
                case StatType.Dexterity:
                    bonus += item.dexterityBonus;
                    break;
                case StatType.Intelligence:
                    bonus += item.intelligenceBonus;
                    break;
            }
        }
        return bonus;
    }
}