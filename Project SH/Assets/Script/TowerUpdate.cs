using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TowerUpgradeManager : MonoBehaviour
{
    public static TowerUpgradeManager Instance { get; private set; }

    [SerializeField] private GameObject upgradePanel; // Bảng nâng cấp tháp
    [SerializeField] private TextMeshProUGUI towerStatsText; // Văn bản hiển thị chỉ số tháp
    [SerializeField] private Button upgradeButton; // Nút nâng cấp
    [SerializeField] private Button closeButton; // Nút đóng bảng

    private Tower selectedTower;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        upgradeButton.onClick.AddListener(OnUpgradeButtonClicked);
        closeButton.onClick.AddListener(CloseUpgradePanel);
    }

    // Mở bảng nâng cấp và hiển thị thông tin tháp
    public void OpenUpgradePanel(Tower tower)
    {
        selectedTower = tower;
        UpdateTowerStatsText();
        upgradePanel.SetActive(true);
    }

    // Cập nhật văn bản hiển thị chỉ số tháp và trạng thái nút nâng cấp
    private void UpdateTowerStatsText()
    {
        if (selectedTower != null)
        {
            towerStatsText.text = $"Level: {selectedTower.level}\n" +
                                  $"Damage: {selectedTower.GetDamage()}\n" +
                                  $"Attack Speed: {selectedTower.GetAttackSpeed()}\n" +
                                  $"Range: {selectedTower.GetRange()}\n" +
                                  $"Upgrade Cost: {50 * selectedTower.level} Gears";

            // Kiểm tra cấp độ và cập nhật trạng thái nút nâng cấp
            if (selectedTower.level >= 3)
            {
                upgradeButton.interactable = false;
                ColorBlock colors = upgradeButton.colors;
                colors.normalColor = Color.gray; // Màu sắc mờ khi không thể nâng cấp
                upgradeButton.colors = colors;
            }
            else
            {
                upgradeButton.interactable = true;
                ColorBlock colors = upgradeButton.colors;
                colors.normalColor = Color.white; // Màu sắc bình thường khi có thể nâng cấp
                upgradeButton.colors = colors;
            }
        }
    }

    // Xử lý khi nhấn nút nâng cấp
    private void OnUpgradeButtonClicked()
    {
        if (selectedTower != null)
        {
            selectedTower.Upgrade();
            UpdateTowerStatsText(); // Cập nhật lại thông số và trạng thái nút sau khi nâng cấp
        }
    }

    // Đóng bảng nâng cấp
    public void CloseUpgradePanel()
    {
        upgradePanel.SetActive(false);
    }
}
