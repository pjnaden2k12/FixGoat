using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TowerUpgradeManager : MonoBehaviour
{
    public GameObject upgradeTowerPanel; // Panel nâng cấp tháp
    public Button upgradeButton; // Nút nâng cấp tháp
    public Button removeTowerButton; // Nút xóa tháp
    public TextMeshProUGUI towerInfoText; // Text hiển thị thông tin tháp

    private Tower currentTower; // Tháp hiện tại đang được nâng cấp

    public static TowerUpgradeManager Instance; // Để truy cập từ TowerManagerInGame

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        // Ẩn panel nâng cấp tháp khi bắt đầu
        upgradeTowerPanel.SetActive(false);

        // Thêm sự kiện cho các nút
        if (upgradeButton != null)
        {
            upgradeButton.onClick.AddListener(UpgradeTower);
        }
        if (removeTowerButton != null)
        {
            removeTowerButton.onClick.AddListener(RemoveTower);
        }
    }

    public void ShowUpgradePanel(Tower tower)
    {
        currentTower = tower;
        if (currentTower != null)
        {
            // Cập nhật thông tin tháp
            UpdateTowerInfo();

            // Hiển thị panel nâng cấp tháp
            upgradeTowerPanel.SetActive(true);
        }
    }

    private void UpdateTowerInfo()
    {
        // Cập nhật thông tin tháp vào TextMeshProUGUI
        if (towerInfoText != null && currentTower != null)
        {
            towerInfoText.text = $"Tower Level: {currentTower.level}\n" +
                                  $"Damage: {currentTower.GetDamage()}\n" +
                                  $"Attack Speed: {currentTower.GetAttackSpeed()}\n" +
                                  $"Cost: {currentTower.upgradeCost} Gear";

            // Cập nhật trạng thái nút nâng cấp
            UpdateUpgradeButtonState();
        }
    }

    private void UpdateUpgradeButtonState()
    {
        if (upgradeButton != null && currentTower != null)
        {
            // Kiểm tra xem tháp đã đạt đến cấp tối đa chưa
            bool isMaxLevel = currentTower.level >= currentTower.maxLevel; // Giả định bạn có thuộc tính maxLevel
            upgradeButton.interactable = !isMaxLevel;
            upgradeButton.GetComponent<Image>().color = isMaxLevel ? Color.gray : Color.white; // Thay đổi màu sắc nút
        }
    }

    private void UpgradeTower()
    {
        if (currentTower != null)
        {
            // Thực hiện nâng cấp tháp
            currentTower.Upgrade();
            // Cập nhật thông tin tháp sau khi nâng cấp
            UpdateTowerInfo();
        }
    }

    private void RemoveTower()
    {
        if (currentTower != null)
        {
            TowerManagerInGame.Instance.RemoveTower(currentTower);
            CloseUpgradePanel(); // Đóng panel nâng cấp sau khi xóa tháp
        }
    }

    private void CloseUpgradePanel()
    {
        // Ẩn panel nâng cấp tháp
        upgradeTowerPanel.SetActive(false);
    }
}
