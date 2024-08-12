using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradePanel : MonoBehaviour
{
    public Button upgradeButton;
    public TextMeshProUGUI infoText;
    private int positionIndex;

    private void Start()
    {
        upgradeButton.onClick.AddListener(OnUpgradeButtonClicked);
    }

    public void Setup(int positionIndex)
    {
        this.positionIndex = positionIndex;
        infoText.text = $"Nâng cấp tháp tại vị trí {positionIndex + 1}?";
    }

    private void OnUpgradeButtonClicked()
    {
        // Xử lý nâng cấp tháp tại vị trí
        Debug.Log("Nâng cấp tháp.");
        gameObject.SetActive(false); // Ẩn panel sau khi nâng cấp
    }
}
