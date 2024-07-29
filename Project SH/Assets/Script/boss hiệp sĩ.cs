using UnityEngine;

public class BossMovement : MonoBehaviour
{
    public float speed = 3.5f; // Tốc độ di chuyển của boss
    public Transform targetPoint; // Điểm mà boss sẽ di chuyển tới
    private Rigidbody2D rb;
   
  

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
       

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

            // Kiểm tra nếu boss đã đến điểm mục tiêu thì dừng lại và tấn công
            if (Vector2.Distance(rb.position, targetPoint.position) < 0.1f)
            {
                rb.velocity = Vector2.zero; // Dừng lại
                AttackTarget();
            }
        }
    }

    public void MoveToTarget(Transform targetPoint)
    {
        targetPoint = targetPoint;
    }

    void AttackTarget()
    {
        // Xử lý khi boss tấn công tường thành
        Debug.Log("Boss tấn công tường thành!");
        // Bạn có thể thêm các hành động tấn công tại đây
    }
}