using UnityEngine;
using TMPro; // Thay vì UnityEngine.UI

public class GearManager : MonoBehaviour
{
    public static GearManager Instance;

    public int totalGears = 0; // Tổng số bánh răng
    public TextMeshProUGUI gearsText; // Tham chiếu đến TextMeshProUGUI

    void Awake()
    {
        // Đảm bảo chỉ có một GearManager trong cảnh
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Giữ GearManager qua các cảnh
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Cập nhật UI khi bắt đầu trò chơi
        UpdateGearsUI();
    }

    // Phương thức để thêm bánh răng
    public void AddGears(int amount)
    {
        totalGears += amount;
        UpdateGearsUI();
    }

    // Phương thức để cập nhật TextMeshProUGUI
    void UpdateGearsUI()
    {
        if (gearsText != null)
        {
            gearsText.text = "" + totalGears;
        }
    }

    // Phương thức để sử dụng bánh răng
    public bool SpendGears(int amount)
    {
        if (totalGears >= amount)
        {
            totalGears -= amount;
            UpdateGearsUI();
            return true;
        }
        return false;
    }
    public float GetGearCount()
    {

    return totalGears; }

}
