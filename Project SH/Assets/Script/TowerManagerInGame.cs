using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TowerManagerInGame : MonoBehaviour
{
    public static TowerManagerInGame Instance;

    public GameObject towerButtonPrefab; // Prefab cho nút tháp
    public Transform buyPanelContent; // Vị trí cha chứa các nút tháp trong panel mua
    public GameObject buyPanel; // Panel mua tháp
    public GameObject upgradePanel; // Panel nâng cấp tháp
    public TextMeshProUGUI upgradeInfoText; // Text hiển thị thông tin nâng cấp tháp
    public Button removeButton; // Nút xóa tháp
    public Button exitButton; // Nút thoát khỏi bảng
    public Button nextPanelButton; // Nút để chuyển đến panel tiếp theo
    public Button prevPanelButton; // Nút để quay lại panel trước đó
    public Button upgradeButton; // Nút nâng cấp tháp

    private GameObject currentTowerPosition; // Vị trí hiện tại để đặt tháp
    private Tower currentTower; // Tháp hiện tại (nếu có)
    private int currentPositionIndex; // Chỉ số của vị trí đang được xử lý
    private int currentPage = 0; // Trang hiện tại
    private const int towersPerPage = 3; // Số tháp hiển thị trên mỗi trang

    void Awake()
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

    void Start()
    {
        // Đảm bảo bảng mua và nâng cấp tháp không hiển thị khi bắt đầu
        buyPanel.SetActive(false);
        upgradePanel.SetActive(false);

        // Đăng ký sự kiện cho các nút
        removeButton.onClick.AddListener(RemoveTower);
        exitButton.onClick.AddListener(ClosePanels);
        nextPanelButton.onClick.AddListener(NextPage);
        prevPanelButton.onClick.AddListener(PrevPage);
        upgradeButton.onClick.AddListener(UpgradeTower);
    }

    // Hiển thị các tháp có thể mua trong panel mua
    public void DisplayBuyPanel(int positionIndex, GameObject towerPosition)
    {
        currentTowerPosition = towerPosition;
        currentPositionIndex = positionIndex;
        currentPage = 0; // Reset trang khi mở bảng mua

        // Xóa tất cả các nút tháp hiện có trong panel mua
        foreach (Transform child in buyPanelContent)
        {
            Destroy(child.gameObject);
        }

        // Lấy danh sách các tháp đã mở khóa từ TowerManager
        List<Tower> unlockedTowers = TowerManager.Instance.GetUnlockedTowers();
        int totalPages = Mathf.CeilToInt((float)unlockedTowers.Count / towersPerPage);

        // Đảm bảo chỉ số trang hợp lệ
        if (currentPage >= totalPages)
        {
            currentPage = totalPages - 1;
        }
        else if (currentPage < 0)
        {
            currentPage = 0;
        }

        // Hiển thị các tháp cho trang hiện tại
        int startIndex = currentPage * towersPerPage;
        int endIndex = Mathf.Min(startIndex + towersPerPage, unlockedTowers.Count);

        for (int i = startIndex; i < endIndex; i++)
        {
            Tower tower = unlockedTowers[i];
            GameObject buttonObject = Instantiate(towerButtonPrefab, buyPanelContent);
            Button button = buttonObject.GetComponent<Button>();
            Image towerImage = buttonObject.GetComponentInChildren<Image>();
            TMP_Text buttonText = buttonObject.GetComponentInChildren<TMP_Text>();

            // Cập nhật hình ảnh và thông tin của nút tháp
            towerImage.sprite = tower.GetComponent<SpriteRenderer>().sprite; // Hoặc sprite của bạn
            buttonText.text = $"Buy (50 gears)";

            // Đăng ký sự kiện cho nút mua
            button.onClick.AddListener(() => BuyTower(tower));
        }

        // Cập nhật trạng thái nút điều hướng
        prevPanelButton.gameObject.SetActive(currentPage > 0);
        nextPanelButton.gameObject.SetActive(currentPage < totalPages - 1);

        buyPanel.SetActive(true);
    }

    // Chuyển đến trang tiếp theo
    private void NextPage()
    {
        int totalPages = Mathf.CeilToInt((float)TowerManager.Instance.GetUnlockedTowers().Count / towersPerPage);
        if (currentPage < totalPages - 1)
        {
            currentPage++;
            DisplayBuyPanel(currentPositionIndex, currentTowerPosition);
        }
    }

    // Quay lại trang trước đó
    private void PrevPage()
    {
        if (currentPage > 0)
        {
            currentPage--;
            DisplayBuyPanel(currentPositionIndex, currentTowerPosition);
        }
    }

    // Mua tháp
    void BuyTower(Tower tower)
    {
        if (GearManager.Instance.SpendGears(50))
        {
            Instantiate(tower.gameObject, currentTowerPosition.transform.position, Quaternion.identity, currentTowerPosition.transform);
            ClosePanels(); // Đóng bảng và tiếp tục trò chơi
        }
    }

    // Hiển thị panel nâng cấp tháp
    public void DisplayUpgradePanel(Tower tower)
    {
        currentTower = tower;
     

        if (tower != null)
        {
            Tower towerScript = tower.GetComponent<Tower>();
            if (towerScript != null)
            {
                upgradeInfoText.text = $"Level {towerScript.level} - Cost: {50 * (towerScript.level + 1)} gears";
            }
        }

        upgradePanel.SetActive(true);
    }

    // Nâng cấp tháp
    public void UpgradeTower()
    {
        if (currentTower != null)
        {
            // Không cần gọi GetComponent<Tower>() nữa
            if (GearManager.Instance.SpendGears(50 * (currentTower.level + 1))) // Tính giá nâng cấp dựa trên level hiện tại
            {
                currentTower.Upgrade();
                upgradeInfoText.text = $"Upgraded to level {currentTower.level}";
            }
        }
    }


    void RemoveTower()
    {
        if (currentTower != null)
        {
            Destroy(currentTower.gameObject); // Xóa GameObject liên kết với Tower
            currentTower = null; // Đặt lại biến currentTower
            ClosePanels(); // Đóng bảng và tiếp tục trò chơi
        }
    }


    // Đóng tất cả các panel
    void ClosePanels()
    {
        buyPanel.SetActive(false);
        upgradePanel.SetActive(false);
    }
}
