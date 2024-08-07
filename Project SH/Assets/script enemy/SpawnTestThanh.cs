using UnityEngine;
using System.Collections;

public class SpawnManagerTest : MonoBehaviour
{
    public GameObject[] enemyPrefabs;       // Mảng chứa các loại quái vật (prefab)
    public float spawnInterval = 10f;       // Khoảng thời gian giữa các lần spawn

    public float spawnXMin = -10f;          // Giới hạn x nhỏ nhất cho điểm spawn
    public float spawnXMax = 10f;           // Giới hạn x lớn nhất cho điểm spawn
    public float targetXMin = -5f;          // Giới hạn x nhỏ nhất cho điểm mục tiêu
    public float targetXMax = 5f;           // Giới hạn x lớn nhất cho điểm mục tiêu

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
            spawnCount += 1; // Tăng số lượng quái vật sẽ được spawn trong lần tiếp theo
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnEnemy()
    {
        if (enemyPrefabs.Length == 0) return;

        // Chọn một prefab quái vật ngẫu nhiên từ mảng
        int randomEnemyIndex = Random.Range(0, enemyPrefabs.Length);
        GameObject enemyPrefab = enemyPrefabs[randomEnemyIndex];

        // Chọn một điểm spawn ngẫu nhiên trong phạm vi x đã định
        Vector3 spawnPosition = new Vector3(
            Random.Range(spawnXMin, spawnXMax),
            0, // Nếu điểm spawn chỉ thay đổi trên trục x, giữ y = 0 hoặc giá trị tùy chỉnh
            0 // Giữ z = 0 hoặc giá trị tùy chỉnh
        );

        // Spawn quái vật tại điểm spawn
        GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

        // Chọn một điểm mục tiêu ngẫu nhiên trong phạm vi x đã định
        Vector3 targetPosition = new Vector3(
            Random.Range(targetXMin, targetXMax),
            0, // Nếu điểm mục tiêu chỉ thay đổi trên trục x, giữ y = 0 hoặc giá trị tùy chỉnh
            0 // Giữ z = 0 hoặc giá trị tùy chỉnh
        );

       
    }
}
