using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance { get; private set; }

    public int gold { get; private set; }
    public int diamonds { get; private set; }
    public int towerPieces { get; private set; }

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
            gold = 500;
            diamonds = 10;
            towerPieces = 100;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddGold(int amount)
    {
        gold += amount;
        NotifyResourceChanged();
    }

    public void SpendGold(int amount)
    {
        if (gold >= amount)
        {
            gold -= amount;
            NotifyResourceChanged();
        }
        else
        {
            // Xử lý khi không đủ vàng
        }
    }

    public void AddDiamonds(int amount)
    {
        diamonds += amount;
        NotifyResourceChanged();
    }

    public void SpendDiamonds(int amount)
    {
        if (diamonds >= amount)
        {
            diamonds -= amount;
            NotifyResourceChanged();
        }
        else
        {
            // Xử lý khi không đủ kim cương
        }
    }

    public void AddTowerPieces(int amount)
    {
        towerPieces += amount;
        NotifyResourceChanged();
    }

    private void NotifyResourceChanged()
    {
        // Kích hoạt sự kiện thông báo thay đổi tài nguyên
        OnResourceChanged?.Invoke();
    }
}
