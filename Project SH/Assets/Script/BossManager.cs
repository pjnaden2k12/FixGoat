using UnityEngine;
using UnityEngine.UI; // Thêm dòng này để sử dụng các lớp UI như Image

public class BossManager : MonoBehaviour
{
    
    public Transform spawnPoint; // Vị trí sinh ra boss
    public Transform targetPoint; // Điểm mà boss sẽ di chuyển tới
    public GameObject bossHealthUIPrefab; // Prefab của thanh máu boss
    private GameObject bossInstance;
    private GameObject bossHealthUIInstance;

    
    private float healthRegenDelay = 90f; // Thời gian chờ để hồi máu (1 phút 30 giây)
    private bool bossSpawned = false;

    void Start()
    {
        SpawnBoss();
    }

    void SpawnBoss()
    {
        if (!bossSpawned)
        {
            
            bossHealthUIInstance = Instantiate(bossHealthUIPrefab, Vector3.zero, Quaternion.identity); // Tạo UI thanh máu

            // Gán UI thanh máu cho boss
            BossHealthUI bossHealthUI = bossHealthUIInstance.GetComponent<BossHealthUI>();
            if (bossHealthUI != null)
            {
                bossHealthUI.healthBarFill = bossInstance.GetComponentInChildren<Image>(); // Gán Image của thanh máu
                bossHealthUI.UpdateHealthBar();
            }

            bossSpawned = true;

            // Bắt đầu di chuyển boss tới mục tiêu
            BossMovement bossMovement = bossInstance.GetComponent<BossMovement>();
            if (bossMovement != null)
            {
                bossMovement.MoveToTarget(targetPoint);
            }

            // Bắt đầu hồi máu sau thời gian nhất định
            Invoke("RegenerateBossHealth", healthRegenDelay);
        }
    }

    void RegenerateBossHealth()
    {
        if (bossInstance != null)
        {
            BossHealth bossHealth = bossInstance.GetComponent<BossHealth>();
            if (bossHealth != null)
            {
                bossHealth.RegenerateHealth(0.7f); // Hồi 70% máu
            }

            // Cập nhật thanh máu sau khi hồi máu
            BossHealthUI bossHealthUI = bossHealthUIInstance.GetComponent<BossHealthUI>();
            if (bossHealthUI != null)
            {
                bossHealthUI.UpdateHealthBar();
            }
        }
    }
}
