using UnityEngine;

public class Gear : MonoBehaviour
{
    public int amount = 1; // Số lượng bánh răng mà vật phẩm này cung cấp
    public float lifetime = 1f; // Thời gian tồn tại trước khi tự hủy

    void Start()
    {
        // Thêm bánh răng vào tổng số
        GearManager.Instance.AddGears(amount);

        // Gọi phương thức DestroyAfterTime sau một khoảng thời gian
        Invoke("DestroyAfterTime", lifetime);
    }

    void DestroyAfterTime()
    {
        // Hủy đối tượng bánh răng sau thời gian tồn tại
        Destroy(gameObject);
    }
}
