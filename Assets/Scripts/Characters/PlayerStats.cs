using UnityEngine;

[RequireComponent(typeof(BaseCharacter))]
[RequireComponent(typeof(CharacterEquipment))]
public class PlayerStats : MonoBehaviour
{
    private BaseCharacter baseCharacter;
    private CharacterEquipment characterEquipment;
    private PlayerExperience playerExperience; // PlayerExperience'a referans ekledik.

    [Header("Kaynaklar")]
    [SerializeField] private int currentGold = 0;

    void Awake()
    {
        baseCharacter = GetComponent<BaseCharacter>();
        characterEquipment = GetComponent<CharacterEquipment>();
        playerExperience = GetComponent<PlayerExperience>(); // Referansı Awake'de alıyoruz.
    }

    // ## ÖNCEKİ ADIMDA EKSİK KALAN METOT ##
    // Bu metot, stat puanı harcamak için PlayerCharacter tarafından çağrılıyor.
    public void SpendStatPoint(StatType statToIncrease)
    {
        // Harcanacak puan var mı diye PlayerExperience'ı kontrol et.
        if (playerExperience != null && playerExperience.AvailableStatPoints > 0)
        {
            // Puanı harca.
            playerExperience.UseStatPoint();

            // İlgili istatistiği artır.
            baseCharacter.AddToStat(statToIncrease, 1);

            Debug.Log(statToIncrease.ToString() + " istatistiği 1 artırıldı!");
        }
        else
        {
            Debug.LogWarning("Harcanacak istatistik puanı yok!");
        }
    }

    // Belirli bir istatistiğin TOPLAM değerini döndüren metot.
    public int GetTotalStatValue(StatType stat)
    {
        int baseValue = baseCharacter.GetBaseStat(stat);
        int bonusValue = characterEquipment.GetStatBonus(stat);

        return baseValue + bonusValue;
    }

    public void AddGold(int amount)
    {
        currentGold += amount;
        Debug.Log(amount + " altın toplandı! Toplam altın: " + currentGold);
    }
}