using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class SpinManager : MonoBehaviour
{
   
    public Button spinButton; // Nút quay
    public Button exchangeButton; // Nút đổi mảnh tháp lấy đá vạn năng
    public TextMeshProUGUI universalStoneText; // Hiển thị số đá vạn năng
    public Image universalStoneImage; // Hình ảnh biểu tượng đá vạn năng

    public GameObject rewardPanel; // Panel hiển thị phần thưởng
    //public Image rewardImage; // Hình ảnh phần thưởng trong rewardPanel
    public TextMeshProUGUI rewardText; // Text hiển thị số lượng phần thưởng
    public Button closeButton; // Nút thoát rewardPanel

    public Image chestImage; // Hình ảnh rương quay

    private const int spinCost = 50; // Chi phí mỗi lần quay
    private const float buttonResetDelay = 0.5f; // Thời gian khóa nút quay (2 giây)


    void Start()
    {
        spinButton.onClick.AddListener(PerformSpin);
        UpdateUniversalStoneUI(); // Cập nhật UI đá vạn năng ngay khi bắt đầu
        exchangeButton.onClick.AddListener(ExchangeTowerPiecesForUniversalStone);
        closeButton.onClick.AddListener(CloseRewardPanel);
        rewardPanel.SetActive(false); // Ẩn rewardPanel khi bắt đầu

    }

    void PerformSpin()
    {
        if (ResourceManager.Instance.diamonds >= spinCost)
        {
            ResourceManager.Instance.SpendDiamonds(spinCost);

            // Hiển thị hiệu ứng rung cho rương
            // Khóa nút quay và bắt đầu hiệu ứng rung
            spinButton.interactable = false;
            StartCoroutine(ChestShakeEffect());

            // Đặt delay để hiển thị rewardPanel sau 2 giây
            StartCoroutine(DisplayRewardPanelAfterDelay(1.5f));
        }
        else
        {
            Debug.LogWarning("Không đủ kim cương!");
        }
    }

    IEnumerator ChestShakeEffect()
    {
        
        Vector3 originalPosition = chestImage.transform.position;
        float shakeDuration = 1f; // Thời gian rung
        float elapsedTime = 0f;
        float shakeMagnitude = 0.2f; // Độ mạnh của rung

        while (elapsedTime < shakeDuration)
        {
            float xOffset = Random.Range(-shakeMagnitude, shakeMagnitude);
            float yOffset = Random.Range(-shakeMagnitude, shakeMagnitude);
            chestImage.transform.position = new Vector3(originalPosition.x + xOffset, originalPosition.y + yOffset, originalPosition.z);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Đặt lại vị trí rương về vị trí gốc
        chestImage.transform.position = originalPosition;
    }

    IEnumerator DisplayRewardPanelAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        (string reward, Sprite rewardSprite) = GetRandomReward();
        rewardText.text = "" + reward;
        //rewardImage.sprite = rewardSprite; // Cập nhật hình ảnh phần thưởng

        // Hiển thị panel phần thưởng
        rewardPanel.SetActive(true);
        UpdateUniversalStoneUI(); // Cập nhật số đá vạn năng sau mỗi lần quay
        // Mở lại nút quay sau 2 giây
        yield return new WaitForSeconds(buttonResetDelay);
        spinButton.interactable = true;
    }

    (string, Sprite) GetRandomReward()
    {
        int randomValue = Random.Range(0, 100);
        string rewardDescription = "";
        Sprite rewardSprite = null;

        if (randomValue < 50)
        {
            ResourceManager.Instance.AddTowerPieces(10);
            rewardDescription = "10 piece tower";
            rewardSprite = Resources.Load<Sprite>("Path/To/TowerPieces10Image");
        }
        else if (randomValue < 80)
        {
            ResourceManager.Instance.AddTowerPieces(20);
            rewardDescription = "20 piece tower";
            rewardSprite = Resources.Load<Sprite>("Path/To/TowerPieces20Image");
        }
        else if (randomValue < 95)
        {
            ResourceManager.Instance.AddTowerPieces(50);
            rewardDescription = "50 piece tower";
            rewardSprite = Resources.Load<Sprite>("Path/To/TowerPieces50Image");
        }
        else
        {
            ResourceManager.Instance.AddUniversalStone(1); // Thêm 1 đá vạn năng
            rewardDescription = "Stone color";
            rewardSprite = Resources.Load<Sprite>("Path/To/UniversalStoneImage");
        }

        return (rewardDescription, rewardSprite);
    }
    void ExchangeTowerPiecesForUniversalStone()
    {
        ResourceManager.Instance.ExchangeTowerPiecesForUniversalStone();
        UpdateUniversalStoneUI(); // Cập nhật UI đá vạn năng sau khi đổi
    }
    void UpdateUniversalStoneUI()
    {
        int universalStones = ResourceManager.Instance.GetUniversalStoneCount();
        universalStoneText.text = "" + universalStones;
    }
    void CloseRewardPanel()
    {
        rewardPanel.SetActive(false);
    }
}
