using UnityEngine;

// BaseSkill'deki CreateAssetMenu'yu burada da kullanıyoruz ki bu yetenekten de asset oluşturabilelim.
[CreateAssetMenu(fileName = "New_PunchSkill", menuName = "aRPG-Nahrok/Skills/Punch Skill")]
public class PunchSkill : BaseSkill
{
    [Header("Yumruk Ayarları")]
    public float damage = 25f;
    public float attackRange = 10f; // Yumruğun ne kadar uzağa "ulaşabildiği"

    // BaseSkill'den gelen zorunlu metodu dolduruyoruz.
    public override void Activate(BaseCharacter caster)
    {
        Camera mainCamera = Camera.main;
        if (mainCamera == null) return;

        // 1. ADIM: Menzil sınırı OLMADAN bir ışın göndererek neye tıkladığımızı öğrenelim.
        // Mathf.Infinity, "sonsuz menzil" anlamına gelir.
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
        {
            // Bir şeye tıkladık. Bu bir canavar mı (BaseCharacter mı)?
            if (hit.collider.TryGetComponent<BaseCharacter>(out BaseCharacter target))
            {
                // Kendimize tıklamadığımızdan emin olalım.
                if (target == caster) return;

                // 2. ADIM: Tıkladığımız hedef ile bizim aramızdaki mesafeyi ölçelim.
                float distanceToTarget = Vector3.Distance(caster.transform.position, target.transform.position);

                // 3. ADIM: Mesafeyi yeteneğin menziliyle karşılaştıralım.
                if (distanceToTarget <= attackRange)
                {
                    // Menzil içindeyiz! Saldırıyı gerçekleştir.
                    Debug.Log(caster.name + ", " + target.name + " hedefine '" + skillName + "' yeteneğini kullandı!");
                    target.TakeDamage(damage);
                }
                else
                {
                    // Menzil dışındayız! İstenen log mesajını yazdır.
                    // Debug.LogWarning, mesajı konsolda sarı bir uyarı olarak gösterir, daha dikkat çekicidir.
                    // ToString("F1"), mesafeyi 1 ondalık basamakla yazar (örn: 125.3).
                    Debug.LogWarning("Menzil yetmedi! Hedef " + distanceToTarget.ToString("F1") +
                                     " mesafede. Mevcut menzil: " + attackRange);
                }
            }
        }
    }
}