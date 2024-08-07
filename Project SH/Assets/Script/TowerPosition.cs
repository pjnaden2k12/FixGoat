using UnityEngine;

public class TowerPlacementPoint : MonoBehaviour
{
    public int positionIndex; // Chỉ số của vị trí (0, 1, 2, 3)

    private void OnMouseDown()
    {
        if (TowerManagerInGame.Instance != null)
        {
            Tower existingTower = GetComponentInChildren<Tower>();
            if (existingTower == null)
            {
                // Vị trí chưa có tháp, hiển thị bảng mua tháp
                TowerManagerInGame.Instance.DisplayBuyPanel(positionIndex, gameObject);
            }
            else
            {
                // Vị trí đã có tháp, hiển thị bảng nâng cấp tháp
                TowerManagerInGame.Instance.DisplayUpgradePanel(existingTower);
            }
        }
        else
        {
            Debug.LogError("TowerManagerInGame instance is not set.");
        }
    }
}
