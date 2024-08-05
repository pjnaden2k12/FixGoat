using UnityEngine;

public class DisappearAfterTime : MonoBehaviour
{
    // Thời gian chờ trước khi đối tượng biến mất
    public float timeToDisappear = 5.0f;

    // Start được gọi một lần khi script được kích hoạt
    void Start()
    {
        // Gọi hàm Destroy sau khi đợi thời gian chỉ định
        Destroy(gameObject, timeToDisappear);
    }
}
