using UnityEngine;
using UnityEngine.UI;

public class EvolutionUIManager : MonoBehaviour
{
    public Text evolutionLevelText;
    public Text wallHealthBonusText;
    public Text wallDefenseBonusText;
    public Text healthRegenBonusText;
    public Image characterImage; // Hình ảnh nhân vật
    public Button evolveButton;

    // Thêm sprite cho các cấp độ tiến hóa
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
        evolutionLevelText.text = "Evolution Level: " + ResourceManager.Instance.evolutionLevel;
        wallHealthBonusText.text = "Wall Health Bonus: " + ResourceManager.Instance.wallHealthBonus;
        wallDefenseBonusText.text = "Wall Defense Bonus: " + ResourceManager.Instance.wallDefenseBonus;
        healthRegenBonusText.text = "Health Regen Bonus: " + ResourceManager.Instance.healthRegenBonus;

        // Cập nhật hình ảnh nhân vật tùy theo level tiến hóa
        characterImage.sprite = GetCharacterSpriteForLevel(ResourceManager.Instance.evolutionLevel);
    }

    private Sprite GetCharacterSpriteForLevel(int level)
    {
        switch (level)
        {
            case 2:
                return characterSpriteLevel2;
            case 5:
                return characterSpriteLevel5;
            case 10:
                return characterSpriteLevel10;
            default:
                return characterSpriteLevel1;
        }
    }
}
