using System.Collections.Generic;
using UnityEngine;

public class CharacterEquipment : MonoBehaviour
{
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
    }

    public void Unequip(EquipmentSlot slot)
    {
        if (equippedItems.Remove(slot, out EquipmentItem oldItem))
        {
            Debug.Log(oldItem.itemName + " çıkarıldı.");
        }
    }

    // İstenen stat için toplam bonusu hesaplayan YENİ metot.
    public int GetStatBonus(StatType stat)
    {
        int bonus = 0;
        foreach (EquipmentItem item in equippedItems.Values)
        {
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