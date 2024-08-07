using UnityEngine;
using System.Collections;
using TMPro;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemyPrefabs;       // Mảng chứa các loại quái vật (prefab)
    public float spawnInterval = 2f;       // Khoảng thời gian giữa các lần spawn
    public Transform[] spawnPoints;         // Mảng chứa các điểm spawn
    public Transform[] targetPoints;        // Mảng chứa các điểm mục tiêu trên tường thành

    private float spawnCount;                 // Số lượng quái vật sẽ được spawn trong mỗi lần spawn

    void Start()
    {
        spawnCount = 0.01f; // Khởi tạo số lượng quái vật sẽ được spawn lần đầu tiên
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
            spawnCount += 1; // Tăng số lượng quái vật sẽ được spawn trong lần tiếp theo
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnEnemy()
    {
        if (enemyPrefabs.Length == 0 || spawnPoints.Length == 0 || targetPoints.Length == 0) return;

        // Chọn một prefab quái vật ngẫu nhiên từ mảng
        int randomEnemyIndex = Random.Range(0, enemyPrefabs.Length);
        GameObject enemyPrefab = enemyPrefabs[randomEnemyIndex];

        // Chọn một điểm spawn ngẫu nhiên từ mảng
        int randomSpawnIndex = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[randomSpawnIndex];

        // Spawn quái vật tại điểm spawn
        GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);

        // Chọn một điểm mục tiêu ngẫu nhiên từ mảng và gán cho quái vật
        int randomTargetIndex = Random.Range(0, targetPoints.Length);
        Transform targetPoint = targetPoints[randomTargetIndex];

        enemy.GetComponent<enemyAI>().SetTarget(targetPoint.position);
    }
}
