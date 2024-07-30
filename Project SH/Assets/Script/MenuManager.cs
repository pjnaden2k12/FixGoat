using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    public float slideDuration = 0.5f; // Thời gian cho hiệu ứng slide
    public float panelWidth = 360f; // Độ rộng của panel (tùy thuộc vào kích thước của bạn)
    private Vector3 hiddenPosition = new Vector3(-1600, 0, 0); // Vị trí ẩn các panel bên trái

    private RectTransform currentActivePanel;
    private Button currentLockedButton;
    private bool isTransitioning = false; // Flag để theo dõi quá trình chuyển đổi

    private void Start()
    {
        HideAllPanels();

        // Gán sự kiện cho các nút bấm
        shopButton.onClick.AddListener(() => OnButtonClicked(shopButton, shopPanel));
        equipmentButton.onClick.AddListener(() => OnButtonClicked(equipmentButton, equipmentPanel));
        battleButton.onClick.AddListener(GoToMainMenu);
        gachaButton.onClick.AddListener(() => OnButtonClicked(gachaButton, gachaPanel));
        leaderboardButton.onClick.AddListener(() => OnButtonClicked(leaderboardButton, leaderboardPanel));
    }

    private void OnButtonClicked(Button clickedButton, RectTransform targetPanel)
    {
        if (!isTransitioning)
        {
            if (currentLockedButton != null && currentLockedButton != clickedButton)
            {
                currentLockedButton.interactable = true; // Mở khóa nút hiện tại nếu nó không phải là nút vừa được bấm
            }

            currentLockedButton = clickedButton;
            currentLockedButton.interactable = false; // Khóa nút vừa được bấm
            ShowPanel(targetPanel);
        }
    }

    private void ShowPanel(RectTransform targetPanel)
    {
        if (currentActivePanel != null)
        {
            LeanTween.move(currentActivePanel, hiddenPosition, slideDuration).setOnComplete(() =>
            {
                currentActivePanel.gameObject.SetActive(false);
                currentActivePanel = null;
                ActivatePanel(targetPanel);
            });
        }
        else
        {
            ActivatePanel(targetPanel);
        }
    }

    private void ActivatePanel(RectTransform targetPanel)
    {
        if (targetPanel != null)
        {
            targetPanel.gameObject.SetActive(true);
            targetPanel.anchoredPosition = new Vector2(panelWidth, 0);
            LeanTween.move(targetPanel, Vector2.zero, slideDuration).setOnComplete(() =>
            {
                currentActivePanel = targetPanel;
                isTransitioning = false; // Mở khóa các nút sau khi chuyển đổi hoàn tất
            });
        }
        else
        {
            isTransitioning = false; // Đảm bảo flag được hạ nếu không có panel mới
        }
    }

    private void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    private void HideAllPanels()
    {
        shopPanel.anchoredPosition = hiddenPosition;
        equipmentPanel.anchoredPosition = hiddenPosition;
        gachaPanel.anchoredPosition = hiddenPosition;
        leaderboardPanel.anchoredPosition = hiddenPosition;

        shopPanel.gameObject.SetActive(false);
        equipmentPanel.gameObject.SetActive(false);
        gachaPanel.gameObject.SetActive(false);
        leaderboardPanel.gameObject.SetActive(false);
    }
}
