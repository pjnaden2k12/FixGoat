using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    public static TowerManager Instance { get; private set; }

    public Tower[] towers; // Mảng các tháp

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        
    }

   
    public void UnlockTower(int index)
    {
        if (index >= 0 && index < towers.Length)
        {
            Tower tower = towers[index];
            if (tower.IsUnlocked())
            {
                Debug.LogWarning($"Tháp {index} đã được mở khóa.");
                return;
            }

            // Kiểm tra nếu có đủ tài nguyên để mở khóa tháp
            if (ResourceManager.Instance.GetTowerPieces() >= 25)
            {
                ResourceManager.Instance.SpendTowerPieces(25);
                tower.Unlock();
                Debug.Log($"Tháp {index} đã được mở khóa.");
            }
            else
            {
                Debug.LogWarning("Không đủ mảnh tháp để mở khóa.");
            }
        }
        else
        {
            Debug.LogWarning("Chỉ số tháp không hợp lệ.");
        }
    }

    public Tower GetTower(int index)
    {
        if (index >= 0 && index < towers.Length)
        {
            return towers[index];
        }
        else
        {
            Debug.LogWarning("Chỉ số tháp không hợp lệ.");
            return null;
        }
    }

    public List<Tower> GetUnlockedTowers()
    {
        List<Tower> unlockedTowers = new List<Tower>();
        foreach (Tower tower in towers)
        {
            if (tower.IsUnlocked())
            {
                unlockedTowers.Add(tower);
            }
        }
        return unlockedTowers;
    }
}
