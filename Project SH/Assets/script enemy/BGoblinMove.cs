using UnityEngine;
using System.Collections;

public class BGoblinMove : MonoBehaviour
{
    public float speed = 3.5f; // Tốc độ di chuyển của địch
    public Transform targetPoint; // Điểm mục tiêu, như là tường hoặc người chơi
    public float attackDamage = 20f; // Sát thương gây ra khi tấn công
    public float attackInterval = 2f; // Thời gian giữa các lần tấn công

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
        if (targetPoint == null) return;

        // Tính toán hướng di chuyển và di chuyển địch
        Vector2 direction = (Vector2)targetPoint.position - rb.position;
        float distance = direction.magnitude;
        direction.Normalize();

        if (distance > 0.1f && !isAttacking)
        {
            rb.velocity = direction * speed;
            animator.SetBool("isRunning", true);
            animator.SetBool("isAttacking", false); // Đảm bảo tắt hoạt ảnh tấn công nếu đang chạy
        }
        else
        {
            rb.velocity = Vector2.zero;
            animator.SetBool("isRunning", false);
        }

        // Nếu địch đến điểm mục tiêu thì dừng lại và tấn công
        if (distance <= 0.1f && !isAttacking)
        {
            StartCoroutine(AttackTarget());
        }
    }

    private IEnumerator AttackTarget()
    {
        isAttacking = true;

        // Đặt trạng thái tấn công và kích hoạt hoạt ảnh tấn công
        animator.SetBool("isAttacking", true);
        Debug.Log("Quái tấn công tường thành!");

        // Gây sát thương lên mục tiêu (cần thêm logic để lấy thành phần sức khỏe của mục tiêu)
        // Ví dụ: targetPoint.GetComponent<Health>().TakeDamage(attackDamage);

        // Chờ hoạt ảnh tấn công hoàn tất (đặt thời gian phù hợp với hoạt ảnh của bạn)
        yield return new WaitForSeconds(1f);

        // Tắt trạng thái tấn công và đợi trước khi tấn công lần tiếp theo
        animator.SetBool("isAttacking", false);
        yield return new WaitForSeconds(attackInterval);

        isAttacking = false;
    }
}
