using UnityEngine;

public class BasicMonster : BaseCharacter
{
    [Header("Ganimet Ayarları")]
    [SerializeField] private int experienceAmount = 10; // Bu canavar öldüğünde verilecek XP miktarı
    [SerializeField] private GameObject goldLootPrefab; // Yaratılacak altın nesnesinin prefab'ı
    [SerializeField] private int goldDropAmount = 25;   // Düşecek altın miktarı

    [SerializeField] private BaseItem itemToDrop; // Düşürülecek eşya (ScriptableObject)

    private Transform player;

    protected override void Awake()
    {
        base.Awake(); // BaseCharacter'daki Awake'i çağırmayı unutmuyoruz!
        player = GameObject.FindGameObjectWithTag("Player").transform;
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
        if (itemToDrop != null && goldLootPrefab != null) // goldLootPrefab kontrolü ekledik.
        {
            GameObject lootObject = Instantiate(goldLootPrefab, transform.position + new Vector3(1, 0, 0), Quaternion.identity);
            lootObject.GetComponent<LootItem>()?.SetItem(itemToDrop);
        }

        // base.Die() metodunu en son çağırıyoruz.
        base.Die();
    }
    // --- DEĞİŞİKLİK SONU ---
}