using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 0.5f;

    void Update()
    {
        // Di chuyển quái vật từ trên xuống dưới theo trục Y
        transform.Translate(Vector2.down * speed * Time.deltaTime);

        // Kiểm tra nếu quái vật di chuyển ra khỏi màn hình và xoá nó để tiết kiệm tài nguyên
        if (transform.position.y < -10f)  // Điều chỉnh giá trị này dựa trên vị trí màn hình của bạn
        {
            Destroy(gameObject);
        }
    }
}
