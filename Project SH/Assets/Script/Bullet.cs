using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f; // Tốc độ của đạn
    private float damage; // Sát thương của đạn

    private Transform target;

    public void SetDamage(float bulletDamage)
    {
        damage = bulletDamage;
    }

    public void Seek(GameObject _target)
    {
        target = _target.transform;
    }

    void Update()
    {
        if (target == null)
        {
            // Hủy đạn nếu không còn mục tiêu
            Destroy(gameObject);
            return;
        }

        // Tính hướng di chuyển tới mục tiêu
        Vector3 direction = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        // Kiểm tra nếu đạn tới gần mục tiêu
        if (direction.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        // Di chuyển đạn về phía mục tiêu
        transform.Translate(direction.normalized * distanceThisFrame, Space.World);
    }

    void HitTarget()
    {
        //Gây sát thương cho mục tiêu
        if (target.CompareTag("Enemy"))
        {
            target.GetComponent<Enemy1Health>().TakeDamage(damage);
        }
        else if (target.CompareTag("Boss"))
        {
            target.GetComponent<capnhatthanhmaubosshiepsi>().TakeDamage(damage);

        }
        // Hủy đạn sau khi va chạm
        Destroy(gameObject);
    }
}
