using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance { get; private set; }

    public int gold { get; private set; }
    public int diamonds { get; private set; }
    public int towerPieces { get; private set; }

    // Chỉ số vĩnh viễn
    public int wallHealthBonus { get; private set; }
    public int wallDefenseBonus { get; private set; }
    public int healthRegenBonus { get; private set; }

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
            gold = 999999;
            diamonds = 10;
            towerPieces = 100;
            wallHealthBonus = 0;
            wallDefenseBonus = 0;
            healthRegenBonus = 0;
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

    // Thêm mảnh tường
    public void AddTowerPieces(int amount)
    {
        towerPieces += amount;
        NotifyResourceChanged();
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

    // Cập nhật các chỉ số vĩnh viễn khi tiến hóa
    private void UpdateWallBonuses()
    {
        // Cập nhật các chỉ số vĩnh viễn tương ứng với cấp độ tiến hóa
        // Ví dụ: Giả sử mỗi cấp tiến hóa tăng chỉ số thêm 10%
        wallHealthBonus = 100 * evolutionLevel;
        wallDefenseBonus = 50 * evolutionLevel;
        healthRegenBonus = 10 * evolutionLevel;
    }

    // Thông báo sự thay đổi tài nguyên
    private void NotifyResourceChanged()
    {
        OnResourceChanged?.Invoke();
    }
}
