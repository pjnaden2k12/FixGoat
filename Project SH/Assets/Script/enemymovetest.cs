using UnityEngine;
using System.Collections; // Đảm bảo thêm namespace này

public class BossMovement : MonoBehaviour
{
    public float speed = 3.5f;
    public Transform targetPoint;
    public float attackDamage = 20f;
    public float attackInterval = 2f;

    private Rigidbody2D rb;
    private Animator animator;
    private bool isAttacking = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Di chuyển boss về phía targetPoint
        Vector2 direction = (Vector2)targetPoint.position - rb.position;
        direction.Normalize();

        if (!isAttacking)
        {
            rb.velocity = direction * speed;
            animator.SetBool("isRunning", true);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            // Khi boss va chạm với tường thành, bắt đầu tấn công
            StartCoroutine(AttackTarget(collision.gameObject.GetComponent<FortressHealth>()));
        }
    }

    private IEnumerator AttackTarget(FortressHealth fortress)
    {
        isAttacking = true;
        rb.velocity = Vector2.zero; // Dừng di chuyển
        animator.SetBool("isRunning", false);
        animator.SetBool("isAttacking", true);

        while (fortress != null && fortress.CurrentHealth > 0)
        {
            fortress.TakeDamage(attackDamage);
            Debug.Log("Tường thành bị thiệt hại: " + attackDamage);

            yield return new WaitForSeconds(attackInterval);
        }

        animator.SetBool("isAttacking", false);
        isAttacking = false;
    }
}
