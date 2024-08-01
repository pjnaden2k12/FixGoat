using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public GameObject goldShopPanel;
    public GameObject diamondShopPanel;
    public Button openGoldShopButton;
    public Button openDiamondShopButton;
    public Button[] transactionButtons; // Các nút đại diện cho các giao dịch

    private int[] goldAmounts = { 1000, 2000, 3000, 4000, 5000 }; // Số vàng cho mỗi giao dịch
    private int[] diamondRewards = { 10, 25, 40, 60, 90 }; // Số kim cương nhận được

    void Start()
    {
        ShowGoldShop(); // Mở shop vàng mặc định

        // Đăng ký sự kiện khi tài nguyên thay đổi để cập nhật UI
        ResourceManager.Instance.OnResourceChanged += UpdateResourceUI;

        // Gán sự kiện cho các nút giao dịch
        for (int i = 0; i < transactionButtons.Length; i++)
        {
            int index = i; // Capture the index in a local variable
            transactionButtons[i].onClick.AddListener(() => ExchangeGoldForDiamonds(index));
        }

        // Đăng ký sự kiện cho các nút mở panel shop
        openGoldShopButton.onClick.AddListener(ShowGoldShop);
        openDiamondShopButton.onClick.AddListener(ShowDiamondShop);
    }

    void OnDestroy()
    {
        // Hủy đăng ký sự kiện khi đối tượng này bị phá hủy
        if (ResourceManager.Instance != null)
        {
            ResourceManager.Instance.OnResourceChanged -= UpdateResourceUI;
        }
    }

    public void ShowGoldShop()
    {
        goldShopPanel.SetActive(true);
        diamondShopPanel.SetActive(false);
        openGoldShopButton.interactable = false; // Khóa nút vàng khi đang ở panel vàng
        openDiamondShopButton.interactable = true; // Mở khóa nút kim cương
    }

    public void ShowDiamondShop()
    {
        goldShopPanel.SetActive(false);
        diamondShopPanel.SetActive(true);
        openGoldShopButton.interactable = true; // Mở khóa nút vàng
        openDiamondShopButton.interactable = false; // Khóa nút kim cương khi đang ở panel kim cương
    }

    public void ExchangeGoldForDiamonds(int transactionIndex)
    {
        if (transactionIndex < 0 || transactionIndex >= goldAmounts.Length)
        {
            Debug.LogWarning("Giao dịch không hợp lệ!");
            return;
        }

        int goldAmount = goldAmounts[transactionIndex];
        int diamonds = diamondRewards[transactionIndex];

        if (ResourceManager.Instance.gold >= goldAmount)
        {
            ResourceManager.Instance.SpendGold(goldAmount);
            ResourceManager.Instance.AddDiamonds(diamonds);
        }
        else
        {
            Debug.LogWarning("Không đủ vàng!");
        }
    }

    void UpdateResourceUI()
    {
        // Cập nhật UI tài nguyên nếu cần
        // Ví dụ: Refresh resource-related visuals if needed
    }
}
