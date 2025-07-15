using UnityEngine;

public abstract class BaseCharacter : MonoBehaviour
{
    [Header("Temel Karakter İstatistikleri")]
    [SerializeField] protected float maxHealth = 100f;
    protected float currentHealth;

    [SerializeField] protected float moveSpeed = 5f;

    // --- YENİ EKLENEN BÖLÜM ---
    [Header("Ana İstatistikler (Stats)")]
    [SerializeField] protected int strength = 10;     // Güç: Fiziksel hasarı etkiler.
    [SerializeField] protected int dexterity = 10;    // Çeviklik: Saldırı hızı, kritik vuruş şansını etkiler.
    [SerializeField] protected int intelligence = 10; // Zeka: Büyü hasarını, manayı etkiler.
    // --- YENİ BÖLÜM SONU ---


    protected virtual void Awake()
    {
        currentHealth = maxHealth;
    }

    public abstract void Attack();

    public virtual void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        Debug.Log(gameObject.name + " " + damageAmount + " hasar aldı! Kalan can: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        Debug.Log(gameObject.name + " öldü!");
        Destroy(gameObject);
    }

    // --- YENİ EKLENEN METOT ---
    // Bu metot, dışarıdan bir istatistiğin değerini almamızı sağlar.
    // Örneğin, bir kılıç kuşanıldığında "strength" değerini öğrenmek için kullanılabilir.
    public int GetStat(StatType type)
    {
        switch (type)
        {
            case StatType.Strength: return strength;
            case StatType.Dexterity: return dexterity;
            case StatType.Intelligence: return intelligence;
            default: return 0;
        }
    }
    // --- YENİ METOT SONU ---
    // Bu metodu BaseCharacter sınıfının en altına ekle

    public void AddToStat(StatType stat, int amount)
    {
        switch (stat)
        {
            case StatType.Strength:
                strength += amount;
                break;
            case StatType.Dexterity:
                dexterity += amount;
                break;
            case StatType.Intelligence:
                intelligence += amount;
                break;
        }
    }
}

// --- YENİ EKLENEN ENUM ---
// Bu "enum", istatistikleri kod içinde string ("Strength") olarak yazmak yerine
// daha güvenli ve hatasız bir şekilde (StatType.Strength) kullanmamızı sağlar.
public enum StatType
{
    Strength,
    Dexterity,
    Intelligence
}
// --- YENİ ENUM SONU ---