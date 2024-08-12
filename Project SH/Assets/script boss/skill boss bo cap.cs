using System.Collections;
using UnityEngine;

public class bossbocap : MonoBehaviour
{
    public thanhmaubossgiapsat thanhmaubossgiapsat;
    public float luongmauhientai;
    public float luongmautoida = 1000;
    public GameObject explosionEffect; // Prefab hiệu ứng nổ
  
    public Animator animator; // Animator Controller của boss
    private bool isDead = false;
    private bool isInvulnerable = false; // Biến kiểm soát trạng thái bất khả xâm phạm
    //private bool effectTriggered = false; // Biến kiểm soát hiệu ứng
    private GameObject activeInvulnerabilityEffect; // Hiệu ứng bất tử đang hoạt động

    // Start is called before the first frame update
    void Start()
    {
        luongmauhientai = luongmautoida;
        thanhmaubossgiapsat.CapNhatThanhMau(luongmauhientai, luongmautoida);
    }

    private void OnMouseDown()
    {
        if (!isDead && !isInvulnerable)
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
            Destroy(explosion, 1f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
