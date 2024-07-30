using UnityEngine;
using System.Collections;

public class enemymove : MonoBehaviour
{
    public float speed = 3.5f; // Tốc độ di chuyển của boss
    public Transform targetPoint; // Vị trí tường thành
    public float attackDamage = 20f; // Sát thương của boss khi tấn công
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

        // Tính toán hướng di chuyển và di chuyển boss
        Vector2 direction = (Vector2)targetPoint.position - rb.position;
        float distance = direction.magnitude;
        direction.Normalize();

        if (distance > 0.1f && !isAttacking)
        {
            rb.velocity = direction * speed;
            animator.SetBool("isRunning", true);
        }
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
        Debug.Log("Quái tấn công tường thành!");

        // Gây sát thương lên tường thành
     

        // Chờ animation tấn công hoàn tất (ví dụ 1 giây)
        yield return new WaitForSeconds(1f);

        // Kết thúc animation tấn công và chờ trước khi tấn công lần tiếp theo
        animator.SetBool("isAttacking", false);
        yield return new WaitForSeconds(attackInterval);

        isAttacking = false;
    }
}
