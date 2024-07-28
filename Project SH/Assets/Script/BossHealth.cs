using UnityEngine;
using UnityEngine.UI; // Để sử dụng UI Image
using System.Collections;

public class BossHealth : MonoBehaviour
{
    public float maxHealth = 100f; // Máu tối đa của boss
    public Image healthBarFill; // Thanh máu
    private float currentHealth;
    private float healthRegenInterval = 10f; // Thời gian hồi máu
    private float healthRegenAmount = 0.05f; // % hồi máu

    public bool IsAttacking { get; set; } // Thuộc tính cho biết boss có đang tấn công không
<<<<<<< HEAD

=======
    
>>>>>>> main
    void Start()
    {
        currentHealth = maxHealth;

        // Đặt thanh máu ban đầu
        if (healthBarFill != null)
        {
            healthBarFill.fillAmount = currentHealth / maxHealth;
        }

        InvokeRepeating("RegenerateHealth", healthRegenInterval, healthRegenInterval);
    }

    private void RegenerateHealth()
    {
        if (currentHealth < maxHealth)
        {
            currentHealth += maxHealth * healthRegenAmount;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

            // Cập nhật thanh máu
            if (healthBarFill != null)
            {
                healthBarFill.fillAmount = currentHealth / maxHealth;
            }

            Debug.Log("Boss hồi máu!");
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        // Cập nhật thanh máu
        if (healthBarFill != null)
        {
            healthBarFill.fillAmount = currentHealth / maxHealth;
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Xử lý khi boss chết (ví dụ: xóa boss, phát thưởng, v.v.)
        Debug.Log("Boss đã chết!");
        Destroy(gameObject);
    }
}
