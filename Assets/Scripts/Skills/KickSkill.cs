using UnityEngine;
using UnityEngine.Rendering.Universal; // Decal Projector için bu satır gerekli!

[CreateAssetMenu(fileName = "New_KickSkill", menuName = "aRPG-Nahrok/Skills/Kick Skill")]
public class KickSkill : BaseSkill
{
    [Header("Tekme Ayarları")]
    public float baseDamage = 5f;
    public float kickRadius = 5f;   // Tekmenin menzili (uzunluğu)
    public float kickAngle = 90f;   // Tekmenin açısı

    [Header("İstatistik Etkileşimi")]
    public float strengthScaling = 1.5f;

    [Header("Görsel Ayarlar")]
    public GameObject areaVisualizerPrefab; // Decal Projector prefab'ımız
    public float visualizerDuration = 0.5f; // Göstergenin ekranda kalma süresi

    public override bool Activate(BaseCharacter caster)
    {
        // Karakteri fare yönüne döndürme kısmı (PlayerCharacter'da olduğu için burada artık gereksiz)
        // Bu mantık artık PlayerCharacter.HandleRotation() içinde yönetiliyor.

        // Görsel göstergeyi yarat
        if (areaVisualizerPrefab != null)
        {
            // Prefab'ı karakterin pozisyonunda ve rotasyonunda yarat.
            GameObject visualizer = Object.Instantiate(areaVisualizerPrefab, caster.transform.position, caster.transform.rotation);

            // Decal Projector bileşenini bul ve ayarlarını yeteneğimizin özelliklerine göre güncelle.
            if (visualizer.TryGetComponent<DecalProjector>(out DecalProjector decalProjector))
            {
                // Decal'ın boyutunu tekmenin menziline (Z) ve açısına göre (X) ayarla.
                // Trigonometri kullanarak açıdan genişliği hesaplıyoruz.
                float width = kickRadius * Mathf.Tan(kickAngle * 0.5f * Mathf.Deg2Rad) * 2;
                // Yüksekliği (Y ekseni) makul bir değerde tutuyoruz, örneğin 4.
                decalProjector.size = new Vector3(width, 4f, kickRadius);
            }

            // Belirtilen süre sonunda göstergeyi yok et.
            Object.Destroy(visualizer, visualizerDuration);
        }

        // Düşmanları bulma ve hasar verme döngüsü
        Collider[] collidersInRadius = Physics.OverlapSphere(caster.transform.position, kickRadius);
        foreach (Collider col in collidersInRadius)
        {
            // Sadece düşmanlara hasar ver, kendine veya diğer oyunculara değil (geleceğe yönelik)
            if (col.TryGetComponent<BaseCharacter>(out BaseCharacter target) && target != caster)
            {
                Vector3 directionToTarget = (target.transform.position - caster.transform.position).normalized;
                float angleToTarget = Vector3.Angle(caster.transform.forward, directionToTarget);

                // Hedef, saldırı konisinin içinde mi diye kontrol et.
                if (angleToTarget < kickAngle / 2)
                {
                    PlayerStats playerStats = caster.GetComponent<PlayerStats>();
                    if (playerStats == null) return false; // Güvenlik kontrolü

                    int casterStrength = playerStats.GetTotalStatValue(StatType.Strength);
                    float totalDamage = baseDamage + (casterStrength * strengthScaling);

                    Debug.Log(caster.name + ", " + target.name + " hedefine " + totalDamage + " hasar vurdu!");
                    target.TakeDamage(totalDamage);
                    return true; // Saldırı başarılı olduysa true döndür.
                }
            }
        }
        return false;
    }
}