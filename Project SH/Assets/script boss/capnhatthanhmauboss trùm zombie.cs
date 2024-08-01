using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class capnhatthanhmaubosstrumzombie : MonoBehaviour
{
    public thanhmaubossgiapsat thanhmaubossgiapsat;
    public float luongmauhientai;
    public float luongmautoida = 1000;
    public float recoveryAmount = 0.1f; // 10% máu tối đa
    public float recoveryInterval = 10.0f; // Thời gian hồi máu (10 giây)
    public GameObject hieuUngNo; // Tham chiếu đến prefab hiệu ứng nổ
    public float thoiGianChoTruocKhiNo = 1.0f; // Thời gian tồn tại của hiệu ứng nổ
    private bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        // Đặt lượng máu hiện tại bằng lượng máu tối đa khi bắt đầu
        luongmauhientai = luongmautoida;
        thanhmaubossgiapsat.CapNhatThanhMau(luongmauhientai, luongmautoida);
        StartCoroutine(HoiMau());
    }

    // Update is called once per frame
    void Update()
    {
        // Kiểm tra sự kiện nhấp chuột
        if (Input.GetMouseButtonDown(0)) // Kiểm tra nếu nhấp chuột trái
        {
            // Kiểm tra xem có nhấp vào boss trùm zombie không
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                // Giảm máu khi nhấp vào boss
                GiamMau(200);
            }
        }
    }

    // Hàm để giảm máu
    public void GiamMau(float luong)
    {
        luongmauhientai -= luong;
        if (luongmauhientai < 0)
        {
            luongmauhientai = 0;
        }
        thanhmaubossgiapsat.CapNhatThanhMau(luongmauhientai, luongmautoida);

        if (luongmauhientai == 0 && !isDead)
        {
            BatDauNo();
        }
    }

    // Hàm để bắt đầu quá trình nổ
    void BatDauNo()
    {
        isDead = true;
        // Hiển thị hiệu ứng nổ
        GameObject explosion = Instantiate(hieuUngNo, transform.position, transform.rotation);
        // Tiêu diệt boss ngay lập tức
        Destroy(gameObject);
        // Tiêu diệt hiệu ứng nổ sau một khoảng thời gian ngắn
        Destroy(explosion, thoiGianChoTruocKhiNo);
    }

    // Coroutine để hồi máu
    private IEnumerator HoiMau()
    {
        while (!isDead)
        {
            luongmauhientai += luongmautoida * recoveryAmount;
            if (luongmauhientai > luongmautoida)
            {
                luongmauhientai = luongmautoida;
            }
            thanhmaubossgiapsat.CapNhatThanhMau(luongmauhientai, luongmautoida);
            yield return new WaitForSeconds(recoveryInterval);
        }
    }
}
