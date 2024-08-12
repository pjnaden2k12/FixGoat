using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TowerDisplayManager : MonoBehaviour
{
    public GameObject scrollViewContent; // Content của Scroll View
    public GameObject towerPrefab; // Prefab của tháp

    private TowerManager towerManager;

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
            Image towerImage = towerGO.GetComponentInChildren<Image>();
            Button towerButton = towerGO.GetComponent<Button>();
            TextMeshProUGUI towerText = towerGO.GetComponentInChildren<TextMeshProUGUI>();

            if (towerImage == null || towerText == null)
            {
                Debug.LogError("Image or Text component not found on the prefab.");
                continue;
            }

            // Cập nhật hình ảnh tháp
            towerImage.sprite = tower.towerSprite;

            // Cập nhật trạng thái của nút (có thể nhấn hay không)
            bool isUnlocked = tower.isUnlocked;
            towerButton.interactable = isUnlocked;
            Color buttonColor = isUnlocked ? Color.white : Color.gray;
            towerImage.color = buttonColor;

            // Cập nhật văn bản với chỉ số cơ bản của tháp
            towerText.text = $"Damage: {tower.baseDamage}\nAttack Speed: {tower.baseAttackSpeed}\nRange: {tower.baseRange}";

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

        // Kiểm tra xem người chơi có đủ gear không
        if (GearManager.Instance.SpendGears(50))
        {
            
            
                Debug.Log("Không có vị trí trống để đặt tháp.");
            
        }
        else
        {
            Debug.Log("Không đủ gear để mua tháp.");
        }
    }
}
