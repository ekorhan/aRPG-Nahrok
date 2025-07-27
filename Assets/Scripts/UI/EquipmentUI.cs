using UnityEngine;
using UnityEngine.UI;

public class EquipmentUI : MonoBehaviour
{
    public Image headSlotIcon;
    public Image chestSlotIcon;
    public Image weaponSlotIcon;
    public Image legSlotIcon;
    // ... diğer slotlar ...

    private CharacterEquipment characterEquipment;

    void Start()
    {
        characterEquipment = GameObject.FindGameObjectWithTag("Player")?.GetComponent<CharacterEquipment>();
        if (characterEquipment != null)
        {
            characterEquipment.OnEquipmentChanged += UpdateUI;
            UpdateUI();
        }
        else
        {
            Debug.LogError("EquipmentUI: Sahnede 'Player' tag'ine sahip bir CharacterEquipment bulunamadı!");
        }
    }

    void UpdateUI()
    {
        Debug.Log("--- EquipmentUI GÜNCELLENİYOR ---");
        UpdateSlot(EquipmentSlot.Head, headSlotIcon);
        UpdateSlot(EquipmentSlot.Chest, chestSlotIcon);
        UpdateSlot(EquipmentSlot.Weapon, weaponSlotIcon);
        UpdateSlot(EquipmentSlot.Legs, legSlotIcon);
        // ... diğer slotlar ...
    }

    void UpdateSlot(EquipmentSlot slotType, Image iconImage)
    {
        if (iconImage == null)
        {
            Debug.LogWarning(slotType.ToString() + " yuvası için Inspector'da icon atanmamış.");
            return;
        }

        // iconImage'in bir üstündeki parent'ın (yani Slot'un kendisinin) Image bileşenini al.
        Image slotBackgroundImage = iconImage.transform.parent.GetComponent<Image>();

        EquipmentItem itemInSlot = characterEquipment.GetItemInSlot(slotType);

        if (itemInSlot != null)
        {
            // Yuvada eşya varsa:
            iconImage.sprite = itemInSlot.itemIcon;
            iconImage.color = Color.white; // ikonu görünür yap.

            // (İsteğe bağlı) Slot'un arka planını da hafif görünür yapabiliriz.
            if (slotBackgroundImage != null) slotBackgroundImage.color = new Color(1, 1, 1, 1);
        }
        else
        {
            // Yuvada eşya yoksa:
            //iconImage.sprite = null; //TODO: ekorhan eger item yoksa yine de ikonunu temizleyeceksek bunu yapabiliriz.
            iconImage.color = new Color(1, 1, 1, 1); // ikonu şeffaf yap.

            // Slot'un kendi arka planını da şeffaf yap!
            if (slotBackgroundImage != null) slotBackgroundImage.color = new Color(1, 1, 1, 0.5f);
        }
    }
}