using UnityEngine;
using UnityEngine.UI;

public class BossManager : MonoBehaviour
{
    public GameObject bossPrefab; // Prefab của boss

    public Image healthBarFill; // Thanh máu của boss
    public Transform targetPoint; // Điểm mục tiêu của boss

    private GameObject currentBoss; // Boss hiện tại
   

    void Start()
    {
    }

    void SpawnBoss()
    {
        // Kiểm tra nếu boss chưa được sinh ra
       

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

                
            if (Input.GetKeyDown(KeyCode.Space)) // Nhấn phím Space để gây thiệt hại
            {
                GetComponent<BossHealth>().TakeDamage(10f);
            }
        }
    }

