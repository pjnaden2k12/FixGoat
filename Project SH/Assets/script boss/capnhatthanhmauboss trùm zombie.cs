using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class capnhatthanhmaubosstrumzombie : MonoBehaviour
{
    public thanhmaubossgiapsat thanhmaubossgiapsat;
    public float luongmauhientai;
    public float luongmautoida = 1000;

    // Start is called before the first frame update
    void Start()
    {
        // Đặt lượng máu hiện tại bằng lượng máu tối đa khi bắt đầu
        luongmauhientai = luongmautoida;
        thanhmaubossgiapsat.CapNhatThanhMau(luongmauhientai, luongmautoida);
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
    }
}
