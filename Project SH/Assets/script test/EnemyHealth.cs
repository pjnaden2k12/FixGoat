using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100; // Máu tối đa của quái vật
    private int currentHealth; // Máu hiện tại của quái vật
    private Animator animator; // Animator của quái vật
    private bool isDead = false; // Trạng thái chết của quái vật
    public float moveSpeed = 2f; // Tốc độ di chuyển của quái vật

    void Start()
    {
        // Khởi tạo máu hiện tại bằng máu tối đa khi bắt đầu
        currentHealth = maxHealth;
        // Lấy Animator component
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!isDead)
        {
            Move();
        }
    }

    // Hàm gọi để gây sát thương cho quái vật
    public void TakeDamage(int damageAmount)
    {
        if (isDead)
            return;

        currentHealth -= damageAmount; // Giảm máu hiện tại
        Debug.Log("Damage taken: " + damageAmount + " Current Health: " + currentHealth);

        // Kiểm tra nếu máu bằng hoặc ít hơn 0
        if (currentHealth <= 0)
        {
            Die(); // Gọi hàm xử lý khi quái vật chết
        }
    }

    // Hàm xử lý khi quái vật chết
    void Die()
    {
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
        TakeDamage(10); // Số máu bị giảm khi nhấp chuột
    }

    void Move()
    {
        // Di chuyển quái vật từ trên xuống dưới
        transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
    }
}
