using UnityEngine;
using System.Collections.Generic; // Listeler için bu gerekli

public class FlatlandMap : BaseMap
{
    private List<GameObject> activeEnemies = new List<GameObject>();
    private int enemiesToWin;

    void Start()
    {
        enemiesToWin = initialEnemyCount;
        SpawnEnemies();
    }

    public override void SpawnEnemies()
    {
        if (enemyPrefab == null || enemySpawnPoints.Length == 0)
        {
            Debug.LogError("Düşman Prefab'ı veya Spawn Noktaları atanmamış!");
            return;
        }

        for (int i = 0; i < initialEnemyCount; i++)
        {
            // Rasgele bir spawn noktası seç
            Transform spawnPoint = enemySpawnPoints[Random.Range(0, enemySpawnPoints.Length)];
            GameObject newEnemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
            activeEnemies.Add(newEnemy);
        }
    }

    public override void OnEnemyKilled()
    {
        enemiesToWin--;
        if (enemiesToWin <= 0)
        {
            WinGame();
        }
    }

    private void WinGame()
    {
        Debug.Log("KAZANDINIZ! Ana menüye dönülüyor...");
        // Burada bir "Kazandınız" ekranı gösterebilir ve sonra menüye dönebiliriz.
        // Şimdilik direkt ana menüye dönelim.
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}