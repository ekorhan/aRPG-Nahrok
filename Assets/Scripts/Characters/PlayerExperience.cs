using UnityEngine;

public class PlayerExperience : MonoBehaviour
{
    [Header("Seviye Bilgileri")]
    [SerializeField] private int level = 1;
    [SerializeField] private int currentExperience = 0;
    [SerializeField] private int experienceToNextLevel = 100;

    [Header("Puanlar")]
    [SerializeField] private int statPointsPerLevel = 5; // Her seviyede verilecek puan sayısı
    
    // --- DEĞİŞEN SATIR ---
    // public: Diğer script'lerin bu değere erişebilmesi için.
    // { get; private set; }: Değeri dışarıdan okumaya izin verir ama sadece bu script'in içinden değiştirmeye izin verir.
    public int AvailableStatPoints { get; private set; }
    // --- DEĞİŞİKLİK SONU ---

    public void AddExperience(int amount)
    {
        currentExperience += amount;
        Debug.Log(amount + " XP kazanıldı! Toplam XP: " + currentExperience);

        if (currentExperience >= experienceToNextLevel)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        level++;
        currentExperience -= experienceToNextLevel;
        experienceToNextLevel = Mathf.RoundToInt(experienceToNextLevel * 1.5f);

        // --- YENİ EKLENEN SATIR ---
        AvailableStatPoints += statPointsPerLevel;
        // --- YENİ SATIR SONU ---

        Debug.LogWarning("SEVİYE ATLANDI! Yeni Seviye: " + level + ". Kazandığın puan: " + statPointsPerLevel);
    }
    
    // --- YENİ EKLENEN METOT ---
    public void UseStatPoint()
    {
        if (AvailableStatPoints > 0)
        {
            AvailableStatPoints--;
        }
    }
    // --- YENİ METOT SONU ---
}