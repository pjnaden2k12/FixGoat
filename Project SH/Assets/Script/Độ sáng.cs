using UnityEngine;
using UnityEngine.UI;

public class BrightnessControl : MonoBehaviour
{
    public Image brightnessOverlay; // Image Overlay cho Brightness
    public Slider brightnessSlider; // Slider để điều chỉnh Brightness
    public float maxAlpha = 0.8f; // Mức alpha tối đa

    private void Start()
    {
        if (brightnessOverlay != null && brightnessSlider != null)
        {
            // Đặt giá trị ban đầu của Slider bằng alpha hiện tại của Image
            brightnessSlider.value = brightnessOverlay.color.a;

            // Đăng ký sự kiện cho Slider
            brightnessSlider.onValueChanged.AddListener(UpdateBrightness);
        }
    }

    // Hàm này sẽ được gọi khi Slider thay đổi giá trị
    private void UpdateBrightness(float value)
    {
        if (brightnessOverlay != null)
        {
            Color color = brightnessOverlay.color;
            color.a = Mathf.Clamp(value, 0, maxAlpha); // Giới hạn alpha không vượt quá maxAlpha
            brightnessOverlay.color = color;
        }
    }
}
