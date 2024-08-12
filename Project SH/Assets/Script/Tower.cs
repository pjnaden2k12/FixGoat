using UnityEngine;
using static TowerManager;

public class Tower : MonoBehaviour
{
    public static Tower Instance { get; private set; }

    public int id; // ID hoặc tên duy nhất cho mỗi tháp
    public int level = 1; // Cấp độ của tháp
    public float baseDamage = 10f; // Sát thương cơ bản
    public float baseAttackSpeed = 1f; // Tốc độ tấn công cơ bản
    public float baseRange = 5f; // Tầm bắn cơ bản

    private float damage; // Sát thương hiện tại
    private float attackSpeed; // Tốc độ tấn công hiện tại
    private float range; // Tầm bắn hiện tại

    void Start()
    {
        UpdateStats(); // Cập nhật thông số khi bắt đầu
    }

    public void Upgrade()
    {
        if (level < 3) // Kiểm tra cấp độ tối đa
        {
            level++;
            UpdateStats(); // Cập nhật thông số sau khi nâng cấp
        }
        else
        {
            Debug.LogWarning("Tower is already at maximum level!");
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
                range = baseRange * 1.5f; // Tăng tầm bắn ở cấp độ 2
                break;
            case 3:
                damage = baseDamage * 3f; // Tăng sát thương ở cấp độ 3
                attackSpeed = baseAttackSpeed * 2f; // Tăng tốc độ tấn công ở cấp độ 3
                range = baseRange * 2f; // Tăng tầm bắn ở cấp độ 3
                break;
        }

        // Hiển thị thông tin tháp khi cập nhật
        Debug.Log($"Tower ID: {id}, Level: {level}, Damage: {damage}, Attack Speed: {attackSpeed}, Range: {range}");
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
