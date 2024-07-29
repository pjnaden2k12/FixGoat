using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public Image shopImage;
    public Image equipmentImage;
    
    public Image gachaImage;
    public Image leaderboardImage;
    

    public Button shopButton;
    public Button equipmentButton;
    public Button battleButton;
    public Button gachaButton;
    public Button leaderboardButton;

    public Button previousLevelButton;
    public Button nextLevelButton;
    public Image levelDisplay;

    private void Start()
    {
        HideAllImages();
        // Gán sự kiện cho các nút bấm
        shopButton.onClick.AddListener(ShowShopImage);
        equipmentButton.onClick.AddListener(ShowEquipmentImage);
        battleButton.onClick.AddListener(GoToMainMenu);
        gachaButton.onClick.AddListener(ShowGachaImage);
        leaderboardButton.onClick.AddListener(ShowLeaderboardImage);

        previousLevelButton.onClick.AddListener(ShowPreviousLevel);
        nextLevelButton.onClick.AddListener(ShowNextLevel);

        
    }

    private void ShowShopImage()
    {
        HideAllImages();
        shopImage.gameObject.SetActive(true);
    }

    private void ShowEquipmentImage()
    {
        HideAllImages();
        equipmentImage.gameObject.SetActive(true);
    }


    private void ShowGachaImage()
    {
        HideAllImages();
        gachaImage.gameObject.SetActive(true);
    }

    private void ShowLeaderboardImage()
    {
        HideAllImages();
        leaderboardImage.gameObject.SetActive(true);
    }

    private void GoToMainMenu()
    {
        // Tải lại scene chính hoặc hiển thị lại màn hình chính nếu cùng trong một scene
        SceneManager.LoadScene("MainMenuScene"); // Thay "MainMenuScene" bằng tên của scene chính của bạn
    }

    private void HideAllImages()
    {
        shopImage.gameObject.SetActive(false);
        equipmentImage.gameObject.SetActive(false);
        
        gachaImage.gameObject.SetActive(false);
        leaderboardImage.gameObject.SetActive(false);
        
    }

    private void ShowPreviousLevel()
    {
        // Thực hiện logic để chuyển đến màn chơi trước đó
        Debug.Log("Showing Previous Level");
    }

    private void ShowNextLevel()
    {
        // Thực hiện logic để chuyển đến màn chơi tiếp theo
        Debug.Log("Showing Next Level");
    }
}
