using UnityEngine;

public class HealerHealth : MonoBehaviour
{
    // Số lượng máu tối đa
    public int maxHealth = 100;
    // Số lượng máu hiện tại
    private int currentHealth;
    // Animator component
    private Animator animator;
    // Renderer component
    private Renderer rend;

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        rend = GetComponent<Renderer>();
    }

    // Nhận sát thương
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }
        Debug.Log("Took damage. Current health: " + currentHealth);

        if (currentHealth == 0)
        {
            Die();
        }
    }

    // Hồi máu
    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        Debug.Log("Healed. Current health: " + currentHealth);
    }

    // Xử lý khi chết
    void Die()
    {
        Debug.Log("Has died.");
        if (animator != null)
        {
            animator.SetTrigger("Die");
        }
        StartCoroutine(FadeOut());
    }

    // Coroutine để làm mờ
    System.Collections.IEnumerator FadeOut()
    {
        float duration = 2.0f;
        float counter = 0f;

        Color originalColor = rend.material.color;

        while (counter < duration)
        {
            counter += Time.deltaTime;
            float alpha = Mathf.Lerp(1, 0, counter / duration);
            rend.material.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        Destroy(gameObject);
    }

    void OnMouseDown()
    {
        TakeDamage(10); // Số lượng sát thương khi click chuột, có thể thay đổi
    }
}
