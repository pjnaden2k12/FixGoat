using UnityEngine;

public class EnemyHealthTest : MonoBehaviour
{
    // Số lượng máu tối đa của quái
    public int maxHealth = 100;
    // Số lượng máu hiện tại của quái
    private int currentHealth;

    void Start()
    {
        // Khởi tạo máu hiện tại bằng với máu tối đa khi bắt đầu
        currentHealth = maxHealth;
    }

    // Phương thức để quái nhận sát thương
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        // Kiểm tra nếu máu nhỏ hơn 0 thì đặt lại thành 0
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }
        Debug.Log("Enemy took damage. Current health: " + currentHealth);

        // Kiểm tra nếu máu bằng 0 thì gọi phương thức chết
        if (currentHealth == 0)
        {
            Die();
        }
    }

    // Phương thức để quái hồi máu
    public void Heal(int amount)
    {
        currentHealth += amount;
        // Đảm bảo máu hiện tại không vượt quá máu tối đa
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        Debug.Log("Enemy healed. Current health: " + currentHealth);
    }

    // Phương thức để xử lý khi quái chết
    void Die()
    {
        Debug.Log("Enemy has died.");
        // Thực hiện các hành động cần thiết khi quái chết, ví dụ: phá hủy đối tượng
        Destroy(gameObject);
    }
}
