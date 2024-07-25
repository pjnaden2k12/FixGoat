using UnityEngine.TextCore.Text;
using UnityEngine;

public class WallDenfense : MonoBehaviour
{
    public float baseHealth = 100f;
    public float baseDefense = 10f;
    public float baseHealingRate = 1f;
    public float currentHealth;

    private Character character;

    void Start()
    {
        character = FindObjectOfType<Character>();
        currentHealth = baseHealth;
    }

    void Update()
    {
        (float healthBoost, float defenseBoost, float healingBoost) = character.CalculateWallBuffs();
        Heal(healthBoost, healingBoost);
    }

    void Heal(float healthBoost, float healingBoost)
    {
        currentHealth += (baseHealingRate + healingBoost) * Time.deltaTime;
        if (currentHealth > baseHealth + healthBoost)
        {
            currentHealth = baseHealth + healthBoost;
        }
    }
}
