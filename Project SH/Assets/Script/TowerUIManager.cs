using UnityEngine;
using UnityEngine.UI;
using TMPro; // Sử dụng TextMeshPro nếu bạn có TextMeshPro

public class TowerUIManager : MonoBehaviour
{
    public TowerManager towerManager; // Gán TowerManager
    public ResourceManager resourceManager; // Thêm ResourceManager để quản lý mảnh tháp
    public GameObject towerPanel;
    public GameObject towerButtonPrefab;
    public GameObject towerInfoPanel;
    public Image infoImage;
    public TextMeshProUGUI infoDamage;
    public TextMeshProUGUI infoAttackSpeed;
    public TextMeshProUGUI infoRange;
    public Button unlockButton;
    public Button closeButton; // Thêm biến cho nút đóng panel

    //private int selectedTowerIndex = -1;

    private void Start()
    {
        PopulateTowerList();
        towerInfoPanel.SetActive(false);
        closeButton.onClick.AddListener(CloseInfoPanel); // Gán sự kiện cho nút đóng
    }

    private void PopulateTowerList()
    {
        foreach (Transform child in towerPanel.transform)
        {
            Destroy(child.gameObject);
        }
        for (int i = 0; i < towerManager.towers.Length; i++)
        {
            TowerManager.TowerData towerData = towerManager.towers[i];
            GameObject towerButton = Instantiate(towerButtonPrefab, towerPanel.transform);

            // Tìm Image cho hình ảnh tháp trong prefab
            Image buttonImage = towerButton.transform.Find("TowerImage").GetComponent<Image>(); // Giả sử tên GameObject chứa Image là "TowerImage"
            TextMeshProUGUI[] texts = towerButton.GetComponentsInChildren<TextMeshProUGUI>();

            // Cập nhật hình ảnh tháp
            buttonImage.sprite = towerData.towerSprite;

            // Cập nhật các thông số tháp
            if (texts.Length > 0)
            {
                texts[0].text = $"ATK: {towerData.baseDamage}";
                texts[1].text = $"SPD: {towerData.baseAttackSpeed}";
                texts[2].text = $"RNG: {towerData.baseRange}";
            }

            // Gán sự kiện cho nút
            Button button = towerButton.GetComponent<Button>();
            int index = i;
            button.onClick.AddListener(() => OnTowerButtonClick(index));
        }

    }

    private void OnTowerButtonClick(int index)
    {
        TowerManager.TowerData towerData = towerManager.towers[index];

        if (towerData.isUnlocked)
        {
            // Hiển thị thông tin tháp
            infoImage.sprite = towerData.towerSprite;
            infoDamage.text = $"Damage: {towerData.baseDamage}";
            infoAttackSpeed.text = $"Speed: {towerData.baseAttackSpeed}";
            infoRange.text = $"Range: {towerData.baseRange}";
            unlockButton.gameObject.SetActive(false);
        }
        else
        {
            // Hiển thị nút mở khóa
            infoImage.sprite = null; // Hoặc đặt hình ảnh mặc định
            infoDamage.text = "Locked";
            infoAttackSpeed.text = "";
            infoRange.text = "";
            unlockButton.gameObject.SetActive(true);
            unlockButton.onClick.RemoveAllListeners();
            unlockButton.onClick.AddListener(() => UnlockTower(index));
        }

        towerInfoPanel.SetActive(true);
    }

    private void UnlockTower(int index)
    {
        int unlockCost = 100; // Chi phí mở khóa

        if (resourceManager.GetTowerPieces() >= unlockCost)
        {
            resourceManager.SpendTowerPieces(unlockCost);
            towerManager.UnlockTower(index);
            PopulateTowerList(); // Cập nhật danh sách tháp
            towerInfoPanel.SetActive(false);
        }
        else
        {
            Debug.Log("Not enough tower pieces to unlock this tower.");
        }
    }
    private void CloseInfoPanel()
    {
        towerInfoPanel.SetActive(false);
    }
}
