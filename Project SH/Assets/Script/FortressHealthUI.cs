using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FortressHealthUI : MonoBehaviour
{
    public FortressHealth fortressHealth; // Reference to the FortressHealth script
    public Slider healthSlider; // Reference to the UI Slider
    public TextMeshProUGUI healthText; // Reference to the TextMesh Pro UI component

    void Start()
    {
        if (fortressHealth == null)
        {
            Debug.LogError("FortressHealth chưa được gán!");
            return;
        }

        // Set the maximum value for the health slider
        healthSlider.maxValue = fortressHealth.MaxHealth; // Cập nhật thanh trượt với giá trị hiện tại
        healthSlider.value = fortressHealth.CurrentHealth; // Ensure the slider starts at the correct value
        UpdateHealthUI();
    }

    void Update()
    {
        UpdateHealthUI();
    }

    void UpdateHealthUI()
    {
        if (fortressHealth == null)
        {
            Debug.LogError("FortressHealth chưa được gán!");
            return;
        }

        // Cập nhật thanh trượt sức khỏe và thành phần văn bản TextMeshPro
        healthSlider.value = fortressHealth.CurrentHealth;
        healthText.text = "Health: " + Mathf.RoundToInt(fortressHealth.CurrentHealth) + "/" + Mathf.RoundToInt(fortressHealth.MaxHealth);
    }
}
