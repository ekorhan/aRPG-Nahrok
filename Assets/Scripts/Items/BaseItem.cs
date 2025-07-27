using UnityEngine;

// Yeni eşya türleri eklemek için kullanılacak enum
public enum ItemType
{
    Consumable, // Tüketilebilir (İksir vb.)
    Equipment,  // Ekipman (Kılıç, Zırh vb.)
    Miscellaneous // Diğer (Görev eşyası vb.)
}

public abstract class BaseItem : ScriptableObject
{
    [Header("Temel Eşya Bilgileri")]
    public string itemName;
    [TextArea] public string itemDescription;
    public Sprite itemIcon;// Envanterde görünecek ikon
    public ItemType type;

    // "virtual" anahtar kelimesi, bu metodun alt sınıflar tarafından ezilebileceğini (override) belirtir.
    // Bu metot, eşya kullanıldığında ne olacağını tanımlar.
    public virtual void Use(BaseCharacter user)
    {
        // Varsayılan olarak hiçbir şey yapmaz.
        Debug.Log(itemName + " kullanıldı.");
    }
}