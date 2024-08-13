﻿using UnityEngine;

public class MonsterMovement : MonoBehaviour
{
    public float speed = 1f; // Tốc độ di chuyển của quái vật
    public float stopYPosition = -2.8f; // Vị trí Y tại đó quái vật sẽ dừng lại
    public Animator animator; // Animator của quái vật
    public float attackDamage = 10f; // Sát thương gây ra khi tấn công
    public string attackAnimationTrigger = "Attack"; // Tên trigger animation tấn công
    public float attackRadius = 0.5f; // Bán kính để kiểm tra va chạm với tường thành
    public LayerMask wallLayer; // Layer của tường thành

    private bool hasAttacked = false; // Để đảm bảo chỉ tấn công một lần

    void Update()
    {
        // Di chuyển quái vật theo hướng Y với tốc độ được xác định
        transform.position += Vector3.down * speed * Time.deltaTime;

        // Kiểm tra nếu quái vật đã đạt đến vị trí dừng
        if (transform.position.y <= stopYPosition)
        {
            // Nếu quái vật chưa tấn công
            if (!hasAttacked)
            {
                // Kích hoạt animation tấn công
                if (animator != null)
                {
                    animator.SetTrigger(attackAnimationTrigger);
                }

                // Gây sát thương lên tường thành
                DealDamageToWall();

                // Đánh dấu rằng quái vật đã tấn công
                hasAttacked = true;
            }

            // Đảm bảo quái vật không di chuyển qua vị trí dừng
            transform.position = new Vector3(transform.position.x, stopYPosition, transform.position.z);
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
