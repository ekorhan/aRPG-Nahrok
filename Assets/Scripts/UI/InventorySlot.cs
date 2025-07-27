using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; // Tıklama olaylarını yakalamak için bu gerekli!

public class InventorySlot : MonoBehaviour, IPointerClickHandler
{
    public Image icon; // Slot'un içindeki ikon resmi (Inspector'dan atanacak)

    private BaseItem item; // Bu slotun temsil ettiği eşya
    private PlayerCharacter player; // Oyuncu referansı

    void Start()
    {
        // Oyuncuyu bul ve referansını al.
        player = GameObject.FindGameObjectWithTag("Player")?.GetComponent<PlayerCharacter>();
    }

    // Bu metot, InventoryUI tarafından çağrılarak slot'a eşya atar.
    public void AddItem(BaseItem newItem)
    {
        item = newItem;
        icon.sprite = item.itemIcon;
        icon.color = new Color(1, 1, 1, 1); // Görünür yap.
    }

    // Bu metot, slot'u temizler.
    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.color = new Color(1, 1, 1, 1); // Şeffaf yap.
    }

    // IPointerClickHandler arayüzünden gelen bu metot, slot'a tıklandığında çalışır.
    public void OnPointerClick(PointerEventData eventData)
    {
        // Eğer slot boş değilse ve SAĞ MOUSE tuşuna tıklandıysa...
        if (item != null && eventData.button == PointerEventData.InputButton.Right)
        {
            // Eşyanın kendi "Use" metodunu çağır.
            // Bu metot, eşya iksir ise can doldurur, ekipman ise giymeyi tetikler.
            item.Use(player);
        }
    }
}