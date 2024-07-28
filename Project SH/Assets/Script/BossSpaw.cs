using UnityEngine;

public class BossSpaw : MonoBehaviour
{
    public GameObject bossPrefab; // Prefab của boss
    public Transform spawnPoint; // Vị trí sinh ra boss

    private float spawnDelay = 2f; // Thời gian chờ trước khi sinh boss

    void Start()
    {
        // Gọi phương thức SpawnBoss sau spawnDelay giây
        Invoke("SpawnBoss", spawnDelay);
    }

    void SpawnBoss()
    {
        if (bossPrefab != null && spawnPoint != null)
        {
            Instantiate(bossPrefab, spawnPoint.position, Quaternion.identity);
            Debug.Log("Boss đã được sinh ra!");
        }
        else
        {
            Debug.LogWarning("Boss Prefab hoặc vị trí sinh ra không được thiết lập!");
        }
    }
}
