using UnityEngine;

// Bu script'in çalışabilmesi için PlayerExperience ve BaseCharacter script'lerinin de aynı nesnede olması gerekir.
[RequireComponent(typeof(PlayerExperience))]
[RequireComponent(typeof(BaseCharacter))]
public class PlayerStats : MonoBehaviour
{
    private PlayerExperience playerExperience;
    private BaseCharacter characterStats;
    
    [Header("Kaynaklar")]
    [SerializeField] private int currentGold = 0;

    void Awake()
    {
        playerExperience = GetComponent<PlayerExperience>();
        characterStats = GetComponent<BaseCharacter>();
    }

    // Dışarıdan çağrılacak olan ana metot budur.
    public void SpendStatPoint(StatType statToIncrease)
    {
        // Harcanacak puan var mı?
        if (playerExperience.AvailableStatPoints > 0)
        {
            // Puanı harca.
            playerExperience.UseStatPoint();
            
            // İlgili istatistiği artır.
            characterStats.AddToStat(statToIncrease, 1);

            Debug.Log(statToIncrease.ToString() + " istatistiği 1 artırıldı!");
        }
        else
        {
            Debug.LogWarning("Harcanacak istatistik puanı yok!");
        }
    }
    
    public void AddGold(int amount)
    {
        currentGold += amount;
        Debug.Log(amount + " altın toplandı! Toplam altın: " + currentGold);
    }
}