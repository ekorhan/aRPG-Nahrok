using UnityEngine;
using System.Collections.Generic; // Listeler için bu satır gerekli!

public class BasicMonster : BaseCharacter
{
    [Header("Ganimet Ayarları")]
    [SerializeField] private int experienceAmount = 10; // Bu canavar öldüğünde verilecek XP miktarı
    [SerializeField] private GameObject goldLootPrefab; // Yaratılacak altın nesnesinin prefab'ı
    [SerializeField] private int goldDropAmount = 25;   // Düşecek altın miktarı
    // Tek bir eşya yerine, bir eşya listesi (Ganimet Tablosu)
    [SerializeField] private List<BaseItem> possibleLoot;
    [Range(0, 1)] // 0 ile 1 arasında bir yüzde değeri
    [SerializeField] private float itemDropChance = 0.5f; // Eşya düşürme ihtimali (%50)

    private Transform player;

    protected override void Awake()
    {
        base.Awake();
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    void Update()
    {
        if (player != null)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);
        }
    }

    public override void Attack()
    {
        // Canavarların henüz bir saldırı metodu yok.
    }

    // --- DEĞİŞEN BÖLÜM ---
    protected override void Die()
    {
        // Oyuncuyu bul ve XP vermeyi dene.
        // ... XP verme kodu aynı ...
        PlayerExperience playerExperience = player.GetComponent<PlayerExperience>();
        if (playerExperience != null)
        {
            playerExperience.AddExperience(experienceAmount);
        }

        // Altın Düşürme
        if (goldLootPrefab != null && goldDropAmount > 0)
        {
            GameObject lootObject = Instantiate(goldLootPrefab, transform.position, Quaternion.identity);
            // Doğru metot adını çağırıyoruz: SetGoldValue
            lootObject.GetComponent<LootItem>()?.SetGoldValue(goldDropAmount);
        }

        // Eşya Düşürme
        if (Random.value <= itemDropChance)
        {
            // Ganimet tablosu boş değilse...
            if (possibleLoot != null && possibleLoot.Count > 0)
            {
                // Listeden rastgele bir eşya seç.
                BaseItem itemToDrop = possibleLoot[Random.Range(0, possibleLoot.Count)];
                BaseItem itemToDrop1 = possibleLoot[Random.Range(0, possibleLoot.Count)];
                BaseItem itemToDrop2 = possibleLoot[Random.Range(0, possibleLoot.Count)];

                // Seçilen eşyayı yarat.
                if (itemToDrop != null && goldLootPrefab != null)
                {
                    GameObject lootObject = Instantiate(goldLootPrefab, transform.position + new Vector3(1, 0, 0), Quaternion.identity);
                    lootObject.GetComponent<LootItem>()?.SetItem(itemToDrop);
                    GameObject lootObject1 = Instantiate(goldLootPrefab, transform.position + new Vector3(1, 0, 0), Quaternion.identity);
                    lootObject1.GetComponent<LootItem>()?.SetItem(itemToDrop1);
                    GameObject lootObject2 = Instantiate(goldLootPrefab, transform.position + new Vector3(1, 0, 0), Quaternion.identity);
                    lootObject2.GetComponent<LootItem>()?.SetItem(itemToDrop2);
                }
            }
        }

        // base.Die() metodunu en son çağırıyoruz.
        base.Die();
    }
    // --- DEĞİŞİKLİK SONU ---
}