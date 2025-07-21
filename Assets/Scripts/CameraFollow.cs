using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Takip edilecek hedef (Player)
    public float smoothSpeed = 0.125f; // Takip yumuşaklığı
    private Vector3 offset; // Kamera ile hedef arasındaki mesafe

    void Start()
    {
        if (target == null) return;

        // Oyun başladığında kamera ile hedef arasındaki mesafeyi hesapla ve kaydet.
        offset = transform.position - target.position;
    }

    // LateUpdate, tüm Update'ler bittikten sonra çalışır.
    // Bu, karakter hareket ettikten sonra kameranın hareket etmesini sağlayarak titremeyi önler.
    void LateUpdate()
    {
        if (target == null) return;

        // Hedef pozisyonu, oyuncunun pozisyonuna offset'i ekleyerek bul.
        Vector3 desiredPosition = target.position + offset;
        // Kameranın mevcut pozisyonundan hedef pozisyona doğru yumuşak bir geçiş yap.
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Kameranın pozisyonunu güncelle.
        transform.position = smoothedPosition;

        // (İsteğe bağlı) Kameranın her zaman hedefe bakmasını sağla.
        transform.LookAt(target);
    }
}