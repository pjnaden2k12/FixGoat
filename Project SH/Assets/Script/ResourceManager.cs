using UnityEngine;

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

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // Thiết lập giá trị ban đầu cho các tài nguyên
            gold = 99999;
            diamonds = 1999;
            towerPieces = 1000;
            wallHealthBonus = 0;
            wallDefenseBonus = 0;
            healthRegenBonus = 0;
            towerDamageBonus = 0;
            towerAttackSpeedBonus = 0;
        }
        else
        {
            Destroy(gameObject);
        }
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
}
