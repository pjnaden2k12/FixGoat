using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private float levelCompletionTime; // Thời gian hoàn thành cấp độ
    private int currentLevelId; // ID của cấp độ hiện tại

    private void Awake()
    {
        // Đảm bảo chỉ có một GameManager trong cảnh
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Gán thời gian hoàn thành cấp độ
    public void SetLevelCompletionTime(float time)
    {
        levelCompletionTime = time;
    }

    // Lấy thời gian hoàn thành cấp độ
    public float GetLevelCompletionTime()
    {
        return levelCompletionTime;
    }

    // Gán ID cấp độ hiện tại
    public void SetCurrentLevelId(int id)
    {
        currentLevelId = id;
    }

    // Lấy ID cấp độ hiện tại
    public int GetCurrentLevelId()
    {
        return currentLevelId;
    }
}
