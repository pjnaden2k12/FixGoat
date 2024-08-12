using UnityEngine;
using System.Collections;

public class Enemy1Health : MonoBehaviour
{
    public int maxHealth = 100; // Máu tối đa của quái vật
    public int armor = 20; // Giáp của quái vật
    private int currentHealth; // Máu hiện tại của quái vật
    private Animator animator; // Animator của quái vật
    private bool isDead = false; // Trạng thái chết của quái vật
    public float moveSpeed = 0.5f; // Tốc độ di chuyển của quái vật
    public float attackRange = 1.0f; // Phạm vi để quái vật bắt đầu tấn công

    private Rigidbody rb;
    private bool isAttacking = false; // Trạng thái tấn công của quái vật

    void Start()
    {
        // Khởi tạo máu hiện tại bằng máu tối đa khi bắt đầu
        currentHealth = maxHealth;
        // Lấy Animator component
        animator = GetComponent<Animator>();

        rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.useGravity = false; // Tắt lực hấp dẫn để kiểm soát di chuyển bằng script
            rb.isKinematic = true; // Đặt Rigidbody thành kinematic để không bị ảnh hưởng bởi các lực vật lý
        }
    }

    void Update()
    {
    }

    // Hàm gọi để gây sát thương cho quái vật
    public void TakeDamage(int damageAmount)
    {
        if (isDead)
            return;

        // Tính toán sát thương sau khi trừ giáp
        int damageAfterArmor = Mathf.Max(damageAmount - armor, 0);

        currentHealth -= damageAfterArmor; // Giảm máu hiện tại
        Debug.Log("Damage taken: " + damageAmount + " (after armor: " + damageAfterArmor + ") Current Health: " + currentHealth);

        // Kiểm tra nếu máu bằng hoặc ít hơn 0
        if (currentHealth <= 0)
        {
            Die(); // Gọi hàm xử lý khi quái vật chết
        }
    }

    // Hàm xử lý khi quái vật chết
    void Die()
    {
        if (isDead)
            return;

        isDead = true;
        Debug.Log("Enemy died!");

        // Kích hoạt animation chết
        if (animator != null)
        {
            animator.SetTrigger("Die");
        }

        // Bắt đầu coroutine để làm mờ dần rồi biến mất
        StartCoroutine(FadeOutAndDestroy());
    }

    // Coroutine để làm mờ dần rồi biến mất
    IEnumerator FadeOutAndDestroy()
    {
        // Giả sử quái vật có Renderer với vật liệu hỗ trợ alpha blending
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            Color color = renderer.material.color;
            for (float t = 0; t < 1; t += Time.deltaTime / 2f) // Thời gian làm mờ dần là 2 giây
            {
                color.a = Mathf.Lerp(1, 0, t);
                renderer.material.color = color;
                yield return null;
            }
            color.a = 0;
            renderer.material.color = color;
        }

        // Tiêu diệt đối tượng sau khi làm mờ
        Destroy(gameObject);
    }

    void OnMouseDown()
    {
        // Giảm máu khi nhấp chuột
        TakeDamage(50); // Số máu bị giảm khi nhấp chuột
    }
}
