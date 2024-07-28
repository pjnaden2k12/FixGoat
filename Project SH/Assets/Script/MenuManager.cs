using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public Button shopButton; // Nút Shop
    public Button equipmentButton; // Nút Equipment
    public Button battleButton; // Nút Battle
    public Button towerButton; // Nút Tower
    public Button leaderboardButton; // Nút Leaderboard

    void Start()
    {
        // Gán sự kiện cho các nút
      
        shopButton.onClick.AddListener(OpenShop);
        equipmentButton.onClick.AddListener(OpenEquipment);
        battleButton.onClick.AddListener(OpenBattle);
        towerButton.onClick.AddListener(OpenTower);
        leaderboardButton.onClick.AddListener(OpenLeaderboard);
    }


    void OpenShop()
    {
        // Mở màn hình Shop
        SceneManager.LoadScene("ShopScene"); // Thay "ShopScene" bằng tên scene của Shop
    }

    void OpenEquipment()
    {
        // Mở màn hình Equipment
        SceneManager.LoadScene("EquipmentScene"); // Thay "EquipmentScene" bằng tên scene của Equipment
    }

    void OpenBattle()
    {
        
        SceneManager.LoadScene("MapScene"); 
    }

    void OpenTower()
    {
        // Mở màn hình Tower
        SceneManager.LoadScene("TowerScene"); // Thay "TowerScene" bằng tên scene của Tower
    }

    void OpenLeaderboard()
    {
        // Mở màn hình Leaderboard
        SceneManager.LoadScene("LeaderboardScene"); // Thay "LeaderboardScene" bằng tên scene của Leaderboard
    }
}
