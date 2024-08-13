using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class HighscoreManager : MonoBehaviour
{
    public string insertHighscoreUrl = "https://fpl.expvn.com/InsertHighscore.php";
    public string getHighscoreUrl = "https://fpl.expvn.com/GetHighscore.php";
    public TMP_InputField playerNameInput; // Sử dụng TMP_InputField thay vì InputField
    public Button submitButton; // Sử dụng TMP_Button thay vì Button
    public TMP_Text feedbackText; // Sử dụng TMP_Text thay vì Text
    public TMP_Text highscoreText;

    private string token;
    private HighscoreCalculator highscoreCalculator;

    private void Start()
    {
        token = PlayerPrefs.GetString("token", "");
        submitButton.onClick.AddListener(SubmitHighscore);
        highscoreCalculator = FindObjectOfType<HighscoreCalculator>();
        StartCoroutine(UpdateHighscoreRoutine());
    }

    public void SubmitHighscore()
    {
        string playerName = playerNameInput.text;
        int score = highscoreCalculator.CalculateScore();
        StartCoroutine(SubmitHighscoreCoroutine(playerName, score));
    }

    private IEnumerator SubmitHighscoreCoroutine(string playerName, int score)
    {
        WWWForm form = new WWWForm();
        form.AddField("token", token);
        form.AddField("playerName", playerName);
        form.AddField("score", score);

        using (UnityWebRequest www = UnityWebRequest.Post(insertHighscoreUrl, form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                HandleInsertHighscoreResponse(www.downloadHandler.text);
            }
            else
            {
                feedbackText.text = "Lỗi kết nối!";
            }
        }
    }

    private void HandleInsertHighscoreResponse(string response)
    {
        if (response.Contains("Bạn cần đăng nhập để thực hiện thao tác này"))
        {
            feedbackText.text = "Bạn cần đăng nhập lại.";
            // Implement re-login logic if needed
        }
        else if (response.Contains("Done"))
        {
            feedbackText.text = "Đã lưu dữ liệu lên server thành công.";
        }
        else
        {
            feedbackText.text = "Lỗi kết nối!";
        }
    }

    private IEnumerator UpdateHighscoreRoutine()
    {
        while (true)
        {
            yield return FetchHighscore();
            yield return new WaitForSeconds(60f);
        }
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
                ProcessHighscores(www.downloadHandler.text);
            }
            else
            {
                feedbackText.text = "Lỗi kết nối!";
            }
        }
    }

    private void ProcessHighscores(string response)
    {
        string[] lines = response.Split('\n');
        if (lines.Length > 0)
        {
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
