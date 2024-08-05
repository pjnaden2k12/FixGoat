using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 2f; // Tốc độ di chuyển của quái vật

    void Update()
    {
        Move();
    }

    void Move()
    {
        // Di chuyển quái vật từ trên xuống dưới
        transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
    }
}
