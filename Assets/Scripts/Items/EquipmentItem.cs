using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "aRPG-Nahrok/Items/Equipment")]
public class EquipmentItem : BaseItem
{
    [Header("Ekipman Ayarları")]
    public EquipmentSlot equipSlot; // Bu ekipman hangi yuvaya takılıyor?

    [Header(" verdiği İstatistikler")]
    public int strengthBonus;
    public int dexterityBonus;
    public int intelligenceBonus;
    public int healthBonus;
    public int armorBonus;

    public EquipmentItem()
    {
        // Bu eşyanın türünü varsayılan olarak "Equipment" yap.
        type = ItemType.Equipment;
    }

    // Ekipmanları "kullanmak", aslında onları "giymek" demektir.
    public override void Use(BaseCharacter user)
    {
        base.Use(user);
        // Eşyayı giymesi için InventoryManager'a haber ver.
        InventoryManager.Instance.EquipItem(this, user);
    }
}