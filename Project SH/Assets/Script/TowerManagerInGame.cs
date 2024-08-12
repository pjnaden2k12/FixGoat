using UnityEngine;

public class TowerManagerInGame : MonoBehaviour
{
    public GameObject selectTowerPanel; // Panel chọn tháp
    public GameObject upgradeTowerPanel; // Panel nâng cấp/xóa tháp
    public GameObject[] towerPrefabs; // Các prefab của tháp với ID tương ứng
    public TowerSlot[] towerSlots; // Các ô trên tường thành

    private TowerSlot selectedSlot;

    private void Start()
    {
        foreach (TowerSlot slot in towerSlots)
        {
            slot.Initialize(this);
        }
    }

    public void OpenSelectTowerPanel(TowerSlot slot)
    {
        selectedSlot = slot;
        selectTowerPanel.SetActive(true);
        // Cập nhật panel chọn tháp, làm mờ những tháp chưa mở khóa
    }

    public void OpenUpgradeTowerPanel(TowerSlot slot)
    {
        selectedSlot = slot;
        upgradeTowerPanel.SetActive(true);
        // Cập nhật panel nâng cấp/xóa tháp với thông tin tháp hiện tại
    }

    public void SelectTowerById(int towerId)
    {
        // Gọi khi người chơi chọn tháp từ panel với ID cụ thể
        GameObject selectedTowerPrefab = towerPrefabs[towerId];
        selectedSlot.PlaceTower(selectedTowerPrefab);
        selectTowerPanel.SetActive(false);
    }

    public void UpgradeTower()
    {
        // Gọi khi người chơi nâng cấp tháp (có thể thay đổi prefab hoặc tăng cấp độ)
        upgradeTowerPanel.SetActive(false);
    }

    public void RemoveTower()
    {
        selectedSlot.RemoveTower();
        upgradeTowerPanel.SetActive(false);
    }
}
