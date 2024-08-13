using System.Collections; // Đảm bảo rằng bạn đã bao gồm namespace này
using UnityEngine;
using TMPro;
using UnityEngine.Networking; // Thêm nếu bạn sử dụng UnityWebRequest


public class HighscoreDisplay : MonoBehaviour
{
    public TextMeshProUGUI highscoreText; // Tham chiếu đến TextMeshProUGUI
    public string getHighscoreUrl = "https://fpl.expvn.com/GetHighscore.php";
    private string token;

    private void Start()
    {
        // Tải token từ PlayerPrefs
        token = PlayerPrefs.GetString("token", "");

        // Cập nhật điểm cao nhất ngay khi bắt đầu
        StartCoroutine(FetchHighscore());
    }

    private IEnumerator FetchHighscore()
    {
        WWWForm form = new WWWForm();
        form.AddField("token", token);

        using (UnityWebRequest www = UnityWebRequest.Post(getHighscoreUrl, form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                string response = www.downloadHandler.text;
                UpdateHighscoreText(response);
            }
            else
            {
                highscoreText.text = "Lỗi kết nối!";
            }
        }
    }

    private void UpdateHighscoreText(string response)
    {
        string[] lines = response.Split('\n');
        if (lines.Length > 0)
        {
            // Lấy điểm cao nhất từ danh sách
            string highestScoreLine = lines[0].Trim();
            if (!string.IsNullOrEmpty(highestScoreLine))
            {
                string[] parts = highestScoreLine.Split('\t');
                if (parts.Length == 2)
                {
                    highscoreText.text = $"Highscore: {parts[0]} - {parts[1]}";
                }
            }
        }
    }
}
