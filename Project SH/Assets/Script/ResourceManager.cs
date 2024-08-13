using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement; // Để kiểm tra tên cảnh
public class ResourceManager : MonoBehaviour
{
    public string sceneToPreserve;

    public static ResourceManager Instance { get; private set; }
    
    public int gold { get; private set; }
    public int diamonds { get; private set; }
    public int towerPieces { get; private set; }
    public int universalStones { get; private set; }

    // Chỉ số vĩnh viễn cho tường thành
    public int wallHealthBonus { get; private set; }
    public int wallDefenseBonus { get; private set; }
    public int healthRegenBonus { get; private set; }

    // Chỉ số vĩnh viễn cho các tháp
    public float towerDamageBonus { get; private set; }
    public float towerAttackSpeedBonus { get; private set; }

    // Tiến hóa
    public int evolutionLevel { get; private set; } = 0;
    public int evolveCost = 1000; // Chi phí tiến hóa cơ bản
    public int maxEvolutionLevel = 10; // Cấp độ tiến hóa tối đa

    // Định nghĩa sự kiện để thông báo UI cập nhật
    public delegate void ResourceChanged();
    public event ResourceChanged OnResourceChanged;

    private string resourcesFilePath;

    private void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
            // Kiểm tra tên cảnh hiện tại
            string currentScene = SceneManager.GetActiveScene().name;
            if (currentScene == sceneToPreserve)
            {
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }


            // Xác định đường dẫn tệp văn bản
            string folderName = "resourceSave";
            string folderPath = Path.Combine(Application.persistentDataPath, folderName);
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            resourcesFilePath = Path.Combine(folderPath, "resources.txt");

            // Tải dữ liệu từ tệp
            LoadResources();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void LoadResources()
    {
        // Kiểm tra và tải dữ liệu từ PlayerPrefs trước
        if (PlayerPrefs.HasKey("Gold"))
        {
            gold = PlayerPrefs.GetInt("Gold");
            diamonds = PlayerPrefs.GetInt("Diamonds");
            towerPieces = PlayerPrefs.GetInt("TowerPieces");
            universalStones = PlayerPrefs.GetInt("UniversalStones");
            wallHealthBonus = PlayerPrefs.GetInt("WallHealthBonus");
            wallDefenseBonus = PlayerPrefs.GetInt("WallDefenseBonus");
            healthRegenBonus = PlayerPrefs.GetInt("HealthRegenBonus");
            towerDamageBonus = PlayerPrefs.GetFloat("TowerDamageBonus");
            towerAttackSpeedBonus = PlayerPrefs.GetFloat("TowerAttackSpeedBonus");
            evolutionLevel = PlayerPrefs.GetInt("EvolutionLevel");
        }
        else if (File.Exists(resourcesFilePath))
        {
            // Nếu không có trong PlayerPrefs, tải từ file
            string[] lines = File.ReadAllLines(resourcesFilePath);
            if (lines.Length >= 10)
            {
                gold = int.Parse(lines[0].Split(':')[1].Trim());
                diamonds = int.Parse(lines[1].Split(':')[1].Trim());
                towerPieces = int.Parse(lines[2].Split(':')[1].Trim());
                universalStones = int.Parse(lines[3].Split(':')[1].Trim());
                wallHealthBonus = int.Parse(lines[4].Split(':')[1].Trim());
                wallDefenseBonus = int.Parse(lines[5].Split(':')[1].Trim());
                healthRegenBonus = int.Parse(lines[6].Split(':')[1].Trim());
                towerDamageBonus = float.Parse(lines[7].Split(':')[1].Trim());
                towerAttackSpeedBonus = float.Parse(lines[8].Split(':')[1].Trim());
                evolutionLevel = int.Parse(lines[9].Split(':')[1].Trim());
            }
        }
        else
        {
            // Thiết lập giá trị mặc định nếu không có tệp hoặc PlayerPrefs
            gold = 5000;
            diamonds = 1000;
            towerPieces = 1000;
            universalStones = 0;
            wallHealthBonus = 0;
            wallDefenseBonus = 0;
            healthRegenBonus = 0;
            towerDamageBonus = 0;
            towerAttackSpeedBonus = 0;
            evolutionLevel = 0;
        }

        NotifyResourceChanged();
    }


