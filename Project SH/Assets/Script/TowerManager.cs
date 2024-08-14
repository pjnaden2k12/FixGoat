using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Để kiểm tra tên cảnh
using System.IO;
public class TowerManager : MonoBehaviour
{
    public static TowerManager Instance { get; private set; }
    private static bool isInMenuScene = false;

    [System.Serializable]
    public class TowerData
    {
        public int id;
        public bool isUnlocked;
        public float baseDamage;
        public float baseAttackSpeed;
        public float baseRange;
        public Sprite towerSprite;
        public GameObject towerPrefab; // Prefab của tháp
    }

    public TowerData[] towers;
    private string saveFilePath;
    private List<Tower> allTowers = new List<Tower>(); // Danh sách tháp để cập nhật

    private void Awake()
    {
        // Kiểm tra nếu đã có instance tồn tại chưa
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            // Kiểm tra xem đang ở menu không
            if (isInMenuScene)
            {
                Destroy(gameObject); // Xóa đối tượng nếu nó đang ở menu
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }
    }

    private void OnEnable()
    {
        // Đăng ký sự kiện để theo dõi khi chuyển cảnh
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // Hủy đăng ký sự kiện khi không còn sử dụng
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Kiểm tra xem có đang ở cảnh menu không
        if (scene.name == "MainMenuScene") // Thay đổi "MenuScene" thành tên cảnh menu của bạn
        {
            isInMenuScene = true;
        }
        else
        {
            isInMenuScene = false;
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

    public void UpdateTowerData(int id, float newBaseDamage, float newBaseAttackSpeed, float newBaseRange)
    {
        TowerData towerData = GetTowerDataById(id);
        if (towerData != null)
        {
            towerData.baseDamage = newBaseDamage;
            towerData.baseAttackSpeed = newBaseAttackSpeed;
            towerData.baseRange = newBaseRange;
            SaveTowerData();
            NotifyTowersOfUpdate();
        }
    }

    private void SaveTowerData()
    {
        List<string> lines = new List<string>();
        for (int i = 0; i < towers.Length; i++)
        {
            var tower = towers[i];
            string towerData = $"{tower.id},{tower.isUnlocked},{tower.baseDamage},{tower.baseAttackSpeed},{tower.baseRange}";
            lines.Add(towerData);

            // Lưu vào PlayerPrefs
            PlayerPrefs.SetString($"TowerData_{i}", towerData);
        }

        // Lưu vào file
        File.WriteAllLines(saveFilePath, lines);
        PlayerPrefs.Save(); // Lưu tất cả thay đổi vào PlayerPrefs
    }

    private void LoadTowerData()
    {
        bool dataLoaded = false;

        for (int i = 0; i < towers.Length; i++)
        {
            string towerData = PlayerPrefs.GetString($"TowerData_{i}", null);
            if (!string.IsNullOrEmpty(towerData))
            {
                string[] data = towerData.Split(',');
                towers[i].id = int.Parse(data[0]);
                towers[i].isUnlocked = bool.Parse(data[1]);
                towers[i].baseDamage = float.Parse(data[2]);
                towers[i].baseAttackSpeed = float.Parse(data[3]);
                towers[i].baseRange = float.Parse(data[4]);
                dataLoaded = true;
            }
        }

        // Nếu không có dữ liệu trong PlayerPrefs, load từ file
        if (!dataLoaded && File.Exists(saveFilePath))
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
            }
        }
    }


    private void NotifyTowersOfUpdate()
    {
        foreach (var tower in allTowers)
        {
            if (tower != null)
            {
                tower.UpdateStats(); // Cập nhật các tháp khi dữ liệu thay đổi
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

    public TowerData GetTowerDataById(int id)
    {
        foreach (var tower in towers)
        {
            if (tower.id == id)
            {
                return tower;
            }
        }
        Debug.LogError("Không tìm thấy tháp với ID: " + id);
        return null;
    }

    public void RegisterTower(Tower tower)
    {
        if (!allTowers.Contains(tower))
        {
            allTowers.Add(tower);
        }
    }

    public void UnregisterTower(Tower tower)
    {
        if (allTowers.Contains(tower))
        {
            allTowers.Remove(tower);
        }
    }
}