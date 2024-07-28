using UnityEngine;
<<<<<<< HEAD
using System.Collections;

public class BossMovement : MonoBehaviour
{
    public float speed = 3.5f; // Tốc độ di chuyển của boss
    public Transform targetPoint; // Vị trí tường thành
    public float attackDamage = 20f; // Sát thương của boss khi tấn công
    public float attackInterval = 2f; // Thời gian giữa các lần tấn công
=======
using System.Collections; // Đảm bảo thêm namespace này

public class BossMovement : MonoBehaviour
{
    public float speed = 3.5f;
    public Transform targetPoint;
    public float attackDamage = 20f;
    public float attackInterval = 2f;
>>>>>>> main

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
<<<<<<< HEAD
        if (targetPoint == null) return;

        // Tính toán hướng di chuyển và di chuyển boss
        Vector2 direction = (Vector2)targetPoint.position - rb.position;
        float distance = direction.magnitude;
        direction.Normalize();

        if (distance > 0.1f && !isAttacking)
=======
        // Di chuyển boss về phía targetPoint
        Vector2 direction = (Vector2)targetPoint.position - rb.position;
        direction.Normalize();

        if (!isAttacking)
>>>>>>> main
        {
            rb.velocity = direction * speed;
            animator.SetBool("isRunning", true);
        }
<<<<<<< HEAD
        else
        {
            rb.velocity = Vector2.zero;
            animator.SetBool("isRunning", false);
            animator.SetBool("isAttacking", false);
        }

        // Kiểm tra nếu boss đã đến điểm mục tiêu thì dừng lại và tấn công
        if (distance < 0.1f && !isAttacking)
        {
            StartCoroutine(AttackTarget());
        }
    }

    private IEnumerator AttackTarget()
    {
        isAttacking = true;

        // Đặt trạng thái tấn công
        animator.SetBool("isAttacking", true);

        // Thực hiện animation tấn công tại đây
        Debug.Log("Boss tấn công tường thành!");

        // Gây sát thương lên tường thành
        var fortress = targetPoint.GetComponent<FortressHealth>();
        if (fortress != null)
        {
            fortress.TakeDamage(attackDamage);
            Debug.Log("Tường thành bị thiệt hại: " + attackDamage);
        }

        // Chờ animation tấn công hoàn tất (ví dụ 1 giây)
        yield return new WaitForSeconds(1f);

        // Kết thúc animation tấn công và chờ trước khi tấn công lần tiếp theo
        animator.SetBool("isAttacking", false);
        yield return new WaitForSeconds(attackInterval);

=======
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
>>>>>>> main
        isAttacking = false;
    }
}
