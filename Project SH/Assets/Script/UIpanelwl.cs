using UnityEngine;
using UnityEngine.SceneManagement; // Để quản lý các scene
using TMPro;
using UnityEngine.UI; // Đảm bảo bạn có thư viện này để sử dụng các thành phần UI

public class UIManager : MonoBehaviour
{
    public GameObject winPanel; // Tham chiếu đến panel thắng
    public GameObject rewardPanel; // Tham chiếu đến panel phần thưởng
    public Button playAgainButton; // Nút chơi lại
    public Button mainMenuButton; // Nút về màn chính

    void Start()
    {
        // Gán các phương thức cho các nút
        playAgainButton.onClick.AddListener(RestartGame);
        mainMenuButton.onClick.AddListener(GoToMainMenu);
    }

    // Phương thức chơi lại trò chơi
    void RestartGame()
    {
        // Tải lại scene hiện tại để bắt đầu lại trò chơi
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Phương thức về màn chính
    void GoToMainMenu()
    {
        rewardPanel.SetActive(false);
        // Tải scene chính của trò chơi (giả sử là scene có tên "MainMenu")
        SceneManager.LoadScene("MainMenuScene");
    }
}
