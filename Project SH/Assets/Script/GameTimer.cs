using UnityEngine;
using TMPro;
using UnityEngine.UI; // Đảm bảo bạn có thư viện này để sử dụng các thành phần UI

public class GameTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public GameObject winPanel; // Tham chiếu đến panel thắng
    public TextMeshProUGUI winGoldText; // Tham chiếu để hiển thị phần thưởng vàng
    public TextMeshProUGUI winDiamondText; // Tham chiếu để hiển thị phần thưởng kim cương
    public TextMeshProUGUI winTowerPieceText; // Tham chiếu để hiển thị phần thưởng mảnh tháp
    public int gearCount; // Số lượng gear nhận được (thay thế bằng logic thực tế của bạn)

    private float timeElapsed = 0f;
    private bool isGamePaused = false;
    private bool gameEnded = false;

    void Update()
    {
        if (!isGamePaused && !gameEnded)
        {
            timeElapsed += Time.deltaTime;
            UpdateTimerText();

            if (timeElapsed >= 6f) // 10 phút tính bằng giây
            {
                EndGame();
            }
        }
    }

    void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(timeElapsed / 60F);
        int seconds = Mathf.FloorToInt(timeElapsed % 60F);
        int milliseconds = Mathf.FloorToInt((timeElapsed * 100F) % 100F);
        timerText.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);
    }

    public void TogglePause(bool pause)
    {
        isGamePaused = pause;
        Time.timeScale = pause ? 0 : 1;
    }

    public float GetTimeElapsed()
    {
        return timeElapsed;
    }

    void EndGame()
    {
        gameEnded = true;
        TogglePause(true); // Tạm dừng trò chơi khi kết thúc

        // Hiển thị panel thắng
        winPanel.SetActive(true);

        // Tính toán phần thưởng
        int goldReward = Mathf.RoundToInt(Random.Range(500, 2000) + (gearCount * 1.5f));
        int diamondReward = Mathf.RoundToInt(Random.Range(10, 90) * 1.5f);
        int towerPieceReward = Mathf.RoundToInt(Random.Range(10, 90) * 1.5f);

        // Cập nhật số lượng phần thưởng trong ResourceManager nếu cần
        ResourceManager.Instance.AddGold(goldReward);
        ResourceManager.Instance.AddDiamonds(diamondReward);
        ResourceManager.Instance.AddTowerPieces(towerPieceReward);

        // Hiển thị phần thưởng
        winGoldText.text = "Gold: " + goldReward;
        winDiamondText.text = "Diamonds: " + diamondReward;
        winTowerPieceText.text = "Tower Pieces: " + towerPieceReward;
    }
}
