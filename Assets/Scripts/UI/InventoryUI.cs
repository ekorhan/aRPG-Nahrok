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
            // Slot prefab'ını yarat.
            GameObject newSlotObject = Instantiate(slotPrefab, slotsGridParent);

            // Üzerindeki InventorySlot script'ini al.
            InventorySlot newSlot = newSlotObject.GetComponent<InventorySlot>();

            // Eşyayı bu yeni slota ata.
            newSlot.AddItem(item);

            activeSlots.Add(newSlotObject);
        }
    }
}