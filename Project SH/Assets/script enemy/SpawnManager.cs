using UnityEngine;
using System.Collections;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemyPrefabs;       // Mảng chứa các loại quái vật (prefab)
    public float spawnInterval = 10f;       // Khoảng thời gian giữa các lần spawn
    public Transform[] spawnPoints;         // Mảng chứa các điểm spawn
    

    private int spawnCount;                 // Số lượng quái vật sẽ được spawn trong mỗi lần spawn

    void Start()
    {
        spawnCount = 1; // Khởi tạo số lượng quái vật sẽ được spawn lần đầu tiên
        StartCoroutine(SpawnEnemiesCoroutine());
    }

    IEnumerator SpawnEnemiesCoroutine()
    {
        while (true)
        {
            for (int i = 0; i < spawnCount; i++)
            {
                SpawnEnemy();
            }
            spawnCount += 0; // Tăng số lượng quái vật sẽ được spawn trong lần tiếp theo
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnEnemy()
    {
        

        // Chọn một prefab quái vật ngẫu nhiên từ mảng
        int randomEnemyIndex = Random.Range(0, enemyPrefabs.Length);
        GameObject enemyPrefab = enemyPrefabs[randomEnemyIndex];

        // Chọn một điểm spawn ngẫu nhiên từ mảng
        int randomSpawnIndex = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[randomSpawnIndex];

        // Spawn quái vật tại điểm spawn
        GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);

        

        Enemy1Health enemyHealth = enemy.GetComponent<Enemy1Health>();
      
    }
}
