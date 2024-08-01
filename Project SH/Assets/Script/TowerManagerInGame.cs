using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TowerManagerInGame : MonoBehaviour
{
    public static TowerManagerInGame Instance;

    public GameObject towerPrefab; // Prefab của tháp
    public GameObject buyPanel; // Panel mua tháp
    public GameObject upgradePanel; // Panel nâng cấp tháp
    public TextMeshProUGUI buyPriceText; // Text hiển thị giá tháp
    public TextMeshProUGUI upgradeInfoText; // Text hiển thị thông tin nâng cấp
    public Button buyButton; // Nút mua tháp
    public Button upgradeButton; // Nút nâng cấp tháp
    public Button removeButton; // Nút xóa tháp
    public Button exitButton; // Nút thoát khỏi bảng

    private GameObject currentTowerPosition; // Vị trí hiện tại để đặt tháp
    private GameObject currentTower; // Tháp hiện tại (nếu có)
    private int currentPositionIndex; // Chỉ số của vị trí đang được xử lý

    private const int towerCost = 50; // Giá của tháp

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
        buyButton.onClick.AddListener(BuyTower);
        upgradeButton.onClick.AddListener(UpgradeTower);
        removeButton.onClick.AddListener(RemoveTower);
        exitButton.onClick.AddListener(ClosePanels);
    }

    // Hiển thị bảng mua hoặc nâng cấp tháp dựa trên vị trí và chỉ số
    public void ShowTowerPanel(int positionIndex, GameObject position)
    {
        Time.timeScale = 0; // Tạm dừng trò chơi

        currentPositionIndex = positionIndex;
        currentTowerPosition = position;
        currentTower = position.GetComponentInChildren<Tower>()?.gameObject;

        if (currentTower == null)
        {
            // Hiển thị bảng mua tháp
            buyPanel.SetActive(true);
            buyPriceText.text = "" + towerCost + "";
        }
        else
        {
            // Hiển thị bảng nâng cấp tháp
            upgradePanel.SetActive(true);
            Tower tower = currentTower.GetComponent<Tower>();
            upgradeInfoText.text = $"Upgrade or Remove Tower (Level {tower.level})";
        }
    }

    // Mua tháp
    void BuyTower()
    {
        if (GearManager.Instance.SpendGears(towerCost))
        {
            Instantiate(towerPrefab, currentTowerPosition.transform.position, Quaternion.identity, currentTowerPosition.transform);
            ClosePanels(); // Đóng bảng và tiếp tục trò chơi
        }
    }

    // Nâng cấp tháp
    void UpgradeTower()
    {
        if (currentTower != null)
        {
            // Thực hiện nâng cấp tháp (ví dụ: tăng cấp độ)
            Tower tower = currentTower.GetComponent<Tower>();
            if (tower != null)
            {
                tower.Upgrade();
                ClosePanels(); // Đóng bảng và tiếp tục trò chơi
            }
        }
    }

    // Xóa tháp
    void RemoveTower()
    {
        if (currentTower != null)
        {
            Destroy(currentTower);
            ClosePanels(); // Đóng bảng và tiếp tục trò chơi
        }
    }

    // Đóng các bảng và tiếp tục trò chơi
    void ClosePanels()
    {
        Time.timeScale = 1; // Tiếp tục trò chơi

        buyPanel.SetActive(false);
        upgradePanel.SetActive(false);
    }
}
