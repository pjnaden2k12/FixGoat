using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    public static TowerManager Instance { get; private set; }

    [System.Serializable]
    public class TowerData
    {
        public int id;
        public bool isUnlocked;
        public float baseDamage;
        public float baseAttackSpeed;
        public float baseRange;
        public Sprite towerSprite;
    }

    public TowerData[] towers;
    private string saveFilePath;

    private void Awake()
    {
        // Kiểm tra xem đã có instance tồn tại chưa
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Nếu có, hủy bỏ đối tượng này
        }
        else
        {
            Instance = this; // Nếu không, gán instance này
            DontDestroyOnLoad(gameObject); // Đảm bảo không bị hủy khi scene thay đổi
        }
    }

    private void Start()
    {
        string folderName = "resourceSave";
        string directoryPath = Path.Combine(Application.persistentDataPath, folderName);

        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        saveFilePath = Path.Combine(directoryPath, "towerData.txt");
        LoadTowerData();
    }

    public void UnlockTower(int index)
    {
        if (index >= 0 && index < towers.Length)
        {
            towers[index].isUnlocked = true;
            SaveTowerData();
        }
    }

    private void SaveTowerData()
    {
        List<string> lines = new List<string>();
        foreach (var tower in towers)
        {
            lines.Add($"{tower.id},{tower.isUnlocked},{tower.baseDamage},{tower.baseAttackSpeed},{tower.baseRange}");
        }
        File.WriteAllLines(saveFilePath, lines);
    }

    private void LoadTowerData()
    {
        if (File.Exists(saveFilePath))
        {
            string[] lines = File.ReadAllLines(saveFilePath);
            for (int i = 0; i < lines.Length && i < towers.Length; i++)
            {
                string[] data = lines[i].Split(',');
                towers[i].id = int.Parse(data[0]);
                towers[i].isUnlocked = bool.Parse(data[1]);
                towers[i].baseDamage = float.Parse(data[2]);
                towers[i].baseAttackSpeed = float.Parse(data[3]);
                towers[i].baseRange = float.Parse(data[4]);
                // Sprite cần được gán qua Editor hoặc Load từ Resources
            }
        }
    }

    public List<TowerData> GetUnlockedTowers()
    {
        List<TowerData> unlockedTowers = new List<TowerData>();
        foreach (var tower in towers)
        {
            if (tower.isUnlocked)
            {
                unlockedTowers.Add(tower);
            }
        }
        return unlockedTowers;
    }
}
