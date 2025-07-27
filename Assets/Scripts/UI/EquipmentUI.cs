using UnityEngine;
using UnityEngine.UI;

public class EquipmentUI : MonoBehaviour
{
    public Image headSlotIcon;
    public Image chestSlotIcon;
    public Image weaponSlotIcon;
    // ... diğer slotlar için de buraya Image değişkenleri ekleyin ...

    private CharacterEquipment characterEquipment;

    void Start()
    {
        // Player nesnesini bul ve üzerindeki CharacterEquipment script'ine ulaş.
        characterEquipment = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterEquipment>();
        // Ekipman değiştiğinde UI'ı güncellemek için event'e abone ol.
        characterEquipment.OnEquipmentChanged += UpdateUI;

        UpdateUI(); // Başlangıçta UI'ı bir kere güncelle.
    }

    void UpdateUI()
    {
        // Her bir yuvayı, CharacterEquipment'taki ilgili eşyanın ikonuyla doldur.
        UpdateSlot(EquipmentSlot.Head, headSlotIcon);
        UpdateSlot(EquipmentSlot.Chest, chestSlotIcon);
        UpdateSlot(EquipmentSlot.Weapon, weaponSlotIcon);
        // ... diğer slotlar için de buraya çağrı ekleyin ...
    }

    void UpdateSlot(EquipmentSlot slotType, Image iconImage)
    {
        if (iconImage == null) return;

        EquipmentItem itemInSlot = characterEquipment.GetItemInSlot(slotType);

        if (itemInSlot != null)
        {
            iconImage.sprite = itemInSlot.itemIcon;
            iconImage.color = Color.white; // Görünür yap.
        }
        else
        {
            iconImage.sprite = null;
            iconImage.color = new Color(1, 1, 1, 0); // Şeffaf yap.
        }
    }
}