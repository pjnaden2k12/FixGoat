using UnityEngine;

public class Health : MonoBehaviour
{
    public float currentHealth = 100f;

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Xử lý khi đối tượng chết
        Destroy(gameObject);
    }
}
