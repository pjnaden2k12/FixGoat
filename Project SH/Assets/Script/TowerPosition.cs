using UnityEngine;

public class TowerPlacementPoint : MonoBehaviour
{
    public int positionIndex; // Chỉ số của vị trí (0, 1, 2, 3)

    private void OnMouseDown()
    {
        // Kiểm tra sự tồn tại của TowerManager và gọi phương thức để hiển thị bảng
        if (TowerManager.Instance != null)
        {
            TowerManager.Instance.ShowTowerPanel(positionIndex, gameObject);
        }
        else
        {
            Debug.LogError("TowerManager instance is not set.");
        }
    }
}
