using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LeaderboardManager : MonoBehaviour
{
    public TMP_Text[] leaderboardEntries;

    void Start()
    {
        UpdateLeaderboard();
    }

    void UpdateLeaderboard()
    {
        for (int i = 0; i < 5; i++) // 5 màn
        {
            float bestTime = PlayerPrefs.GetFloat("Level" + (i + 1).ToString() + "Time", float.MaxValue);
            leaderboardEntries[i].text = "Level " + (i + 1).ToString() + ": " + FormatTime(bestTime);
        }
    }

    string FormatTime(float time)
    {
        if (time == float.MaxValue)
            return "N/A";
        else
        {
            int minutes = (int)time / 60;
            int seconds = (int)time % 60;
            return string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }
}
