using UnityEngine;
using TMPro; // Thêm namespace cho TextMeshPro
using UnityEngine.UI;

public class NotificationManager : MonoBehaviour
{
    public GameObject notificationPanel; // Panel thông báo
    public TMP_Text notificationText; // Text hiển thị thông báo
    public Button closeButton; // Nút đóng bảng thông báo

    private void Start()
    {
        // Đảm bảo panel được ẩn khi bắt đầu
        notificationPanel.SetActive(false);

        // Gán sự kiện cho nút đóng
        closeButton.onClick.AddListener(CloseNotificationPanel);
    }

    public void ShowNotification(int towerPiecesReceived)
    {
        // Cập nhật văn bản thông báo
        notificationText.text = $"Bạn Nhận Được {towerPiecesReceived} tower pieces!";

        // Hiển thị panel thông báo
        notificationPanel.SetActive(true);
    }

    private void CloseNotificationPanel()
    {
        // Ẩn panel thông báo
        notificationPanel.SetActive(false);
    }
}
