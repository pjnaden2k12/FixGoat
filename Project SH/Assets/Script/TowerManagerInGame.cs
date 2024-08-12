using UnityEngine;
using UnityEngine.UI;

public class TowerManagerInGame : MonoBehaviour
{
    public static TowerManagerInGame Instance { get; private set; }

    public GameObject buyPanel;
    public GameObject upgradePanel;
    public GameObject deletePanel;
    public Button closeButton;

    public Text towerInfoText;

    private int selectedSlotIndex;
    public Transform[] slotPositions; // Mảng các vị trí trống để đặt tháp
    private Tower[] placedTowers; // Mảng lưu trữ các tháp đã được đặt

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        placedTowers = new Tower[slotPositions.Length]; // Khởi tạo mảng các tháp
    }

    private void Start()
    {
        closeButton.onClick.AddListener(CloseAllPanels);
        CloseAllPanels();
    }

    public void OnSlotClicked(int slotIndex)
    {
        selectedSlotIndex = slotIndex;

        if (placedTowers[slotIndex] != null) // Nếu có tháp ở vị trí này
        {
            OpenUpgradeOrDeletePanel(slotIndex);
        }
        else
        {
            OpenBuyPanel(slotIndex);
        }
    }

    public void OpenBuyPanel(int slotIndex)
    {
        towerInfoText.text = $"Buy a new tower at slot: {slotIndex + 1}";

        buyPanel.SetActive(true);
        upgradePanel.SetActive(false);
        deletePanel.SetActive(false);
    }

    public void OpenUpgradeOrDeletePanel(int slotIndex)
    {
        Tower tower = placedTowers[slotIndex];
        towerInfoText.text = $"Upgrade or Delete Tower at slot: {slotIndex + 1}\n" +
                             $"Level: {tower.level}\nDamage: {tower.GetDamage()}\n" +
                             $"Attack Speed: {tower.GetAttackSpeed()}\nRange: {tower.GetRange()}";

        buyPanel.SetActive(false);
        upgradePanel.SetActive(true);
        deletePanel.SetActive(true);
    }

    public void CloseAllPanels()
    {
        buyPanel.SetActive(false);
        upgradePanel.SetActive(false);
        deletePanel.SetActive(false);
    }

    public void OnBuyButtonClicked()
    {
        // Mua tháp và đặt nó vào vị trí đã chọn
        TowerManager.Instance.UnlockTower(selectedSlotIndex);

        GameObject towerPrefab = TowerManager.Instance.towers[selectedSlotIndex].towerPrefab;
        if (towerPrefab != null)
        {
            GameObject towerObject = Instantiate(towerPrefab, slotPositions[selectedSlotIndex].position, Quaternion.identity);
            placedTowers[selectedSlotIndex] = towerObject.GetComponent<Tower>(); // Lưu tháp đã được đặt
        }

        CloseAllPanels();
    }

    public void OnUpgradeButtonClicked()
    {
        if (placedTowers[selectedSlotIndex] != null)
        {
            placedTowers[selectedSlotIndex].Upgrade(); // Nâng cấp tháp
        }

        CloseAllPanels();
    }

    public void OnDeleteButtonClicked()
    {
        if (placedTowers[selectedSlotIndex] != null)
        {
            Destroy(placedTowers[selectedSlotIndex].gameObject); // Xóa tháp khỏi scene
            placedTowers[selectedSlotIndex] = null; // Xóa tháp khỏi danh sách
        }

        CloseAllPanels();
    }
}
