using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using TMPro; // Thư viện TextMesh Pro

public class HighscoreManager : MonoBehaviour
{
    private string token; // Đoạn mã đã lưu trong PlayerPrefs
    private string getHighscoreUrl = "https://fpl.expvn.com/GetHighscore.php";
    public TextMeshProUGUI highscoreText; // TextMesh Pro UI element

    void Start()
    {
        token = PlayerPrefs.GetString("token", string.Empty);
        if (!string.IsNullOrEmpty(token))
        {
            StartCoroutine(GetHighscore());
        }
        else
        {
            Debug.LogWarning("Token không hợp lệ. Vui lòng đăng nhập.");
        }
    }

    IEnumerator GetHighscore()
    {
        string url = getHighscoreUrl + "?token=" + token;
        UnityWebRequest www = UnityWebRequest.Get(url);

        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            string responseText = www.downloadHandler.text;
            DisplayPlayerHighscore(responseText);
        }
        else
        {
            Debug.LogError("Lỗi kết nối: " + www.error);
        }
    }

    void DisplayPlayerHighscore(string responseText)
    {
        // Tách các dòng của dữ liệu
        string[] lines = responseText.Split('\n');
        foreach (string line in lines)
        {
            if (string.IsNullOrEmpty(line)) continue;

            string[] parts = line.Split('\t');
            if (parts.Length == 2)
            {
                string playerName = parts[0];
                if (int.TryParse(parts[1], out int score))
                {
                    // So sánh tên người chơi với token của người chơi hiện tại
                    if (playerName == GetPlayerNameFromToken(token))
                    {
                        // Hiển thị điểm số của người chơi hiện tại
                        highscoreText.text = $"{playerName} \n{score}";
                        return; // Dừng vòng lặp khi tìm thấy điểm số của người chơi
                    }
                }
            }
        }

        // Nếu không tìm thấy điểm số của người chơi hiện tại
        highscoreText.text = "No score.";
    }

    // Phương thức giả để lấy tên người chơi từ token
    // Thay thế phương thức này bằng phương thức thực tế của bạn
    private string GetPlayerNameFromToken(string token)
    {
        // Ví dụ: trả về tên người chơi từ token
        // Trong thực tế, bạn cần gửi yêu cầu đến server để lấy tên người chơi từ token
        // hoặc lưu trữ tên người chơi cùng với token
        return "PlayerName"; // Thay thế bằng cách thực tế của bạn
    }
}
