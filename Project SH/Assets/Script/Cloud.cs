using UnityEngine;

public class Cloud : MonoBehaviour
{
    public float speed = 1.0f; // Tốc độ di chuyển của đám mây

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        // Di chuyển đám mây theo chiều ngang
        transform.position += Vector3.left * speed * Time.deltaTime;

        // Khi đám mây ra ngoài màn hình, di chuyển về điểm bắt đầu
        if (transform.position.x < -4) // Thay đổi giá trị này nếu cần
        {
            transform.position = startPosition;
        }
    }
}
