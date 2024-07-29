using UnityEngine;

public class FortressHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;
    public float defense = 10f; // Giảm sát thương nhận vào
    public float healthRegen = 5f; // Hồi máu mỗi giây

    public float CurrentHealth
    {
        get { return currentHealth; }
    }

    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        RegenerateHealth();
    }

    public void TakeDamage(float damage)
    {
        // Áp dụng sát thương sau khi giảm bởi phòng thủ (defense)
        float actualDamage = Mathf.Max(damage - defense, 0);
        currentHealth -= actualDamage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        Debug.Log("Tường thành bị thiệt hại: " + actualDamage);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void RegenerateHealth()
    {
        // Hồi máu theo thời gian
        currentHealth += healthRegen * Time.deltaTime;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
    }

    private void Die()
    {
        Debug.Log("Tường thành đã bị phá hủy!");
        // Thực hiện các hành động khi tường thành bị phá hủy, ví dụ: kết thúc trò chơi
        Destroy(gameObject);
    }
}
