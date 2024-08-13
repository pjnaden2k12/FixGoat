using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Prefab của quái vật
    public Transform[] spawnPoints; // Các điểm spawn
    public float initialSpawnInterval = 5f; // Thời gian spawn ban đầu (giây)
    public float spawnIntervalDecrease = 0.5f; // Giảm thời gian spawn sau mỗi khoảng thời gian (giây)
    public float minSpawnInterval = 1f; // Thời gian spawn tối thiểu (giây)
    private float spawnInterval; // Thời gian spawn hiện tại
    private float timeSinceLastSpawn; // Thời gian kể từ lần spawn cuối cùng

    void Start()
    {
        // Khởi tạo thời gian spawn với giá trị ban đầu
        spawnInterval = initialSpawnInterval;
        timeSinceLastSpawn = 0f;
    }

    void Update()
    {
        // Cập nhật thời gian kể từ lần spawn cuối cùng
        timeSinceLastSpawn += Time.deltaTime;

        // Kiểm tra xem đã đến lúc spawn chưa
        if (timeSinceLastSpawn >= spawnInterval)
        {
            SpawnEnemy();
            timeSinceLastSpawn = 0f; // Reset thời gian kể từ lần spawn cuối cùng

            // Giảm thời gian spawn nếu lớn hơn giá trị tối thiểu
            if (spawnInterval > minSpawnInterval)
            {
                spawnInterval -= spawnIntervalDecrease;
            }
        }
    }

    void SpawnEnemy()
    {
        // Chọn một điểm spawn ngẫu nhiên
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        // Instantiate quái vật tại điểm spawn
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
