using UnityEngine;
using System.IO;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance { get; private set; }

    public int gold { get; private set; }
    public int diamonds { get; private set; }
    public int towerPieces { get; private set; }
    public int universalStones { get; private set; } // Đá vạn năng

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
            DontDestroyOnLoad(gameObject);

            // Xác định đường dẫn tệp văn bản
            string folderName = "resourceSave";
            string folderPath = Path.Combine(Application.persistentDataPath, folderName);
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            resourcesFilePath = Path.Combine(folderPath, "resources.txt");

            // Thiết lập giá trị ban đầu cho các tài nguyên
            LoadResources();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void LoadResources()
    {
        if (File.Exists(resourcesFilePath))
        {
            string[] lines = File.ReadAllLines(resourcesFilePath);
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
        else
        {
            // Thiết lập giá trị ban đầu nếu tệp không tồn tại
            gold = 99999;
            diamonds = 1999;
            towerPieces = 1000;
            wallHealthBonus = 0;
            wallDefenseBonus = 0;
            healthRegenBonus = 0;
            towerDamageBonus = 0;
            towerAttackSpeedBonus = 0;
        }
    }

    private void SaveResources()
    {
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
    }

    // Thêm vàng
    public void AddGold(int amount)
    {
        gold += amount;
        NotifyResourceChanged();
    }

    // Tiêu vàng
    public void SpendGold(int amount)
    {
        if (gold >= amount)
        {
            gold -= amount;
            NotifyResourceChanged();
        }
        else
        {
            Debug.LogWarning("Không đủ vàng.");
        }
    }

    // Thêm kim cương
    public void AddDiamonds(int amount)
    {
        diamonds += amount;
        NotifyResourceChanged();
    }

    // Tiêu kim cương
    public void SpendDiamonds(int amount)
    {
        if (diamonds >= amount)
        {
            diamonds -= amount;
            NotifyResourceChanged();
        }
        else
        {
            Debug.LogWarning("Không đủ kim cương.");
        }
    }

    // Thêm mảnh tháp
    public void AddTowerPieces(int amount)
    {
        towerPieces += amount;
        NotifyResourceChanged();
    }

    public void SpendTowerPieces(int amount)
    {
        if (towerPieces >= amount)
        {
            towerPieces -= amount;
            NotifyResourceChanged();
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

    // Thêm đá vạn năng
    public void AddUniversalStone(int amount)
    {
        universalStones += amount;
        NotifyResourceChanged();
    }

    // Tiêu đá vạn năng để nâng cấp tất cả các tháp
    public void UpgradeAllTowers()
    {
        if (universalStones > 0)
        {
            universalStones--; // Tiêu đá vạn năng

            // Nâng cấp tất cả các tháp
            towerDamageBonus += 5; // Tăng sát thương của tháp
            towerAttackSpeedBonus += 0.1f; // Tăng tốc độ tấn công của tháp

            NotifyResourceChanged();
        }
        else
        {
            Debug.LogWarning("Không đủ đá vạn năng!");
        }
    }

    // Lấy số lượng đá vạn năng
    public int GetUniversalStoneCount()
    {
        return universalStones;
    }

    // Tiến hóa nhân vật
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
        }
        else
        {
            Debug.LogWarning("Không đủ vàng để tiến hóa.");
        }
    }

    // Cập nhật các chỉ số vĩnh viễn cho tường thành khi tiến hóa
    private void UpdateWallBonuses()
    {
        wallHealthBonus = 100 * evolutionLevel;
        wallDefenseBonus = 50 * evolutionLevel;
        healthRegenBonus = 10 * evolutionLevel;
    }

    // Đổi 100 mảnh tháp lấy 1 đá vạn năng
    public void ExchangeTowerPiecesForUniversalStone()
    {
        if (towerPieces >= 100)
        {
            towerPieces -= 100;
            AddUniversalStone(1);
        }
        else
        {
            Debug.LogWarning("Không đủ mảnh tháp để đổi.");
        }
    }

    // Thông báo sự thay đổi tài nguyên
    private void NotifyResourceChanged()
    {
        OnResourceChanged?.Invoke();
    }

    private void OnApplicationQuit()
    {
        SaveResources();
    }
}
