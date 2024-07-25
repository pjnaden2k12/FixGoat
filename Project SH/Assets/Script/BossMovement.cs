using UnityEngine;

public class BossMovement : MonoBehaviour
{
    public float speed = 3.5f; // Tốc độ di chuyển của boss
    public Transform targetPoint; // Vị trí tường thành

    private Rigidbody2D rb;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (targetPoint != null)
        {
            // Tính toán hướng di chuyển
            Vector2 direction = (Vector2)targetPoint.position - rb.position;
            direction.Normalize();

            // Di chuyển boss theo hướng đó
            rb.velocity = direction * speed;

            // Cập nhật trạng thái animation
            if (direction.magnitude > 0.1f)
            {
                animator.SetBool("isRunning", true);
                animator.SetBool("isAttacking", false);
            }
            else
            {
                animator.SetBool("isRunning", false);
            }

            // Kiểm tra nếu boss đã đến điểm mục tiêu thì dừng lại và tấn công
            if (Vector2.Distance(rb.position, targetPoint.position) < 0.1f)
            {
                rb.velocity = Vector2.zero; // Dừng lại
                if (!GetComponent<BossHealth>().IsAttacking)
                {
                    StartCoroutine(GetComponent<BossHealth>().AttackTarget());
                }
            }
        }
    }
}
