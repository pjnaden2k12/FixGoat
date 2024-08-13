using UnityEngine;

public class MonsterMovement : MonoBehaviour
{
    public float speed = 1f; // Tốc độ di chuyển của quái vật
    public float stopYPosition = -2.8f; // Vị trí Y tại đó quái vật sẽ dừng lại
    public Animator animator; // Animator của quái vật
    public float attackDamage = 10f; // Sát thương gây ra khi tấn công
    public string attackAnimationTrigger = "Attack"; // Tên trigger animation tấn công
    public float attackRadius = 0.5f; // Bán kính để kiểm tra va chạm với tường thành
    public LayerMask wallLayer; // Layer của tường thành
    public float attackInterval = 1.0f; // Khoảng thời gian giữa các lần tấn công

    private float lastAttackTime = 0f; // Thời gian của lần tấn công cuối cùng

    void Update()
    {
        // Di chuyển quái vật theo hướng Y với tốc độ được xác định
        transform.position += Vector3.down * speed * Time.deltaTime;

        // Kiểm tra nếu quái vật đã đạt đến vị trí dừng
        if (transform.position.y <= stopYPosition)
        {
            // Đảm bảo quái vật không di chuyển qua vị trí dừng
            transform.position = new Vector3(transform.position.x, stopYPosition, transform.position.z);

            // Kiểm tra nếu đã đến thời điểm tấn công tiếp theo
            if (Time.time > lastAttackTime + attackInterval)
            {
                // Kích hoạt animation tấn công
                if (animator != null)
                {
                    animator.SetTrigger(attackAnimationTrigger);
                }

                // Gây sát thương lên tường thành
                DealDamageToWall();

                // Cập nhật thời gian của lần tấn công cuối cùng
                lastAttackTime = Time.time;
            }
        }
    }

    void DealDamageToWall()
    {
        // Tìm tường thành trong bán kính xung quanh quái vật và gây sát thương lên nó
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, attackRadius, wallLayer);

        foreach (Collider2D hitCollider in hitColliders)
        {
            FortressHealth wall = hitCollider.GetComponent<FortressHealth>();
            if (wall != null)
            {
                wall.TakeDamage(attackDamage);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Hiển thị bán kính tấn công trong chế độ xem Scene
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}
