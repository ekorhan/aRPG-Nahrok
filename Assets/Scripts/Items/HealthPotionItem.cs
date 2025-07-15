using UnityEngine;

// CreateAssetMenu, Unity editöründe Sağ Tık > Create menüsüne yeni bir seçenek ekler.
[CreateAssetMenu(fileName = "New Health Potion", menuName = "aRPG-Nahrok/Items/Health Potion")]
public class HealthPotionItem : BaseItem
{
    [Header("İksir Ayarları")]
    public int healAmount = 50;

    public HealthPotionItem()
    {
        // Bu eşyanın türünü varsayılan olarak "Consumable" yap.
        type = ItemType.Consumable;
    }

    // BaseItem'daki Use metodunu eziyoruz (override).
    public override void Use(BaseCharacter user)
    {
        base.Use(user); // "Sağlık İksiri kullanıldı." mesajını yazdırmak için ana metodu çağırıyoruz.
        
        // Eşyayı kullanan karakterin canını artır.
        // Şimdilik canı doğrudan artırıyoruz, ileride BaseCharacter'a bir Heal() metodu ekleyebiliriz.
        user.TakeDamage(-healAmount); // Negatif hasar vermek, canı artırmak için basit bir yöntem.
        
        Debug.Log(user.name + " " + healAmount + " can yeniledi.");
        
        // TODO: İksiri envanterden sil. Bu kısmı envanter sistemini yazınca yapacağız.
    }
}