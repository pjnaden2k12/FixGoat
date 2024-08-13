using UnityEngine;
using TMPro;
using UnityEngine.Networking; // Đảm bảo bạn có thư viện này để sử dụng các thành phần Network
using UnityEngine.UI; // Đảm bảo bạn có thư viện này để sử dụng các thành phần UI
using System.Collections; // Thêm thư viện này để sử dụng IEnumerator

public class GameTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public GameObject winPanel; // Tham chiếu đến panel thắng
    public TextMeshProUGUI winGoldText; // Tham chiếu để hiển thị phần thưởng vàng
    public TextMeshProUGUI winDiamondText; // Tham chiếu để hiển thị phần thưởng kim cương
    public TextMeshProUGUI winTowerPieceText; // Tham chiếu để hiển thị phần thưởng mảnh tháp
    public Button confirmButton; // Nút xác nhận
    public TMP_InputField playerNameInput; // Tham chiếu đến trường tên người chơi
    public TextMeshProUGUI feedbackText; // Tham chiếu đến trường thông báo kết quả
    public TextMeshProUGUI highscoreText; // Thêm trường để hiển thị điểm số
    public int gearCount; // Số lượng gear nhận được (thay thế bằng logic thực tế của bạn)
    public int levelId2;
    public float timeWin = 600f;
    private float timeElapsed = 0f;
    private bool isGamePaused = false;
    private bool gameEnded = false;

    private void Start()
    {
        winPanel.SetActive(false);
        confirmButton.onClick.AddListener(OnConfirmButtonClicked);
    }

    void Update()
    {
        if (!isGamePaused && !gameEnded)
        {
            timeElapsed += Time.deltaTime;
            UpdateTimerText();

            if (timeElapsed >= timeWin) // 10 phút tính bằng giây
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
        int goldReward = Random.Range(100 + levelId2 * 50, 3000 + levelId2 * 100) + gearCount;
        int diamondReward = Random.Range(20 + levelId2 * 5, 180 + levelId2 * 10);
        int towerPieceReward = Random.Range(20 + levelId2 * 5, 180 + levelId2 * 10);

        // Cập nhật số lượng phần thưởng trong ResourceManager nếu cần
        ResourceManager.Instance.AddGold(goldReward);
        ResourceManager.Instance.AddDiamonds(diamondReward);
        ResourceManager.Instance.AddTowerPieces(towerPieceReward);

        // Hiển thị phần thưởng
        winGoldText.text = "Gold: " + goldReward;
        winDiamondText.text = "Diamonds: " + diamondReward;
        winTowerPieceText.text = "Tower Pieces: " + towerPieceReward;

        // Tính toán điểm số và hiển thị lên UI
        int highscore = CalculateHighscore(goldReward, diamondReward, towerPieceReward);
        highscoreText.text = "Highscore: " + highscore;
    }

    void OnConfirmButtonClicked()
    {
        // Vô hiệu hóa nút xác nhận
        confirmButton.interactable = false;
        string playerName = playerNameInput.text;
        int highscore = CalculateHighscore(
            int.Parse(winGoldText.text.Split(':')[1].Trim()),
            int.Parse(winDiamondText.text.Split(':')[1].Trim()),
            int.Parse(winTowerPieceText.text.Split(':')[1].Trim())
        );
        StartCoroutine(SendHighscoreToServer(playerName, highscore));

        
    }

    int CalculateHighscore(int gold, int diamond, int towerPieces)
    {
        return levelId2 * (600 + gold + diamond + towerPieces);
    }

    private IEnumerator SendHighscoreToServer(string playerName, int highscore)
    {
        string token = PlayerPrefs.GetString("token");
        if (string.IsNullOrEmpty(token))
        {
            feedbackText.text = "You must login.";
            yield break;
        }

        string url = "https://fpl.expvn.com/InsertHighscore.php";
        WWWForm form = new WWWForm();
        form.AddField("token", token);
        form.AddField("playerName", playerName);
        form.AddField("score", highscore);

        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                string responseText = www.downloadHandler.text;

                if (responseText == "You must login")
                {
                    feedbackText.text = "Login fail end time. Pls login agian.";
                    // Hiển thị form đăng nhập để người dùng nhập lại thông tin
                    // Ví dụ: OpenLoginPanel();
                }
                else if (responseText == "Done")
                {
                    
                    feedbackText.text = "Highscore is Save Done!";
                }
                else if (responseText.StartsWith("Error"))
                {
                    feedbackText.text = "Error: " + responseText;
                }
                else
                {
                    feedbackText.text = "Error from server.";
                }
            }
            else
            {
                feedbackText.text = "Your Connect: " + www.error;
            }
        }
    }
}
