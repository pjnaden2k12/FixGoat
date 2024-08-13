using UnityEngine;

public class HealerMove : MonoBehaviour
{
    // Tốc độ di chuyển của quái
    public float speed = 5.0f;
    // Đối tượng mục tiêu
    public Transform targetPoint;

    void Update()
    {
        if (targetPoint != null)
        {
            // Di chuyển quái đến vị trí của targetPoint
            transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);

            // Kiểm tra nếu quái đến gần vị trí mục tiêu (với một khoảng sai số nhỏ)
            if (Vector3.Distance(transform.position, targetPoint.position) < 0.1f)
            {
                // Dừng di chuyển bằng cách vô hiệu hóa script này
                enabled = false;
            }
        }
    }
}
