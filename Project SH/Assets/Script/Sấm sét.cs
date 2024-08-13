using UnityEngine;

public class LightningBolt : MonoBehaviour
{
    public float speed = 10f; // Tốc độ di chuyển của tia sét
    public float lifetime = 0.5f; // Thời gian tồn tại của tia sét
    private float damage; // Sát thương của đạn

    private Transform target; // Mục tiêu của tia sét

    void Start()
    {
        Destroy(gameObject, lifetime); // Hủy tia sét sau thời gian tồn tại
       
    }
    public void SetDamage(float samDamage)
    {
        damage = samDamage;
    }

    
    public void Initialize(Transform target)
    {
        this.target = target;
        // Đặt vị trí bắt đầu của tia sét ở trên trời
        transform.position = new Vector3(target.position.x, 10f, target.position.z); // Điều chỉnh theo nhu cầu
    }

    void Update()
    {
        if (target != null)
        {
            // Di chuyển tia sét từ trên trời xuống mục tiêu
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

            // Nếu đã đến mục tiêu, hủy đối tượng tia sét
            if (transform.position == target.position)
            {
                //Gây sát thương cho mục tiêu
                if (target.CompareTag("Enemy"))
                {
                    target.GetComponent<Enemy1Health>().TakeDamage(damage);
                    Destroy(gameObject, lifetime);
                }
                else if (target.CompareTag("Boss"))
                {
                    target.GetComponent<capnhatthanhmaubosshiepsi>().TakeDamage(damage);
                    Destroy(gameObject, lifetime);
                }
                // Hủy đạn sau khi va chạm
                //Destroy(gameObject, lifetime);
                
            }
        }
    }
}
