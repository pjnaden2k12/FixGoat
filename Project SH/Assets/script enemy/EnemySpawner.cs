using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Prefab của quái vật
    public Transform[] spawnPoints; // Các điểm để nhân bản quái vật
    public float spawnInterval = 5f; // Khoảng thời gian giữa các lần nhân bản quái vật
    public int numberOfEnemies = 10; // Số lượng quái vật muốn nhân bản

    private int enemiesSpawned = 0; // Số lượng quái vật đã nhân bản

    void Start()
    {
        // Kiểm tra nếu mảng spawnPoints không rỗng
        if (spawnPoints.Length == 0)
        {
            Debug.LogError("Spawn Points array is empty. Please assign spawn points in the Inspector.");
            return;
        }

        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (enemiesSpawned < numberOfEnemies)
        {
            // Chọn một điểm ngẫu nhiên từ các điểm spawn
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

            // Tạo quái vật tại điểm spawn
            Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
            enemiesSpawned++;

            // Chờ khoảng thời gian trước khi nhân bản quái vật tiếp theo
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
