using UnityEngine;
using UnityEngine.UI; // Để sử dụng UI Image
using System.Collections;

public class WirzardHealth : MonoBehaviour
{
    public float maxHealth = 100f; // Máu tối đa của boss
    public Image healthBarFill; // Thanh máu
    private float currentHealth;

    public bool IsAttacking { get; set; } // Thuộc tính cho biết boss có đang tấn công không

    void Start()
    {
        currentHealth = maxHealth;

        // Đặt thanh máu ban đầu
        if (healthBarFill != null)
        {
            healthBarFill.fillAmount = currentHealth / maxHealth;
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
        Destroy(gameObject);
    }
    void OnMouseDown()
    {
        // Giảm máu khi nhấp chuột
        TakeDamage(10f); // Số máu bị giảm khi nhấp chuột
    }
}
