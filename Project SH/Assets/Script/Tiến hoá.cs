using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EvolutionUIManager : MonoBehaviour
{
    public TextMeshProUGUI evolutionLevelText;
    public TextMeshProUGUI wallHealthBonusText;
    public TextMeshProUGUI wallDefenseBonusText;
    public TextMeshProUGUI healthRegenBonusText;
    public Image characterImage;
    public Button evolveButton;
    public TextMeshProUGUI evolveButtonText;

    public Sprite characterSpriteLevel1;
    public Sprite characterSpriteLevel2;
    public Sprite characterSpriteLevel5;
    public Sprite characterSpriteLevel10;

    private void Start()
    {
        UpdateUI();
        evolveButton.onClick.AddListener(OnEvolveButtonClick);
        ResourceManager.Instance.OnResourceChanged += UpdateUI;
    }

    private void OnEvolveButtonClick()
    {
        ResourceManager.Instance.Evolve();
    }

    private void UpdateUI()
    {
        evolutionLevelText.text = "Tiến Hoá Cấp: " + ResourceManager.Instance.evolutionLevel;
        wallHealthBonusText.text = "Máu Thêm: " + ResourceManager.Instance.wallHealthBonus;
        wallDefenseBonusText.text = "Thủ Thêm: " + ResourceManager.Instance.wallDefenseBonus;
        healthRegenBonusText.text = "Hồi Máu Thêm: " + ResourceManager.Instance.healthRegenBonus;

        characterImage.sprite = GetCharacterSpriteForLevel(ResourceManager.Instance.evolutionLevel);

        if (ResourceManager.Instance.evolutionLevel >= ResourceManager.Instance.maxEvolutionLevel)
        {
            evolveButtonText.text = "Cấp Tối Đa";
            evolveButton.interactable = false;
        }
        else
        {
            evolveButtonText.text = "Tiến Hoá";
            evolveButton.interactable = true;
        }
    }

    private Sprite GetCharacterSpriteForLevel(int level)
    {
        if (level >= 10)
            return characterSpriteLevel10;
        else if (level >= 5)
            return characterSpriteLevel5;
        else if (level >= 2)
            return characterSpriteLevel2;
        else
            return characterSpriteLevel1;
    }
}
