using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class capnhatthanhmaubosshiepsi : MonoBehaviour
{
    public thanhmaubossgiapsat thanhmaubossgiapsat;
    public float luongmauhientai;
    public float luongmautoida = 1000;
    public float recoveryAmount = 100f; // 100 máu hồi lại
    public float recoveryInterval = 5f; // 5 giây
    public GameObject explosionEffect; // Prefab hiệu ứng nổ
    public Animator animator; // Animator Controller của boss
    private bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        luongmauhientai = luongmautoida;
        thanhmaubossgiapsat.CapNhatThanhMau(luongmauhientai, luongmautoida);
        StartCoroutine(RecoverHealth());
    }

    private IEnumerator RecoverHealth()
    {
        while (!isDead)
        {
            yield return new WaitForSeconds(recoveryInterval);

            // Tính toán lượng máu hồi lại
            luongmauhientai = Mathf.Min(luongmauhientai + recoveryAmount, luongmautoida);

            // Cập nhật thanh máu
            thanhmaubossgiapsat.CapNhatThanhMau(luongmauhientai, luongmautoida);
            Debug.Log("Boss health recovered. Current Health: " + luongmauhientai);
        }
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
        isDead = true;
        luongmauhientai = 0;
        thanhmaubossgiapsat.CapNhatThanhMau(luongmauhientai, luongmautoida);
        Debug.Log("Boss is dead.");

        // Hiển thị hiệu ứng nổ
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }

        // Kích hoạt animation chết
        if (animator != null)
        {
            animator.SetTrigger("Dead");
        }

        // Đợi 2 giây để animation hoàn tất
        yield return new WaitForSeconds(0f);

        // Xóa đối tượng boss khỏi cảnh
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
