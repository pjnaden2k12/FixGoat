using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TowerPurchasePanel : MonoBehaviour
{
    public GameObject towerItemPrefab; // Prefab của tháp
    public Transform contentTransform; // Transform của Content trong Scroll View
    public Button closeButton; // Nút đóng bảng

    void Start()
    {
        closeButton.onClick.AddListener(ClosePanel);
        PopulateTowerList();
    }

    void PopulateTowerList()
    {
        // Xóa các mục hiện có
        foreach (Transform child in contentTransform)
        {
            Destroy(child.gameObject);
        }

        // Lấy thông tin tháp từ TowerManager
        Tower[] allTowers = TowerManager.Instance.towers;

        foreach (Tower tower in allTowers)
        {
            if (tower.IsUnlocked()) // Chỉ hiển thị tháp đã mở khóa
            {
                GameObject towerItem = Instantiate(towerItemPrefab, contentTransform);
                // Cập nhật thông tin cho từng item
                towerItem.transform.Find("Image").GetComponent<Image>().sprite = tower.sprite;
                towerItem.transform.Find("NameText").GetComponent<TextMeshProUGUI>().text = tower.name;
                towerItem.transform.Find("PriceText").GetComponent<TextMeshProUGUI>().text = "Price: 50 Gear";

                Button buyButton = towerItem.transform.Find("BuyButton").GetComponent<Button>();
                buyButton.onClick.AddListener(() => BuyTower(tower));
            }
        }
    }

    void BuyTower(Tower tower)
    {
        if (GearManager.Instance.SpendGears(50))
        {
            // Instantiate tháp tại vị trí người chơi chọn
            // Gọi phương thức để đặt tháp tại vị trí
            // TowerManagerInGame.Instance.PlaceTower(tower); // Nếu có phương thức này
            Debug.Log($"Tower {tower.name} purchased.");
            ClosePanel();
        }
        else
        {
            Debug.LogWarning("Not enough gears to buy this tower.");
        }
    }

    void ClosePanel()
    {
        gameObject.SetActive(false);
    }
}
