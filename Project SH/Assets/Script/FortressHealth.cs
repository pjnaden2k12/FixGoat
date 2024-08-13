using UnityEngine;

public class FortressHealth : MonoBehaviour
{
    private float maxHealth = 100f;
    private float currentHealth;
    private float currentDefense;
    private float currentHealthRegen;
    public int levelId;
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
        RegenerateHealth();
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
    }

    private void RegenerateHealth()
    {
        // Hồi máu dựa trên giá trị hồi máu và thời gian đã trôi qua
        currentHealth += currentHealthRegen * Time.deltaTime;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Đảm bảo máu không vượt quá maxHealth
    }

    private void ApplyPermanentBonuses()
    {
        if (ResourceManager.Instance == null)
        {
            Debug.LogError("ResourceManager.Instance is null. Make sure ResourceManager is initialized.");
            return;
        }

        currentDefense = ResourceManager.Instance.wallDefenseBonus;
        currentHealthRegen = ResourceManager.Instance.healthRegenBonus; // Lấy giá trị hồi máu từ ResourceManager
        maxHealth += ResourceManager.Instance.wallHealthBonus; // Cập nhật maxHealth
    }

    private void Die()
    {
        Debug.Log("Tường thành đã bị phá hủy!");
        Time.timeScale = 0f;

        // Thực hiện các hành động khác khi tường thành bị phá hủy (nếu cần)
        RewardManager.Instance.ShowRewardPanel(levelId);
    }
}
