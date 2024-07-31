using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class capnhatthanhmaubosshiepsi : MonoBehaviour
{
    public thanhmaubossgiapsat thanhmaubossgiapsat;
    public float luongmauhientai;
    public float luongmautoida = 1000;
    public float recoveryAmount = 200f; // 200 máu hồi lại
    public float recoveryInterval = 1f; // 1 giây
    public GameObject explosionEffect; // Prefab hiệu ứng nổ
    public Animator animator; // Animator Controller của boss
    private bool isDead = false;
    private bool isInvulnerable = false; // Biến kiểm soát trạng thái bất khả xâm phạm
    private bool triggeredInvulnerability = false; // Đã kích hoạt bất khả xâm phạm

    // Start is called before the first frame update
    void Start()
    {
        luongmauhientai = luongmautoida;
        thanhmaubossgiapsat.CapNhatThanhMau(luongmauhientai, luongmautoida);
        StartCoroutine(HoiMau()); // Bắt đầu hồi máu
    }

    private void OnMouseDown()
    {
        if (!isDead && !isInvulnerable)
        {
            luongmauhientai -= 200;
            thanhmaubossgiapsat.CapNhatThanhMau(luongmauhientai, luongmautoida);

            if (luongmauhientai <= luongmautoida * 0.6f && !triggeredInvulnerability)
            {
                StartCoroutine(MakeInvulnerable(10f)); // Boss không chịu sát thương trong 10 giây
                triggeredInvulnerability = true; // Đánh dấu rằng bất khả xâm phạm đã được kích hoạt
            }

            if (luongmauhientai <= 0)
            {
                StartCoroutine(Die());
            }
        }
    }

    private IEnumerator HoiMau()
    {
        while (!isDead)
        {
            luongmauhientai += recoveryAmount;
            if (luongmauhientai > luongmautoida)
            {
                luongmauhientai = luongmautoida;
            }
            thanhmaubossgiapsat.CapNhatThanhMau(luongmauhientai, luongmautoida);
            yield return new WaitForSeconds(recoveryInterval);
        }
    }

    private IEnumerator MakeInvulnerable(float duration)
    {
        isInvulnerable = true;
        yield return new WaitForSeconds(duration);
        isInvulnerable = false;
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
            Destroy(explosion, 1f);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
