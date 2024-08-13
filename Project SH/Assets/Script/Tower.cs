using UnityEngine;

public class Tower : MonoBehaviour
{
    public string nametower;
    public int id; // ID hoặc tên duy nhất cho mỗi tháp
    public int level = 1; // Cấp độ của tháp
    public float baseDamage = 10f; // Sát thương cơ bản
    public float baseAttackSpeed = 1f; // Tốc độ tấn công cơ bản
    public float baseRange = 5f; // Tầm bắn cơ bản
    public int upgradeCost = 50; // Chi phí nâng cấp cơ bản
    public int maxLevel = 3;

    
    private float damage; // Sát thương hiện tại
    private float attackSpeed; // Tốc độ tấn công hiện tại
    private float range; // Tầm bắn hiện tại

    void Start()
    {
        UpdateStats(); // Cập nhật thông số khi bắt đầu
    }

    public void Upgrade()
    {
        

        if (GearManager.Instance != null && GearManager.Instance.SpendGears(upgradeCost))
        {
            if (level < maxLevel) // Kiểm tra cấp độ tối đa
            {
                level++;
                UpdateStats(); // Cập nhật thông số sau khi nâng cấp
            }
            else
            {
                Debug.LogWarning("Tower is already at maximum level!");
            }
        }
        else
        {
            Debug.LogWarning("Not enough gears to upgrade the tower!");
        }
    }

    public void UpdateStats()
    {
        switch (level)
        {
            case 1:
                damage = baseDamage * 1f;
                attackSpeed = baseAttackSpeed * 1f;
                range = baseRange * 1f;
                break;
            case 2:
                damage = baseDamage * 2f; // Tăng sát thương ở cấp độ 2
                attackSpeed = baseAttackSpeed * 1.5f; // Tăng tốc độ tấn công ở cấp độ 2
                range = baseRange * 1.2f; // Tăng tầm bắn ở cấp độ 2
                break;
            case 3:
                damage = baseDamage * 3f; // Tăng sát thương ở cấp độ 3
                attackSpeed = baseAttackSpeed * 2f; // Tăng tốc độ tấn công ở cấp độ 3
                range = baseRange * 1.5f; // Tăng tầm bắn ở cấp độ 3
                break;
        }
    }

    public float GetDamage()
    {
        return damage;
    }

    public float GetAttackSpeed()
    {
        return attackSpeed;
    }

    public float GetRange()
    {
        return range;
    }
    
}
