using UnityEngine;

[CreateAssetMenu(fileName = "New_KickSkill", menuName = "aRPG-Nahrok/Skills/Kick Skill")]
public class KickSkill : BaseSkill
{
    [Header("Tekme Ayarları")]
    public float baseDamage = 5f; // "damage" ismini "baseDamage" olarak değiştirdik.
    public float kickRadius = 3f;
    public float kickAngle = 90f;

    // --- YENİ EKLENEN BÖLÜM ---
    [Header("İstatistik Etkileşimi")]
    public float strengthScaling = 1.5f; // Güç'ün hasara ne kadar etki edeceği (çarpan)
    // --- YENİ BÖLÜM SONU ---


    public override void Activate(BaseCharacter caster)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
        {
            Vector3 targetDirection = hit.point - caster.transform.position;
            targetDirection.y = 0;
            targetDirection.Normalize();
            caster.transform.forward = targetDirection;
        }

        Collider[] collidersInRadius = Physics.OverlapSphere(caster.transform.position, kickRadius);

        foreach (Collider col in collidersInRadius)
        {
            if (col.TryGetComponent<BaseCharacter>(out BaseCharacter target) && target != caster)
            {
                Vector3 directionToTarget = (target.transform.position - caster.transform.position).normalized;
                float angleToTarget = Vector3.Angle(caster.transform.forward, directionToTarget);

                if (angleToTarget < kickAngle / 2)
                {
                    // --- DEĞİŞEN SATIRLAR ---
                    // Önceki kod: target.TakeDamage(damage);

                    // Yeni Hasaplama:
                    // 1. Yeteneği kullanan karakterin (caster) Güç (Strength) değerini al.
                    int casterStrength = caster.GetStat(StatType.Strength);
                    
                    // 2. Toplam hasarı hesapla: Temel Hasar + (Güç * Güç Çarpanı)
                    float totalDamage = baseDamage + (casterStrength * strengthScaling);

                    Debug.Log(caster.name + ", " + target.name + " hedefine " + totalDamage + " hasar vuruyor! (Temel: " + baseDamage + " + Güç Bonusu: " + (casterStrength * strengthScaling) + ")");
                    target.TakeDamage(totalDamage);
                    // --- DEĞİŞİKLİK SONU ---
                }
            }
        }
    }
}