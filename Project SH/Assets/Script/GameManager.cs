using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public HighscoreManager highscoreManager; // Reference to HighscoreManager
    public GearManager gearManager; // Reference to GearManager
    public GameTimer gameTimer; // Reference to GameTimer
    public int currentLevel = 1; // Current level, should be updated according to the game

    // Call this method when the boss is defeated
    public void OnBossDefeated()
    {
        // Get the total gear and completion time
        int totalGears = gearManager.totalGears;
        float completionTime = gameTimer.GetTimeElapsed(); // Make sure to implement GetTimeElapsed() in GameTimer

        // Set level, totalGear, and completionTime
        HighscoreCalculator highscoreCalculator = highscoreManager.GetComponent<HighscoreCalculator>();
        highscoreCalculator.level = currentLevel;
        highscoreCalculator.totalGear = totalGears;
        highscoreCalculator.completionTime = completionTime;

        // Submit highscore
        if (highscoreManager != null)
        {
            highscoreManager.SubmitHighscore();
        }

        // Optionally, load the victory screen or menu
        // SceneManager.LoadScene("VictoryScene");
    }
}
