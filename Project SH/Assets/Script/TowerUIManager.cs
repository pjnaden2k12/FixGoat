using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TowerManagerUI : MonoBehaviour
{
    public GameObject[] towerPanels; // Các panel tháp bị khóa
    public GameObject towerInfoPanel; // Panel thông tin chi tiết tháp
    public Image towerDetailImage; // Hình ảnh chi tiết tháp
    public TMP_Text towerDetailText; // Mô tả chi tiết tháp
    public Button closeButton; // Nút đóng panel thông tin

    public Sprite[] towerImages; // Hình ảnh của các tháp
    public string[] towerDescriptions; // Mô tả chi tiết của các tháp

    private void Start()
    {
        // Thêm sự kiện cho các panel tháp bị khóa
        for (int i = 0; i < towerPanels.Length; i++)
        {
            int index = i;
            Button panelButton = towerPanels[i].GetComponent<Button>();
            panelButton.onClick.AddListener(() => OnTowerPanelClicked(index));
        }

        closeButton.onClick.AddListener(CloseTowerInfoPanel);
    }

    public void OnTowerPanelClicked(int towerIndex)
    {
        if (IsTowerLocked(towerIndex))
        {
            // Hiển thị thông tin chi tiết cho tháp bị khóa
            towerDetailImage.sprite = towerImages[towerIndex];
            towerDetailText.text = "Công: ???\nTốc độ: ???\nKĩ năng: ???";
            towerInfoPanel.SetActive(true);
        }
    }

    private void ShowTowerInfoPanel(int towerIndex)
    {
        // Cập nhật thông tin chi tiết cho tháp được mở khóa
        towerDetailImage.sprite = towerImages[towerIndex];
        towerDetailText.text = towerDescriptions[towerIndex];
        towerInfoPanel.SetActive(true);
    }

    private void CloseTowerInfoPanel()
    {
        // Ẩn panel thông tin chi tiết
        towerInfoPanel.SetActive(false);
    }

    private bool IsTowerLocked(int towerIndex)
    {
        // Kiểm tra nếu tháp có bị khóa không
        return towerPanels[towerIndex].activeSelf;
    }
}
