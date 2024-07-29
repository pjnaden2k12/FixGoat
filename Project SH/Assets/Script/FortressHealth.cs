using UnityEngine;

public class FortressHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;
    private float currentDefense;
    private float currentHealthRegen;

    public float CurrentHealth
    {
        get { return currentHealth; }
    }

    private void Start()
    {
        currentHealth = maxHealth;
        ApplyPermanentBonuses();
    }

    private void Update()
    {
        RegenerateHealth();
    }

    public void TakeDamage(float damage)
    {
        // Áp dụng sát thương sau khi giảm bởi phòng thủ (defense)
        float actualDamage = Mathf.Max(damage - currentDefense, 0);
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
        currentHealth += currentHealthRegen * Time.deltaTime;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
    }

    private void ApplyPermanentBonuses()
    {
        // Cập nhật chỉ số vĩnh viễn từ ResourceManager
        currentDefense = ResourceManager.Instance.wallDefenseBonus;
        currentHealthRegen = ResourceManager.Instance.healthRegenBonus;
        maxHealth += ResourceManager.Instance.wallHealthBonus;
    }

    private void Die()
    {
        Debug.Log("Tường thành đã bị phá hủy!");
        // Thực hiện các hành động khi tường thành bị phá hủy, ví dụ: kết thúc trò chơi
        Destroy(gameObject);
    }
}
