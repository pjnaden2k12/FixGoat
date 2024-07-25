using UnityEngine;
using UnityEngine.UI;

public class BossHealthUI : MonoBehaviour
{
    public Image healthBarFill; // Image của thanh máu
    public float updateSpeed = 0.5f; // Tốc độ cập nhật thanh máu

    private BossHealth bossHealth; // Tham chiếu đến script BossHealth
    private float targetHealthFillAmount;

    void Start()
    {
        bossHealth = GetComponent<BossHealth>();
        if (bossHealth != null)
        {
            // Cập nhật thanh máu ngay khi boss xuất hiện
            UpdateHealthBar();
        }
    }

    void Update()
    {
        if (healthBarFill != null)
        {
            // Cập nhật thanh máu từ từ để mượt mà
            healthBarFill.fillAmount = Mathf.Lerp(healthBarFill.fillAmount, targetHealthFillAmount, Time.deltaTime * updateSpeed);
        }
    }

    public void UpdateHealthBar()
    {
        if (bossHealth != null && healthBarFill != null)
        {
            float healthPercentage = bossHealth.GetHealthPercentage();
            targetHealthFillAmount = healthPercentage;
        }
    }
}
