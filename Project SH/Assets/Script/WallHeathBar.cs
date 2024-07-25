using UnityEngine;
using UnityEngine.UI;

public class WallHealthBar : MonoBehaviour
{
    public WallDenfense wall;
    public Image healthBarFill;

    void Update()
    {
        UpdateHealthBar();
    }

    void UpdateHealthBar()
    {
        float healthPercent = wall.currentHealth / wall.baseHealth;
        healthBarFill.fillAmount = healthPercent;
    }
}
