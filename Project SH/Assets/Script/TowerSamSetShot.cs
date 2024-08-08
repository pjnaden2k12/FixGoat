using UnityEngine;

public class TowerShootingsam : MonoBehaviour
{
    public Tower tower; // Tham chiếu đến tháp
    public GameObject lightningBoltPrefab; // Prefab của tia sét
    public Transform firePoint; // Vị trí bắn tia sét

    private float fireCooldown = 0f; // Thời gian hồi giữa các lần bắn

    void Update()
    {
        if (tower == null) return; // Kiểm tra nếu chưa liên kết với tháp

        // Tìm mục tiêu gần nhất
        GameObject nearestEnemy = FindNearestEnemy();
        if (nearestEnemy != null)
        {
            // Tính khoảng cách tới mục tiêu
            float distanceToEnemy = Vector2.Distance(transform.position, nearestEnemy.transform.position);

            // Kiểm tra xem mục tiêu có nằm trong phạm vi bắn không
            if (distanceToEnemy <= tower.GetRange())
            {
                // Bắn tia sét
                if (fireCooldown <= 0f)
                {
                    Shoot(nearestEnemy.transform);
                    fireCooldown = 1f / tower.GetAttackSpeed(); // Đặt lại thời gian hồi dựa trên tốc độ tấn công
                }
            }
        }

        // Giảm thời gian hồi mỗi frame
        fireCooldown -= Time.deltaTime;
    }

    void Shoot(Transform target)
    {
        // Tạo tia sét tại vị trí bắn
        GameObject lightningBoltObject = Instantiate(lightningBoltPrefab, firePoint.position, Quaternion.identity);
        LightningBolt lightningBolt = lightningBoltObject.GetComponent<LightningBolt>();
        if (lightningBolt != null)
        {
            lightningBolt.Initialize(target);
            lightningBolt.SetDamage(tower.GetDamage()); // Đặt sát thương của đạn từ tháp
            
        }
    }

    GameObject FindNearestEnemy()
    {
        // Tìm tất cả các đối tượng với tag "Enemy" hoặc "Boss"
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] bosses = GameObject.FindGameObjectsWithTag("Boss");

        // Gộp danh sách quái và boss
        GameObject[] allTargets = new GameObject[enemies.Length + bosses.Length];
        enemies.CopyTo(allTargets, 0);
        bosses.CopyTo(allTargets, enemies.Length);

        GameObject nearestEnemy = null;
        float shortestDistance = Mathf.Infinity;

        foreach (GameObject enemy in allTargets)
        {
            float distanceToEnemy = Vector2.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        return nearestEnemy;
    }
}
