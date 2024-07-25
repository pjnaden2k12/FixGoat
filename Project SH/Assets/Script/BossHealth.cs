using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public float maxHealth = 100f; // Máu tối đa của boss
    private float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    public void RegenerateHealth(float percentage)
    {
        currentHealth = Mathf.Min(maxHealth, currentHealth + maxHealth * percentage);
    }

    public float GetHealthPercentage()
    {
        return currentHealth / maxHealth;
    }

    void Die()
    {
        // Xử lý khi boss chết
        Debug.Log("Boss chết!");
        Destroy(gameObject);
    }
}
