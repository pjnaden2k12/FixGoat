using UnityEngine;

public class Tower : MonoBehaviour
{
    public int level = 1; // Cấp độ của tháp, mặc định là 1
    public float baseDamage = 10f; // Sát thương cơ bản của tháp
    public float baseAttackSpeed = 1f; // Tốc độ tấn công cơ bản (số lượng tấn công mỗi giây)

    private float damage; // Sát thương hiện tại của tháp
    private float attackSpeed; // Tốc độ tấn công hiện tại

    void Start()
    {
        UpdateStats(); // Cập nhật thông số khi bắt đầu
    }

    // Phương thức nâng cấp tháp
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

    // Cập nhật thông số của tháp dựa trên cấp độ
    private void UpdateStats()
    {
        switch (level)
        {
            case 1:
                damage = baseDamage;
                attackSpeed = baseAttackSpeed;
                break;
            case 2:
                damage = baseDamage * 1.5f; // Tăng sát thương ở cấp độ 2
                attackSpeed = baseAttackSpeed * 1.2f; // Tăng tốc độ tấn công ở cấp độ 2
                break;
            case 3:
                damage = baseDamage * 2f; // Tăng sát thương ở cấp độ 3
                attackSpeed = baseAttackSpeed * 1.5f; // Tăng tốc độ tấn công ở cấp độ 3
                break;
        }

        // Cập nhật các thuộc tính khác của tháp nếu cần
        Debug.Log($"Tower Level: {level}, Damage: {damage}, Attack Speed: {attackSpeed}");
    }

    // Phương thức để lấy sát thương của tháp (có thể sử dụng cho các hệ thống khác)
    public float GetDamage()
    {
        return damage;
    }

    // Phương thức để lấy tốc độ tấn công của tháp (có thể sử dụng cho các hệ thống khác)
    public float GetAttackSpeed()
    {
        return attackSpeed;
    }
}
