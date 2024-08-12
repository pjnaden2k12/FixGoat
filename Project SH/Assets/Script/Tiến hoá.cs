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
        evolutionLevelText.text = "Level Hero: " + ResourceManager.Instance.evolutionLevel;
        wallHealthBonusText.text = "HP bonus: " + ResourceManager.Instance.wallHealthBonus;
        wallDefenseBonusText.text = "Tank Bonus: " + ResourceManager.Instance.wallDefenseBonus;
        healthRegenBonusText.text = "Heal bonus: " + ResourceManager.Instance.healthRegenBonus;

        characterImage.sprite = GetCharacterSpriteForLevel(ResourceManager.Instance.evolutionLevel);

        if (ResourceManager.Instance.evolutionLevel >= ResourceManager.Instance.maxEvolutionLevel)
        {
            
            evolveButton.interactable = false;
        }
        else
        {
            
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
