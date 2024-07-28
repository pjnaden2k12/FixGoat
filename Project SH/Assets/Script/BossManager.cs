using UnityEngine;
using UnityEngine.UI;

public class BossManager : MonoBehaviour
{
    public GameObject bossPrefab; // Prefab của boss
<<<<<<< HEAD
    public Transform spawnPoint; // Vị trí sinh ra boss
=======

>>>>>>> main
    public Image healthBarFill; // Thanh máu của boss
    public Transform targetPoint; // Điểm mục tiêu của boss

    private GameObject currentBoss; // Boss hiện tại
<<<<<<< HEAD
    private float spawnDelay = 120f; // Thời gian chờ trước khi sinh boss
    private bool hasSpawnedBoss = false; // Cờ kiểm tra xem boss đã được sinh ra chưa

    void Start()
    {
        // Gọi phương thức SpawnBoss sau spawnDelay giây
        Invoke("SpawnBoss", spawnDelay);
=======
   

    void Start()
    {
>>>>>>> main
    }

    void SpawnBoss()
    {
        // Kiểm tra nếu boss chưa được sinh ra
<<<<<<< HEAD
        if (!hasSpawnedBoss)
        {
            if (bossPrefab != null && spawnPoint != null && targetPoint != null)
            {
                currentBoss = Instantiate(bossPrefab, spawnPoint.position, Quaternion.identity);
                Debug.Log("Boss đã được sinh ra!");
=======
       
>>>>>>> main

                // Thiết lập thanh máu cho boss
                BossHealth bossHealth = currentBoss.GetComponent<BossHealth>();
                if (bossHealth != null)
                {
                    bossHealth.healthBarFill = healthBarFill;
                }

                // Thiết lập điểm mục tiêu của boss
                BossMovement bossMovement = currentBoss.GetComponent<BossMovement>();
                if (bossMovement != null)
                {
                    bossMovement.targetPoint = targetPoint;
                }

<<<<<<< HEAD
                // Đặt cờ đã sinh boss
                hasSpawnedBoss = true;
            }
            else
            {
                Debug.LogWarning("Boss Prefab, vị trí sinh ra, hoặc điểm mục tiêu không được thiết lập!");
            }
=======
                
>>>>>>> main
            if (Input.GetKeyDown(KeyCode.Space)) // Nhấn phím Space để gây thiệt hại
            {
                GetComponent<BossHealth>().TakeDamage(10f);
            }
        }
    }
<<<<<<< HEAD
}
=======

>>>>>>> main
