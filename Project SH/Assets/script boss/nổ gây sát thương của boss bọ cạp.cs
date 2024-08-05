using System.Collections;
using UnityEngine;

public class ExplosionEffect : MonoBehaviour
{
    public float explosionRadius = 5f; // Bán kính sát thương của hiệu ứng nổ
    public float explosionDamage = 100f; // Sát thương của hiệu ứng nổ
    public float delayedDamage = 500f; // Sát thương lớn hơn gây ra từ khoảng cách cụ thể
    public float delayBeforeDamage = 0.5f; // Thời gian trì hoãn trước khi gây sát thương
    public float damageDistance = 2f; // Khoảng cách cụ thể để gây sát thương lớn hơn

    private void Start()
    {
        StartCoroutine(ApplyExplosionDamage());
    }

    private IEnumerator ApplyExplosionDamage()
    {
        yield return new WaitForSeconds(delayBeforeDamage);

        // Gây sát thương cho các đối tượng trong bán kính hiệu ứng nổ
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach (Collider2D collider in colliders)
        {
            Health healthComponent = collider.GetComponent<Health>();
            if (healthComponent != null)
            {
                // Tính khoảng cách từ vị trí nổ đến đối tượng
                float distance = Vector2.Distance(transform.position, collider.transform.position);

                if (distance <= damageDistance)
                {
                    // Gây sát thương lớn hơn nếu trong khoảng cách cụ thể
                    healthComponent.TakeDamage(delayedDamage);
                }
                else
                {
                    // Gây sát thương bình thường
                    healthComponent.TakeDamage(explosionDamage);
                }
            }
        }

        // Xóa hiệu ứng nổ sau khi hoàn tất
        Destroy(gameObject);
    }
}
