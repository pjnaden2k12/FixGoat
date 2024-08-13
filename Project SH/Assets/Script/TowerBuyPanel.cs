using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TowerDisplayManager : MonoBehaviour
{
    public GameObject scrollViewContent; // Content của Scroll View
    public GameObject towerPrefab; // Prefab của tháp

    private TowerManager towerManager;
    private int selectedSlotIndex; // Chỉ số của ô đã chọn
    
    
    private void Start()
    {
        towerManager = TowerManager.Instance;

        if (towerManager == null)
        {
            Debug.LogError("TowerManager.Instance is null. Make sure you have a TowerManager in your scene.");
            return;
        }
        
        DisplayTowers();
    }

    public void SetSelectedSlotIndex(int index)
    {
        selectedSlotIndex = index;
    }

    private void DisplayTowers()
    {
        if (scrollViewContent == null || towerPrefab == null)
        {
            Debug.LogError("scrollViewContent or towerPrefab is null. Make sure they are assigned in the Unity Editor.");
            return;
        }

        // Xóa tất cả các tháp hiện tại trong Content
        foreach (Transform child in scrollViewContent.transform)
        {
            Destroy(child.gameObject);
        }

        // Tạo các tháp từ dữ liệu trong TowerManager
        foreach (var tower in towerManager.towers)
        {
            GameObject towerGO = Instantiate(towerPrefab, scrollViewContent.transform);

            // Lấy tham chiếu tới các thành phần cần thiết
            Image childTowerImage = towerGO.transform.Find("ChildImage").GetComponent<Image>(); // Tên của Image con trong Prefab
            Button towerButton = towerGO.GetComponent<Button>();
            TextMeshProUGUI towerText = towerGO.GetComponentInChildren<TextMeshProUGUI>();

            if (childTowerImage == null || towerText == null)
            {
                Debug.LogError("Image or Text component not found on the prefab.");
                continue;
            }

            // Cập nhật hình ảnh tháp vào Image con
            childTowerImage.sprite = tower.towerSprite;

            // Cập nhật trạng thái của nút (có thể nhấn hay không)
            bool isUnlocked = tower.isUnlocked;
            towerButton.interactable = isUnlocked;
            Color buttonColor = isUnlocked ? Color.white : Color.gray;
            childTowerImage.color = buttonColor;

            // Cập nhật văn bản với chỉ số cơ bản của tháp
            towerText.text = $"ATK: {tower.baseDamage}\nSPD: {tower.baseAttackSpeed}\nRNG: {tower.baseRange}";

            // Gán sự kiện khi người dùng nhấn vào tháp
            towerButton.onClick.AddListener(() => OnTowerButtonClicked(tower));
        }

    }

    private void OnTowerButtonClicked(TowerManager.TowerData towerData)
    {
        if (!towerData.isUnlocked)
        {
            Debug.Log("Tháp chưa mở khóa!");
            return;
        }

        // Kiểm tra và trừ gear khi mua tháp
        if (GearManager.Instance.SpendGears(50)) // Thay đổi 50 nếu cần số gear khác
        {
            // Đặt tháp tại vị trí ô đã chọn
            PlaceTowerAtSelectedSlot(towerData);

            // Đóng panel mua tháp
            TowerManagerInGame.Instance.buyTowerPanel.SetActive(false);
        }
        else
        {
            Debug.Log("Không đủ gear để mua tháp!");
        }
    }


    private void PlaceTowerAtSelectedSlot(TowerManager.TowerData towerData)
    {
        // Đảm bảo rằng chỉ số ô hợp lệ
        if (selectedSlotIndex < 0 || selectedSlotIndex >= TowerManagerInGame.Instance.towerSlots.Length)
        {
            Debug.LogError("Selected slot index is out of bounds.");
            return;
        }

        GameObject selectedSlot = TowerManagerInGame.Instance.towerSlots[selectedSlotIndex];
        GameObject newTower = Instantiate(towerData.towerPrefab, selectedSlot.transform.position, Quaternion.identity);

        // Gán tháp làm con của ô đã chọn
        newTower.transform.SetParent(selectedSlot.transform);

        Debug.Log($"Đặt tháp {towerData.id} tại vị trí ô {selectedSlotIndex}");
    }
    
}
