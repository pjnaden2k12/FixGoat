using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SwordManHealth : MonoBehaviour
{
    public float maxHealth = 100f; // Máu tối đa của boss
    public Image healthBarFill; // Thanh máu
    private float currentHealth;

    public bool IsAttacking { get; set; } // Thuộc tính cho biết boss có đang tấn công không

    public float fadeDuration = 1.5f; // Thời gian làm mờ dần
    public float dieAnimationDuration = 2.0f; // Thời gian của animation chết

    private SpriteRenderer spriteRenderer;
    private Animator animator;

    void Start()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

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
        animator.SetTrigger("Die"); // Kích hoạt animation chết
        StartCoroutine(WaitForDieAnimation());
    }

    private IEnumerator WaitForDieAnimation()
    {
        yield return new WaitForSeconds(dieAnimationDuration); // Chờ đợi cho animation chết hoàn thành
        StartCoroutine(FadeOutAndDestroy());
    }

    private IEnumerator FadeOutAndDestroy()
    {
        Color originalColor = spriteRenderer.color;
        float fadeSpeed = 1f / fadeDuration;

        for (float t = 0; t < 1; t += Time.deltaTime * fadeSpeed)
        {
            Color newColor = originalColor;
            newColor.a = Mathf.Lerp(1, 0, t);
            spriteRenderer.color = newColor;
            yield return null;
        }

        // Đảm bảo rằng màu alpha là 0
        Color finalColor = originalColor;
        finalColor.a = 0;
        spriteRenderer.color = finalColor;

        Destroy(gameObject);
    }

    void OnMouseDown()
    {
        // Giảm máu khi nhấp chuột
        TakeDamage(10f); // Số máu bị giảm khi nhấp chuột
    }
}
