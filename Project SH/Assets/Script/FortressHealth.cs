using UnityEngine;

public class FortressHealth : MonoBehaviour
{
    private float maxHealth = 100f;
    private float currentHealth;
    private float currentDefense;
    private float currentHealthRegen;

    private float timeSinceLastDamage; // Biến để theo dõi thời gian từ lần cuối bị tấn công
    public float healthRegenDelay = 3f; // Thời gian chờ trước khi bắt đầu hồi máu

    public float MaxHealth
    {
        get { return maxHealth; }
    }

    public float CurrentHealth
    {
        get { return currentHealth; }
    }

    private void Start()
    {
        ApplyPermanentBonuses();
        currentHealth = maxHealth; // Đặt máu hiện tại bằng với máu tối đa đã được cập nhật
    }

    private void Update()
    {
        timeSinceLastDamage += Time.deltaTime;

        if (timeSinceLastDamage >= healthRegenDelay)
        {
            RegenerateHealth();
        }
    }

    public void TakeDamage(float damage)
    {
        float actualDamage = Mathf.Max(damage - currentDefense, 0);
        currentHealth -= actualDamage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        Debug.Log("Tường thành bị thiệt hại: " + actualDamage);

        if (currentHealth <= 0)
        {
            Die();
        }

        timeSinceLastDamage = 0f; // Reset thời gian từ lần cuối bị tấn công
    }

    private void RegenerateHealth()
    {
        currentHealth += currentHealthRegen * Time.deltaTime;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
    }

    private void ApplyPermanentBonuses()
    {
        if (ResourceManager.Instance == null)
        {
            Debug.LogError("ResourceManager.Instance is null. Make sure ResourceManager is initialized.");
            return;
        }

        currentDefense = ResourceManager.Instance.wallDefenseBonus;
        currentHealthRegen = ResourceManager.Instance.healthRegenBonus;
        maxHealth += ResourceManager.Instance.wallHealthBonus;
    }

    private void Die()
    {
        Debug.Log("Tường thành đã bị phá hủy!");
        Destroy(gameObject);
    }
}
