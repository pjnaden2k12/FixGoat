using UnityEngine;
using UnityEngine.UI;

public class BGoblinManager : MonoBehaviour
{
    public GameObject bossPrefab; // Prefab của boss
    public Transform spawnPoint; // Vị trí sinh ra boss
    public Image healthBarFill; // Thanh máu của boss
    public Transform targetPoint; // Điểm mục tiêu của boss

    private GameObject currentBoss; // Boss hiện tại
    private float spawnDelay = 120f; // Thời gian chờ trước khi sinh boss
    private bool hasSpawnedBoss = false; // Cờ kiểm tra xem boss đã được sinh ra chưa

    void Start()
    {
        // Gọi phương thức SpawnBoss sau spawnDelay giây
        Invoke("SpawnBoss", spawnDelay);
    }

    void SpawnBoss()
    {
        // Kiểm tra nếu boss chưa được sinh ra
        if (!hasSpawnedBoss)
        {
            if (bossPrefab != null && spawnPoint != null && targetPoint != null)
            {
                currentBoss = Instantiate(bossPrefab, spawnPoint.position, Quaternion.identity);
                Debug.Log("Boss đã được sinh ra!");

                // Thiết lập thanh máu cho boss
                BGoblinHealth enemyHealth = currentBoss.GetComponent<BGoblinHealth>();
                if (enemyHealth != null)
                {
                    enemyHealth.healthBarFill = healthBarFill;
                }

                // Thiết lập điểm mục tiêu của boss
                BGoblinMove bossMovement = currentBoss.GetComponent<BGoblinMove>();
                if (bossMovement != null)
                {
                    bossMovement.targetPoint = targetPoint;
                }

                // Đặt cờ đã sinh boss
                hasSpawnedBoss = true;
            }
            else
            {
                Debug.LogWarning("Boss Prefab, vị trí sinh ra, hoặc điểm mục tiêu không được thiết lập!");
            }
            if (Input.GetKeyDown(KeyCode.Space)) // Nhấn phím Space để gây thiệt hại
            {
                GetComponent<BGoblinHealth>().TakeDamage(10f);
            }
        }
    }
}
