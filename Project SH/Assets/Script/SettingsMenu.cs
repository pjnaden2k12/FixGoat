using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{
    public GameObject settingsPanel;
    public GameTimer gameTimer;
    private bool isMuted = false;

    void Start()
    {
        settingsPanel.SetActive(false);
    }

    public void ToggleSettingsMenu()
    {
        bool isActive = settingsPanel.activeSelf;
        settingsPanel.SetActive(!isActive);
        gameTimer.TogglePause(!isActive);
    }

    public void ToggleMute()
    {
        isMuted = !isMuted;
        AudioListener.volume = isMuted ? 0 : 1;
    }

    public void RestartGame()
    {
        // Chơi lại từ đầu (load lại scene hiện tại)
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        settingsPanel.SetActive(false);
        gameTimer.TogglePause(false);
    }

    public void QuitGame()
    {
        // Bỏ cuộc (đóng game hoặc quay lại menu chính)
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void ResumeGame()
    {
        // Tiếp tục trò chơi
        settingsPanel.SetActive(false);
        gameTimer.TogglePause(false);
    }

    public void GoToMainMenu()
    {
        // Trở về menu chính mà không load lại scene hiện tại
        SceneManager.LoadScene("MainMenuScene");
        settingsPanel.SetActive(false);
        gameTimer.TogglePause(false);
    }
}
