using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UI elemanları için bu satır gerekli!

public class InventoryUI : MonoBehaviour
{
    public GameObject inventoryPanel;       // Inspector'dan sürükleyeceğimiz ana panel
    public GameObject characterPanel;
    public Transform slotsGridParent;       // Slotların içine yaratılacağı Grid Layout nesnesi
    public GameObject slotPrefab;           // Inspector'dan sürükleyeceğimiz Slot prefab'ı

    private InventoryManager inventoryManager;
    private List<GameObject> activeSlots = new List<GameObject>();

    void Start()
    {
        inventoryManager = InventoryManager.Instance;
        // Envanter her güncellendiğinde bizim UpdateUI metodumuzu da çağır.
        inventoryManager.OnInventoryChanged += UpdateUI;

        // Başlangıçta paneli kapat.
        inventoryPanel.SetActive(false);
        characterPanel.SetActive(false);
    }

    void Update()
    {
        // "I" tuşuna basıldığında envanter panelini aç/kapat.
        if (Input.GetKeyDown(KeyCode.I))
        {
            bool isActive = !inventoryPanel.activeSelf;
            inventoryPanel.SetActive(isActive);
            characterPanel.SetActive(isActive);
        }
    }

    void UpdateUI()
    {
        // 1. Önceki tüm slotları temizle.
        foreach (GameObject slot in activeSlots)
        {
            Destroy(slot);
        }
        activeSlots.Clear();

        // 2. InventoryManager'daki her bir eşya için yeni bir slot yarat.
        foreach (BaseItem item in inventoryManager.items)
        {
            GameObject newSlot = Instantiate(slotPrefab, slotsGridParent);

            // Slot'un içindeki ItemIcon resmini bul ve eşyanın ikonuyla değiştir.
            Image itemIcon = newSlot.transform.Find("ItemIcon").GetComponent<Image>();
            itemIcon.sprite = item.itemIcon;
            itemIcon.color = Color.white; // Resmi görünür yap.

            Debug.Log(item.itemName + " için slot yaratıldı.");

            activeSlots.Add(newSlot);
        }
    }
}