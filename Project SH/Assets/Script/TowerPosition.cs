using UnityEngine;

public class TowerPlacementPoint : MonoBehaviour
{
    public int positionIndex; // Chỉ số của vị trí (0, 1, 2, 3)

    private void OnMouseDown()
    {
        // Gọi phương thức trong TowerManager để hiển thị bảng tương ứng với vị trí này
        TowerManager.Instance.ShowTowerPanel(positionIndex, gameObject);
    }
}
