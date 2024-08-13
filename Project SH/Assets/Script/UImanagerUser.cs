using UnityEngine;
using UnityEngine.SceneManagement; // Để chuyển đổi giữa các cảnh nếu cần

public class UImanagerUser : MonoBehaviour
{
    // Các panel
    public GameObject MainPanel;
    public GameObject DangKyPanel;
    public GameObject DangNhapPanel;
    public GameObject SettingPanel;
    public GameObject LoginSuccessPanel; // Panel sau khi đăng nhập thành công

    // Hàm gọi khi nhấn nút Đăng Ký
    public void ShowDangKyPanel()
    {
        MainPanel.SetActive(false); // Ẩn Main Panel
        DangKyPanel.SetActive(true); // Hiện DangKy Panel
        DangNhapPanel.SetActive(false); // Ẩn Đăng Nhập Panel nếu hiện
        SettingPanel.SetActive(false); // Ẩn Setting Panel nếu hiện
    }

    // Hàm gọi khi nhấn nút Đăng Nhập
    public void ShowDangNhapPanel()
    {
        MainPanel.SetActive(false); // Ẩn Main Panel
        DangNhapPanel.SetActive(true); // Hiện DangNhap Panel
        DangKyPanel.SetActive(false); // Ẩn Đăng Ký Panel nếu hiện
        SettingPanel.SetActive(false); // Ẩn Setting Panel nếu hiện
    }

    // Hàm gọi khi nhấn nút Close trong các panel
    public void ClosePanel()
    {
        DangKyPanel.SetActive(false); // Ẩn DangKy Panel
        DangNhapPanel.SetActive(false); // Ẩn DangNhap Panel
        SettingPanel.SetActive(false); // Ẩn Setting Panel
        MainPanel.SetActive(true); // Hiện lại Main Panel
    }

    // Hàm gọi khi đăng nhập thành công
    public void OnLoginSuccess()
    {
        DangNhapPanel.SetActive(false); // Ẩn Đăng Nhập Panel
        LoginSuccessPanel.SetActive(true); // Hiện Login Success Panel
    }

    // Hàm gọi khi nhấn nút Chơi Ngay
    public void PlayGame()
    {
        // Chuyển sang màn chơi chính
        SceneManager.LoadScene("MainMenuScene");
        Debug.Log("Bắt đầu chơi game!");
    }

    // Hàm gọi khi nhấn nút Cài Đặt
    public void OpenSettings()
    {

        

        SettingPanel.SetActive(true); // Hiện Setting Panel
    }

    // Hàm gọi khi nhấn nút Đóng Cài Đặt
    public void CloseSettings()
    {
        SettingPanel.SetActive(false); // Ẩn Setting Panel

        

        LoginSuccessPanel.SetActive(true); // Hiện Login Success Panel

    }

    // Hàm gọi khi nhấn nút Đăng Xuất
    public void Logout()
    {
        LoginSuccessPanel.SetActive(false); // Ẩn Login Success Panel
        MainPanel.SetActive(true); // Quay lại Main Panel
        Debug.Log("Đã đăng xuất!");
        // Xóa thông tin đăng nhập và tải lại scene
        PlayerPrefs.DeleteKey("token");
        PlayerPrefs.DeleteKey("username");
        

        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Tải lại scene hiện tại

    }
}
