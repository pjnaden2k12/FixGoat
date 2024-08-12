//using UnityEngine;
//using UnityEngine.UI;
//using TMPro;

//public class TowerPurchasePanel : MonoBehaviour
//{
//    public GameObject towerItemPrefab; // Prefab của tháp
//    public Transform contentTransform; // Transform của Content trong Scroll View
//    public Button closeButton; // Nút đóng bảng

//    void Start()
//    {
//        closeButton.onClick.AddListener(ClosePanel);
//        PopulateTowerList();
//    }

//    void PopulateTowerList()
//    {
//        Xóa các mục hiện có
//        foreach (Transform child in contentTransform)
//        {
//            Destroy(child.gameObject);
//        }

//        Lấy thông tin tháp từ TowerManager
//        var unlockedTowers = TowerManager.Instance.GetUnlockedTowers();

//        foreach (var towerData in unlockedTowers)
//        {
//            GameObject towerItem = Instantiate(towerItemPrefab, contentTransform);

//            Cập nhật thông tin cho từng item
//           var image = towerItem.transform.Find("Image").GetComponent<Image>();
//            var nameText = towerItem.transform.Find("NameText").GetComponent<TextMeshProUGUI>();
//            var priceText = towerItem.transform.Find("PriceText").GetComponent<TextMeshProUGUI>();
//            var buyButton = towerItem.transform.Find("BuyButton").GetComponent<Button>();

//            Cập nhật thông tin từ TowerData
//            image.sprite = towerData.towerSprite;
//            nameText.text = $"Tower {towerData.id}"; // Tên tháp có thể được lấy từ towerData hoặc thiết lập trước
//            priceText.text = "Price: 50 Gear";

//            buyButton.onClick.RemoveAllListeners();
//            buyButton.onClick.AddListener(() => BuyTower(towerData));
//        }
//    }

//    void BuyTower(TowerManager.TowerData towerData)
//    {
//        Kiểm tra tài nguyên và mua tháp
//        if (GearManager.Instance.SpendGears(50))
//        {
//            Instantiate tháp tại vị trí người chơi chọn
//             Gọi phương thức để đặt tháp tại vị trí
//             TowerManagerInGame.Instance.PlaceTower(towerData); // Nếu có phương thức này

//            Debug.Log($"Tower ID {towerData.id} purchased.");
//            ClosePanel();
//        }
//        else
//        {
//            Debug.LogWarning("Not enough gears to buy this tower.");
//        }
//    }

//    void ClosePanel()
//    {
//        gameObject.SetActive(false);
//    }
//}
