using UnityEngine;

public class BGoblinMove : MonoBehaviour
{
    public float speed = 0.2f;  // Tốc độ di chuyển (đơn vị mỗi giây)
    public Transform target;  // Điểm đến mà đối tượng sẽ di chuyển tới
    private Animator animator;  // Animator của đối tượng
    private bool hasReachedTarget = false;  // Kiểm tra nếu đối tượng đã đến vị trí đích
    private Enemy1Health enemyHealth;  // Hệ thống máu của quái

    void Start()
    {
        // Lấy component Animator và EnemyHealth của đối tượng
        animator = GetComponent<Animator>();
        enemyHealth = GetComponent<Enemy1Health>();
    }

    void Update()
    {
        if (target != null && !hasReachedTarget)
        {
            // Tính toán khoảng cách di chuyển trong mỗi khung hình
            float step = speed * Time.deltaTime;

            // Di chuyển đối tượng đến vị trí đích
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);

            // Kiểm tra nếu đối tượng đã đến vị trí đích
            if (Vector3.Distance(transform.position, target.position) < 0.001f)
            {
                // Đối tượng đã đến vị trí đích, dừng lại
                hasReachedTarget = true;

                // Kích hoạt animation tấn công
                if (animator != null)
                {
                    animator.SetTrigger("Attack");
                }
            }
        }
    }

    // Hàm để nhận sát thương từ bên ngoài
    public void ReceiveDamage(int damage)
    {
        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(damage);
        }
    }
}
