using System.Collections;
using UnityEngine;

public class bossthayma : MonoBehaviour
{
    public thanhmaubossgiapsat thanhmaubossgiapsat;
    public float luongmauhientai;
    public float luongmautoida = 1000;
    public GameObject explosionEffect; // Prefab hiệu ứng nổ
    public Animator animator; // Animator Controller của boss
    private bool isDead = false;
    private bool hasScaledUp = false; // Biến kiểm soát việc phóng to

    // Start is called before the first frame update
    void Start()
    {
        luongmauhientai = luongmautoida;
        thanhmaubossgiapsat.CapNhatThanhMau(luongmauhientai, luongmautoida);
        StartCoroutine(MoveAndScale());
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

    private IEnumerator MoveAndScale()
    {
        // Di chuyển boss (giả sử boss di chuyển theo phương ngang)
        float moveDuration = 9f;
        float elapsedTime = 0f;

        while (elapsedTime < moveDuration)
        {
            // Thay đổi vị trí boss (di chuyển về phía trước hoặc hướng cụ thể)
            transform.Translate(Vector3.forward * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Sau khi di chuyển 5 giây
        if (!hasScaledUp)
        {
            // Phóng to boss 2 lần
            transform.localScale *= 2f;
            hasScaledUp = true;

            // Tăng gấp đôi máu hiện tại
            luongmautoida *= 2;
            luongmauhientai = luongmautoida;
            thanhmaubossgiapsat.CapNhatThanhMau(luongmauhientai, luongmautoida);
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
}
