using UnityEngine;
using UnityEngine.UI;

public class TowerManager : MonoBehaviour
{
    public Tower[] towers; // Danh sách các tháp
    public GameObject towerPanelPrefab; // Prefab cho UI panel của tháp
    public Transform panelContainer; // Nơi chứa các panel trong UI
    public TowerManagerUI towerManagerUI; // UI quản lý thông tin tháp
    public Sprite placeholderSprite; // Hình ảnh placeholder cho tháp bị khóa

    void Start()
    {
        // Tạo các panel cho từng tháp khi bắt đầu
        for (int i = 0; i < towers.Length; i++)
        {
            CreateTowerPanel(i);
        }
    }

    void CreateTowerPanel(int towerIndex)
    {
        // Tạo một panel từ prefab
        GameObject towerPanel = Instantiate(towerPanelPrefab, panelContainer);

        // Cấu hình thông tin cho panel
        TowerButtonHandler buttonHandler = towerPanel.GetComponent<TowerButtonHandler>();
        if (buttonHandler != null)
        {
            buttonHandler.towerIndex = towerIndex;
            buttonHandler.towerManagerUI = towerManagerUI; // Gán UI quản lý tháp
        }

        // Cập nhật hình ảnh và thông tin của panel tùy thuộc vào trạng thái mở khóa
        UpdateTowerPanelUI(towerPanel, towerIndex);
    }

    void UpdateTowerPanelUI(GameObject towerPanel, int towerIndex)
    {
        // Lấy hình ảnh và thông tin từ panel
        Image towerImage = towerPanel.GetComponent<Image>();
        Text towerInfo = towerPanel.GetComponentInChildren<Text>();

        // Cập nhật thông tin panel
        if (towers[towerIndex].IsUnlocked())
        {
            // Nếu tháp đã được mở khóa, hiển thị thông tin đầy đủ
            towerImage.sprite = towers[towerIndex].GetSprite(); // Cập nhật hình ảnh
            towerInfo.text = "Nhấn để xem thông tin"; // Thông tin có thể tùy chỉnh
        }
        else
        {
            // Nếu tháp chưa mở khóa, hiển thị hình ảnh và thông tin placeholder
            towerImage.sprite = placeholderSprite; // Hình ảnh placeholder cho tháp bị khóa
            towerInfo.text = "???"; // Thông tin placeholder
        }
    }
}
