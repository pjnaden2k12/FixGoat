using UnityEngine;

public class TowerSlot : MonoBehaviour
{
    public GameObject currentTower; // Prefab tháp hiện tại trên ô
    public Transform towerPosition; // Vị trí mà prefab tháp sẽ được đặt

    private TowerManagerInGame towerManager;

    public void Initialize(TowerManagerInGame manager)
    {
        towerManager = manager;
    }

    public void PlaceTower(GameObject towerPrefab)
    {
        // Đặt tháp xuống ô
        currentTower = Instantiate(towerPrefab, towerPosition.position, Quaternion.identity, towerPosition);
    }

    public void RemoveTower()
    {
        // Xóa tháp khỏi ô
        if (currentTower != null)
        {
            Destroy(currentTower);
            currentTower = null;
        }
    }

    private void OnMouseDown()
    {
        if (currentTower == null)
        {
            // Nếu ô trống, mở panel chọn tháp
            towerManager.OpenSelectTowerPanel(this);
        }
        else
        {
            // Nếu đã có tháp, mở panel nâng cấp/xóa tháp
            towerManager.OpenUpgradeTowerPanel(this);
        }
    }
}
