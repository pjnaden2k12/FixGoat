using UnityEngine;

public class Attack : MonoBehaviour
{
    public Animator animator; // Animator của nhân vật
    public string attackAnimationName = "Attack"; // Tên của animation đòn tấn công
    public string targetTag = "Enemy"; // Tag của đối tượng mục tiêu
    public int damageAmount = 10; // Số lượng sát thương gây ra mỗi lần tấn công

    void Start()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            // Kích hoạt animation đòn tấn công
            animator.SetTrigger(attackAnimationName);

            // Xử lý va chạm với đối tượng mục tiêu (ví dụ: gây sát thương)
            HandleAttack(other.gameObject);
        }
    }

    void HandleAttack(GameObject target)
    {
        // Logic xử lý tấn công (ví dụ: giảm HP của đối tượng mục tiêu)
        EnemyHealth targetHealth = target.GetComponent<EnemyHealth>();
        if (targetHealth != null)
        {
            targetHealth.TakeDamage(damageAmount); // Gây sát thương cho quái vật
        }
    }
}
