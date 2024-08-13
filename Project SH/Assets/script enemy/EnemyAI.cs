using UnityEngine;
using System.Collections;

public class enemyAI : MonoBehaviour
{
    public int maxHealth = 100;
    private float currentHealth;
    private Animator animator;
    private bool isDead = false;
    public float moveSpeed = 0.05f;
    public float attackRange = 1.0f;
    private bool isAttacking = false;
    public Vector2 targetPosition;

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!isDead && !isAttacking)
        {
            Move();
        }
    }

    public void SetTarget(Vector3 target)
    {
        targetPosition = target;
    }

    public void TakeDamage(float damage)
    {
        if (isDead) return;

        currentHealth -= damage;
        Debug.Log($"Damage taken: {damage} Current Health: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (isDead) return;

        isDead = true;
        Debug.Log("Enemy died!");

        animator?.SetTrigger("Die");
        StartCoroutine(FadeOutAndDestroy());
    }

    IEnumerator FadeOutAndDestroy()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            Color color = renderer.material.color;
            for (float t = 0; t < 1; t += Time.deltaTime / 2f)
            {
                color.a = Mathf.Lerp(1, 0, t);
                renderer.material.color = color;
                yield return null;
            }
            renderer.material.color = new Color(color.r, color.g, color.b, 0);
        }

        Destroy(gameObject);
    }

    void OnMouseDown()
    {
        TakeDamage(10);
    }

    void Move()
    {
        float distance = Vector3.Distance(transform.position, targetPosition);
        if (distance > attackRange)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
        else
        {
            animator?.SetTrigger("Attack");
            isAttacking = true;
        }
    }
}
