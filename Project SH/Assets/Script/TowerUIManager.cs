using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TowerManagerUI : MonoBehaviour
{
    public static TowerManagerUI Instance { get; private set; }

    public GameObject[] towerPanels; // Mảng các panel tháp bị khóa
    public GameObject towerInfoPanel; // Panel thông tin chi tiết tháp
    public Image towerDetailImage; // Hình ảnh chi tiết tháp
    public TMP_Text towerDetailText; // Mô tả chi tiết tháp
    public Button closeButton; // Nút đóng panel thông tin
    public Button unlockButton; // Nút mở khóa tháp
    public Sprite placeholderSprite; // Hình ảnh placeholder cho tháp bị khóa
    public Sprite[] towerSprites; // Hình ảnh của các tháp
    public string[] towerDescriptions; // Mô tả chi tiết của các tháp

    private int currentTowerIndex; // Chỉ số của tháp hiện tại

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Kiểm tra và đảm bảo các tham chiếu UI đã được gán
        if (closeButton == null || unlockButton == null || towerInfoPanel == null || towerDetailImage == null || towerDetailText == null)
        {
            Debug.LogError("Some UI elements are not assigned in the inspector!");
            return;
        }
        closeButton.onClick.AddListener(CloseTowerInfoPanel);
        unlockButton.onClick.AddListener(UnlockCurrentTower);
    }

    public void OnTowerPanelClicked(int towerIndex)
    {
        currentTowerIndex = towerIndex;

        if (IsTowerLocked(towerIndex))
        {
            // Hiển thị thông tin placeholder cho tháp bị khóa
            towerDetailImage.sprite = placeholderSprite;
            towerDetailText.text = "Công: ???\nTốc độ: ???\nTầm bắn: ???\nĐặc điểm: ???";

            // Đảm bảo nút mở khóa có thể nhấn được
            unlockButton.interactable = true;
        }
        else
        {
            // Hiển thị thông tin chi tiết cho tháp đã mở khóa
            ShowTowerInfoPanel(towerIndex);

            // Vô hiệu hóa nút mở khóa nếu tháp đã mở khóa
            unlockButton.interactable = false;
        }

        towerInfoPanel.SetActive(true);
    }

    private void ShowTowerInfoPanel(int towerIndex)
    {
        Tower currentTower = TowerManager.Instance.towers[towerIndex];
        // Cập nhật thông tin chi tiết cho tháp được mở khóa
        towerDetailImage.sprite = towerSprites[towerIndex];
        towerDetailText.text = $"Công: {currentTower.GetDamage()}\nTốc độ: {currentTower.GetAttackSpeed()}\nTầm bắn: {currentTower.GetRange()}\nĐặc điểm: {towerDescriptions[towerIndex]}";
        towerInfoPanel.SetActive(true);

    }

    private void CloseTowerInfoPanel()
    {
        // Ẩn panel thông tin chi tiết
        towerInfoPanel.SetActive(false);
    }

    private void UnlockCurrentTower()
    {
        // Kiểm tra và mở khóa tháp hiện tại
        if (IsTowerLocked(currentTowerIndex) && ResourceManager.Instance.GetTowerPieces() >= 25)
        {
            ResourceManager.Instance.SpendTowerPieces(25);
            TowerManager.Instance.UnlockTower(currentTowerIndex); // Cập nhật trạng thái tháp
            ShowTowerInfoPanel(currentTowerIndex); // Cập nhật thông tin tháp sau khi mở khóa
            unlockButton.interactable = false;
        }
        else
        {
            Debug.LogWarning("Không đủ mảnh tháp để mở khóa hoặc tháp đã được mở khóa.");
        }
    }

    private bool IsTowerLocked(int towerIndex)
    {
        // Giả sử bạn có một cách để kiểm tra trạng thái khóa của tháp
        return !TowerManager.Instance.towers[towerIndex].IsUnlocked();
    }
   
}
