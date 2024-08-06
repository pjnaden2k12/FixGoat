using System.Collections;
using UnityEngine;

public class bossoshaman : MonoBehaviour
{
    public thanhmaubossgiapsat thanhmaubossgiapsat;
    public float luongmauhientai;
    public float luongmautoida = 1000;
    public GameObject explosionEffect; // Prefab hiệu ứng nổ
    public GameObject surroundingEffectPrefab; // Prefab hiệu ứng xung quanh boss

    public Animator animator; // Animator Controller của boss
    private bool isDead = false;
    private bool hasSurroundingEffect = false; // Biến kiểm soát hiệu ứng xung quanh

    // Start is called before the first frame update
    void Start()
    {
        luongmauhientai = luongmautoida;
        thanhmaubossgiapsat.CapNhatThanhMau(luongmauhientai, luongmautoida);
    }

    private void OnMouseDown()
    {
        if (!isDead)
        {
            luongmauhientai -= 200;
            thanhmaubossgiapsat.CapNhatThanhMau(luongmauhientai, luongmautoida);

            // Kích hoạt hiệu ứng xung quanh khi máu giảm xuống dưới 70%
            if (luongmauhientai <= luongmautoida * 0.7f && !hasSurroundingEffect)
            {
                ActivateSurroundingEffect();
            }

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

        // Xóa hiệu ứng nổ sau 1 giây
        if (explosion != null)
        {
            Destroy(explosion, 1f);
        }
    }

    private void ActivateSurroundingEffect()
    {
        if (!hasSurroundingEffect && surroundingEffectPrefab != null)
        {
            // Tạo và gán hiệu ứng xung quanh boss
            GameObject surroundingEffect = Instantiate(surroundingEffectPrefab, transform.position, Quaternion.identity);
            surroundingEffect.transform.SetParent(transform); // Gán parent để theo sát boss
            surroundingEffect.transform.localPosition = Vector3.zero; // Đảm bảo hiệu ứng nằm tại vị trí đúng
            hasSurroundingEffect = true;
        }
    }
}
