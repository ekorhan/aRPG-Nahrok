using UnityEngine;

// "abstract" anahtar kelimesi, bu sınıftan doğrudan bir nesne oluşturulamayacağını,
// sadece başka sınıflar tarafından miras alınabileceğini belirtir.
public abstract class BaseCharacter : MonoBehaviour
{
    // public: Diğer scriptlerden ve Unity editöründen erişilebilir.
    // protected: Sadece bu sınıf ve onu miras alan alt sınıflar erişebilir.
    [Header("Temel Karakter İstatistikleri")]
    [SerializeField] protected float maxHealth = 100f; // [SerializeField] private/protected değişkenleri editörde görünür yapar.
    protected float currentHealth;

    [SerializeField] protected float moveSpeed = 5f;

    // "virtual" anahtar kelimesi, bu metodun alt sınıflar tarafından ezilebileceğini (override) belirtir.
    // Awake, bir nesne ilk oluşturulduğunda çalışan bir Unity metodudur.
    protected virtual void Awake()
    {
        currentHealth = maxHealth;
    }

    // Tüm karakterlerin bir saldırı metodu olmalı.
    // "abstract" olduğu için bu sınıf içinde kodunu yazmıyoruz, miras alan her sınıf bunu yazmak ZORUNDA.
    public abstract void Attack();

    // Tüm karakterler hasar alabilmeli.
    public virtual void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        Debug.Log(gameObject.name + " " + damageAmount + " hasar aldı! Kalan can: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Tüm karakterler ölebilmeli.
    protected virtual void Die()
    {
        Debug.Log(gameObject.name + " öldü!");
        // Şimdilik sadece nesneyi yok ediyoruz. Gelecekte animasyon, ses ekleyeceğiz.
        Destroy(gameObject);
    }
}