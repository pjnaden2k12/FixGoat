using System.Collections;
using UnityEngine;

public class capnhatthanhmaubosshiepsi : MonoBehaviour
{
    public thanhmaubossgiapsat thanhmaubossgiapsat;
    public float luongmauhientai;
    public float luongmautoida = 1000;
    public GameObject explosionEffect; // Prefab hiệu ứng nổ
    public GameObject invulnerabilityEffect; // Prefab hiệu ứng bất tử
    public Vector3 invulnerabilityEffectOffset; // Offset cho vị trí của hiệu ứng bất tử
    public Animator animator; // Animator Controller của boss
    private bool isDead = false;
    private bool isInvulnerable = false; // Biến kiểm soát trạng thái bất khả xâm phạm
    private bool effectTriggered = false; // Biến kiểm soát hiệu ứng
    private GameObject activeInvulnerabilityEffect; // Hiệu ứng bất tử đang hoạt động

    // Start is called before the first frame update
    void Start()
    {
        luongmauhientai = luongmautoida;
        thanhmaubossgiapsat.CapNhatThanhMau(luongmauhientai, luongmautoida);
    }

    //private void OnMouseDown()
    //{
    //    TakeDamage(200); // Gọi phương thức TakeDamage khi boss bị click
    //}

    public void TakeDamage(float damage)
    {
        if (!isDead && !isInvulnerable)
        {
            luongmauhientai -= damage;
            thanhmaubossgiapsat.CapNhatThanhMau(luongmauhientai, luongmautoida);

            if (luongmauhientai <= luongmautoida * 0.6f && !effectTriggered)
            {
                StartCoroutine(MakeInvulnerable(10f)); // Boss không chịu sát thương trong 1 giây
                effectTriggered = true; // Đánh dấu rằng hiệu ứng đã được kích hoạt
            }

            if (luongmauhientai <= 0)
            {
                StartCoroutine(Die());
            }
        }
    }

    private void TriggerEffect()
    {
        if (invulnerabilityEffect != null && activeInvulnerabilityEffect == null)
        {
            activeInvulnerabilityEffect = Instantiate(invulnerabilityEffect, transform.position + invulnerabilityEffectOffset, transform.rotation);
            activeInvulnerabilityEffect.transform.SetParent(transform, false); // Gắn hiệu ứng vào boss và giữ nguyên vị trí, xoay
        }
    }

    private IEnumerator MakeInvulnerable(float duration)
    {
        isInvulnerable = true;
        TriggerEffect(); // Kích hoạt hiệu ứng bất tử
        yield return new WaitForSeconds(duration);
        isInvulnerable = false;
        if (activeInvulnerabilityEffect != null)
        {
            Destroy(activeInvulnerabilityEffect); // Xóa hiệu ứng bất tử sau khi hết thời gian
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
            Destroy(explosion, 0.9f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (activeInvulnerabilityEffect != null)
        {
            activeInvulnerabilityEffect.transform.position = transform.position + invulnerabilityEffectOffset;
        }
    }
}