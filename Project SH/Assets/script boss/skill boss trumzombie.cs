using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class capnhatthanhmaubosstrumzombie : MonoBehaviour
{
    public thanhmaubossgiapsat thanhmaubossgiapsat;
    public float luongmauhientai;
    public float luongmautoida = 1000;
    public GameObject explosionEffect; // Prefab hiệu ứng nổ
    public GameObject minionPrefab; // Prefab của quái con
    public Transform minionsParent; // Đối tượng cha sẽ chứa các quái con
    public Transform targetPoint; // Điểm mục tiêu của quái con
    public Animator animator; // Animator Controller của boss
    private bool isDead = false;
    //private bool effectTriggered = false; // Biến kiểm soát hiệu ứng

    // Start is called before the first frame update
    void Start()
    {
        luongmauhientai = luongmautoida;
        thanhmaubossgiapsat.CapNhatThanhMau(luongmauhientai, luongmautoida);
        StartCoroutine(SpawnMinionsAfterDelay(5f)); // Gọi Coroutine để sinh ra quái con sau 5 giây
    }

    private void OnMouseDown()
    {
        if (!isDead)
        {
            luongmauhientai -= 200;
            thanhmaubossgiapsat.CapNhatThanhMau(luongmauhientai, luongmautoida);

            if (luongmauhientai <= 0)
            {
                StartCoroutine(Die());
            }
        }
    }

    private IEnumerator Die()
    {
        if (isDead) yield break; // Kiểm tra nếu boss đã chết, thoát khỏi coroutine

        isDead = true;
        luongmauhientai = 0;
        thanhmaubossgiapsat.CapNhatThanhMau(luongmauhientai, luongmautoida);
        Debug.Log("Boss is dead.");

        // Hiển thị hiệu ứng nổ chỉ một lần
        GameObject explosion = null;
        if (explosionEffect != null)
        {
            explosion = Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }

        // Đợi animation hoàn tất (ở đây là 0 giây)
        yield return new WaitForSeconds(0f);

        // Xóa đối tượng boss khỏi cảnh
        Destroy(gameObject);

        // Xóa hiệu ứng nổ sau 2 giây
        if (explosion != null)
        {
            Destroy(explosion, 0.6f);
        }
    }

    private IEnumerator SpawnMinionsAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Sinh ra quái con và đặt làm con của minionsParent
        if (minionPrefab != null && minionsParent != null)
        {
            GameObject minion = Instantiate(minionPrefab, transform.position, Quaternion.identity);
            minion.transform.SetParent(minionsParent);

            // Gán targetPoint cho quái con
            BossMovement bossMovement = minion.GetComponent<BossMovement>();
            if (bossMovement != null)
            {
                bossMovement.MoveToTarget(targetPoint);
            }
        }
    }
}
