using UnityEngine;

[CreateAssetMenu(fileName = "New_KickSkill", menuName = "aRPG-Nahrok/Skills/Kick Skill")]
public class KickSkill : BaseSkill
{
    [Header("Tekme Ayarları")]
    public float damage = 15f;
    public float kickRadius = 3f;   // Tekmenin ne kadar uzağa vuracağı
    public float kickAngle = 90f;   // Tekmenin açısı (90 derece = çeyrek daire)

    public override void Activate(BaseCharacter caster)
    {
        // Önce karakterin nereye bakacağını hesaplayalım.
        // Kameradan fare imlecinin olduğu yere bir ışın gönderiyoruz.
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        // Sadece 'Ground' (Zemin) layer'ına çarpan bir ışın istiyoruz ki yönümüzü doğru ayarlayalım.
        // Şimdilik layer olmadan devam edelim, basit tutalım.
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
        {
            // Karakterin pozisyonundan ışının çarptığı noktaya olan yönü hesapla.
            Vector3 targetDirection = hit.point - caster.transform.position;
            targetDirection.y = 0; // Y eksenindeki farkı sıfırla ki karakter havaya bakmasın.
            targetDirection.Normalize(); // Vektörün uzunluğunu 1 yap, sadece yön bilgisi kalsın.

            // Karakteri anında o yöne döndür. Bu, vuruş hissini artırır.
            caster.transform.forward = targetDirection;
        }

        // Şimdi hasar vereceğimiz düşmanları bulalım.
        // 1. ADIM: Tekmenin menzili içindeki tüm nesneleri bir küre çizerek bul (Ön filtreleme).
        Collider[] collidersInRadius = Physics.OverlapSphere(caster.transform.position, kickRadius);

        // 2. ADIM: Bu nesnelerin içinde, tekmenin açısında kalanları bul ve hasar ver.
        foreach (Collider col in collidersInRadius)
        {
            // Nesne bir karakter mi ve bizden başkası mı?
            if (col.TryGetComponent<BaseCharacter>(out BaseCharacter target) && target != caster)
            {
                // Karakterin baktığı yön ile hedefe olan yön arasındaki açıyı hesapla.
                Vector3 directionToTarget = (target.transform.position - caster.transform.position).normalized;
                float angleToTarget = Vector3.Angle(caster.transform.forward, directionToTarget);

                // Eğer bu açı, bizim tekme açımızın yarısından küçükse, hedef koninin içindedir.
                if (angleToTarget < kickAngle / 2)
                {
                    Debug.Log(caster.name + ", " + target.name + " hedefini tekmeledi!");
                    target.TakeDamage(damage);
                }
            }
        }
    }
}