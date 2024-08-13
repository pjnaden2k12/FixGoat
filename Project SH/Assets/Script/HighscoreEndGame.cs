using UnityEngine;

public class HighscoreCalculator : MonoBehaviour
{
    public int level; // Mức độ màn chơi, từ 1 đến 5
    public int totalGear; // Tổng số gear có được
    public float completionTime; // Thời gian hoàn thành màn chơi (tính bằng giây)

    public int CalculateScore()
    {
        // Tính điểm theo công thức: (gear + (level * 9) - (completionTime / 100)) * 99
        int score = (int)((totalGear + (level * 9) - (completionTime / 100)) * 99);
        return score;
    }
}
