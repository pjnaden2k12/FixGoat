using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public RectTransform shopPanel;
    public RectTransform equipmentPanel;
    public RectTransform gachaPanel;
    public RectTransform leaderboardPanel;

    public Button shopButton;
    public Button equipmentButton;
    public Button battleButton;
    public Button gachaButton;
    public Button leaderboardButton;

    private RectTransform currentActivePanel;
    private Button currentLockedButton;

    private void Start()
    {
        HideAllPanels();

        // Gán sự kiện cho các nút bấm
        shopButton.onClick.AddListener(() => OnButtonClicked(shopButton, shopPanel));
        equipmentButton.onClick.AddListener(() => OnButtonClicked(equipmentButton, equipmentPanel));
        battleButton.onClick.AddListener(BackToMainMenu); // Nút "Chiến đấu" quay lại trang đầu
        gachaButton.onClick.AddListener(() => OnButtonClicked(gachaButton, gachaPanel));
        leaderboardButton.onClick.AddListener(() => OnButtonClicked(leaderboardButton, leaderboardPanel));
    }

    private void OnButtonClicked(Button clickedButton, RectTransform targetPanel)
    {
        // Ẩn panel hiện tại
        if (currentActivePanel != null)
        {
            currentActivePanel.gameObject.SetActive(false);
        }

        // Hiển thị panel mục tiêu
        targetPanel.gameObject.SetActive(true);
        currentActivePanel = targetPanel;

        // Cập nhật trạng thái nút
        if (currentLockedButton != null)
        {
            currentLockedButton.interactable = true;
        }
        currentLockedButton = clickedButton;
        currentLockedButton.interactable = false;
    }

    private void BackToMainMenu()
    {
        // Ẩn panel hiện tại và hiển thị trang đầu
        if (currentActivePanel != null)
        {
            currentActivePanel.gameObject.SetActive(false);
        }

        // Mở khóa nút hiện tại
        if (currentLockedButton != null)
        {
            currentLockedButton.interactable = true;
            currentLockedButton = null;
        }
    }

    private void HideAllPanels()
    {
        shopPanel.gameObject.SetActive(false);
        equipmentPanel.gameObject.SetActive(false);
        gachaPanel.gameObject.SetActive(false);
        leaderboardPanel.gameObject.SetActive(false);
    }
}
