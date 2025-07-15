using UnityEngine;

public abstract class BaseMap : MonoBehaviour
{
    [Header("Harita Ayarları")]
    [SerializeField] protected Transform[] enemySpawnPoints; // Düşmanların doğacağı noktalar
    [SerializeField] protected GameObject enemyPrefab; // Yaratılacak düşman modeli (prefab)
    [SerializeField] protected int initialEnemyCount = 10; // Başlangıçtaki düşman sayısı

    // Miras alan her harita, düşmanları nasıl yaratacağını kendi belirlemeli.
    public abstract void SpawnEnemies();

    // Bir düşman öldüğünde bu metodu çağıracağız.
    public abstract void OnEnemyKilled();
}