    private void SaveResources()
    {
        // Lưu vào file
        string data = $"Gold: {gold}\n" +
                      $"Diamonds: {diamonds}\n" +
                      $"Tower Pieces: {towerPieces}\n" +
                      $"Universal Stones: {universalStones}\n" +
                      $"Wall Health Bonus: {wallHealthBonus}\n" +
                      $"Wall Defense Bonus: {wallDefenseBonus}\n" +
                      $"Health Regen Bonus: {healthRegenBonus}\n" +
                      $"Tower Damage Bonus: {towerDamageBonus}\n" +
                      $"Tower Attack Speed Bonus: {towerAttackSpeedBonus}\n" +
                      $"Evolution Level: {evolutionLevel}";

        File.WriteAllText(resourcesFilePath, data);

        // Lưu vào PlayerPrefs
        PlayerPrefs.SetInt("Gold", gold);
        PlayerPrefs.SetInt("Diamonds", diamonds);
        PlayerPrefs.SetInt("TowerPieces", towerPieces);
        PlayerPrefs.SetInt("UniversalStones", universalStones);
        PlayerPrefs.SetInt("WallHealthBonus", wallHealthBonus);
        PlayerPrefs.SetInt("WallDefenseBonus", wallDefenseBonus);
        PlayerPrefs.SetInt("HealthRegenBonus", healthRegenBonus);
        PlayerPrefs.SetFloat("TowerDamageBonus", towerDamageBonus);
        PlayerPrefs.SetFloat("TowerAttackSpeedBonus", towerAttackSpeedBonus);
        PlayerPrefs.SetInt("EvolutionLevel", evolutionLevel);

        PlayerPrefs.Save(); // Lưu tất cả thay đổi vào PlayerPrefs
    }


    private void OnApplicationQuit()
    {
        SaveResources();
    }

    public void AddGold(int amount)
    {
        gold += amount;
        NotifyResourceChanged();
        SaveResources();
    }

    public void SpendGold(int amount)
    {
        if (gold >= amount)
        {
            gold -= amount;
            NotifyResourceChanged();
            SaveResources();
        }
        else
        {
            Debug.LogWarning("Không đủ vàng.");
        }
    }

    public void AddDiamonds(int amount)
    {
        diamonds += amount;
        NotifyResourceChanged();
        SaveResources();
    }

    public void SpendDiamonds(int amount)
    {
        if (diamonds >= amount)
        {
            diamonds -= amount;
            NotifyResourceChanged();
            SaveResources();
        }
        else
        {
            Debug.LogWarning("Không đủ kim cương.");
        }
    }

    public void AddTowerPieces(int amount)
    {
        towerPieces += amount;
        NotifyResourceChanged();
        SaveResources();
    }

    public void SpendTowerPieces(int amount)
    {
        if (towerPieces >= amount)
        {
            towerPieces -= amount;
            NotifyResourceChanged();
            SaveResources();
        }
        else
        {
            Debug.LogWarning("Không đủ mảnh tháp.");
        }
    }

    public int GetTowerPieces()
    {
        return towerPieces;
    }

    public void AddUniversalStone(int amount)
    {
        universalStones += amount;
        NotifyResourceChanged();
        SaveResources();
    }

    public void UpgradeAllTowers()
    {
        if (universalStones > 0)
        {
            universalStones--; // Tiêu đá vạn năng
            towerDamageBonus += 5; // Tăng sát thương của tháp
            towerAttackSpeedBonus += 0.1f; // Tăng tốc độ tấn công của tháp
            NotifyResourceChanged();
            SaveResources();
        }
        else
        {
            Debug.LogWarning("Không đủ đá vạn năng!");
        }
    }

    public int GetUniversalStoneCount()
    {
        return universalStones;
    }

    public void Evolve()
    {
        if (evolutionLevel >= maxEvolutionLevel)
        {
            Debug.LogWarning("Đã đạt cấp độ tiến hóa tối đa.");
            return;
        }

        int cost = evolveCost * (evolutionLevel + 1);
        if (gold >= cost)
        {
            SpendGold(cost);
            evolutionLevel++;
            UpdateWallBonuses();
            NotifyResourceChanged();
            SaveResources();
        }
        else
        {
            Debug.LogWarning("Không đủ vàng để tiến hóa.");
        }
    }

    private void UpdateWallBonuses()
    {
        wallHealthBonus = 100 * evolutionLevel;
        wallDefenseBonus = 50 * evolutionLevel;
        healthRegenBonus = 10 * evolutionLevel;
    }

    public void ExchangeTowerPiecesForUniversalStone()
    {
        if (towerPieces >= 100)
        {
            towerPieces -= 100;
            AddUniversalStone(1);
            SaveResources();
        }
        else
        {
            Debug.LogWarning("Không đủ mảnh tháp để đổi.");
        }
    }

    private void NotifyResourceChanged()
    {
        OnResourceChanged?.Invoke();
    }
    public void ClearResources()
    {
        // Xóa dữ liệu trong PlayerPrefs
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();

        // Xóa file lưu trữ nếu cần
        if (File.Exists(resourcesFilePath))
        {
            File.Delete(resourcesFilePath);
        }
    }


}
