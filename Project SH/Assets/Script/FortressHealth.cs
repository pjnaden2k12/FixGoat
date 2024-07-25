using UnityEngine;

public class FortressHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        Debug.Log("Tường thành bị thiệt hại: " + damage);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Xử lý khi tường thành chết (ví dụ: kết thúc trò chơi, phát thưởng, v.v.)
        Debug.Log("Tường thành đã bị phá hủy!");
        Destroy(gameObject);
    }
}
