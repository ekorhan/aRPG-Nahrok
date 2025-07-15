using UnityEngine;

public class BasicMonster : BaseCharacter
{
    // BaseCharacter'daki Awake metodu zaten canı dolduruyor.

    public override void Attack()
    {
        // Şimdilik canavarlar saldırmıyor.
        Debug.Log(gameObject.name + " saldırı animasyonu oynatıyor (ama bir şey yapmıyor).");
    }

    protected override void Die()
    {
        // Önce kendi ölüm işlemlerimizi tamamlayalım (yani base.Die() metodunu çağıralım).
        // Bu, konsola "öldü" mesajını yazdıracak ve nesneyi yok etme işlemini başlatacak.
        base.Die();

        // Kendi işimiz bittikten sonra haritaya haber verelim.
        // Nesne o an hemen yok olmaz, karenin sonunda yok edilir. Bu yüzden bu kod hala çalışır.
        FindObjectOfType<FlatlandMap>().OnEnemyKilled();
    }
}