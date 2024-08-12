using UnityEngine;

public class FireEffect : MonoBehaviour
{
    public GameObject boss; // Kéo thả đối tượng boss vào đây trong Inspector
    public float healthIncrease = 50f; // Số lượng máu tăng
    public float damageIncrease = 20f; // Số lượng sát thương tăng
    private bool effectActive = false; // Đảm bảo hiệu ứng chỉ kích hoạt một lần

    void OnEnable()
    {
        if (!effectActive)
        {
            BossStats bossStats = boss.GetComponent<BossStats>();
            if (bossStats != null)
            {
                bossStats.currentHealth += healthIncrease;
                bossStats.damage += damageIncrease;
                effectActive = true;
            }
        }
    }
}
