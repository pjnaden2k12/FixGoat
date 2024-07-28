using UnityEngine;
using UnityEngine.UI;
using TMPro; // Import the TextMesh Pro namespace

public class FortressHealthUI : MonoBehaviour
{
    public FortressHealth fortressHealth; // Reference to the FortressHealth script
    public Slider healthSlider; // Reference to the UI Slider
    public TextMeshProUGUI healthText; // Reference to the TextMesh Pro UI component

    void Start()
    {
        // Set the maximum value for the health slider
        healthSlider.maxValue = fortressHealth.maxHealth;
        UpdateHealthUI();
    }

    void Update()
    {
        UpdateHealthUI();
    }

    void UpdateHealthUI()
    {
        // Update the health slider and the TextMeshPro text component
        healthSlider.value = fortressHealth.CurrentHealth;
        healthText.text = "Health: " + fortressHealth.CurrentHealth + "/" + fortressHealth.maxHealth;
    }
}
