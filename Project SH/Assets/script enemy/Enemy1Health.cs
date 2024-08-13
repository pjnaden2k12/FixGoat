using UnityEngine;
using System.Collections;

public class Enemy1Health : MonoBehaviour
{
    public float maxHealth = 100; // Máu tối đa của quái vật
    public float armor = 10; // Giáp của quái vật
    private float currentHealth; // Máu hiện tại của quái vật
    private Animator animator; // Animator của quái vật
    private bool isDead = false; // Trạng thái chết của quái vật
    private Rigidbody rb;

    private GearManager gearManager; // Tham chiếu đến GearManager

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();

        rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.useGravity = false;
            rb.isKinematic = true;
        }

        // Tìm GearManager trong cảnh
        gearManager = FindObjectOfType<GearManager>();
    }

    void Update()
    {
        // Update logic if needed
    }

    public void TakeDamage(float damageAmount)
    {
        if (isDead)
            return;

        float damageAfterArmor = Mathf.Max(damageAmount - armor, 0);
        currentHealth -= damageAfterArmor;
        Debug.Log("Damage taken: " + damageAmount + " (after armor: " + damageAfterArmor + ") Current Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (isDead)
            return;

        isDead = true;
        Debug.Log("Enemy died!");

        if (animator != null)
        {
            animator.SetTrigger("Die");
        }

        // Tính toán số lượng bánh răng ngẫu nhiên từ 10 đến 50
        int gearsToDrop = Random.Range(10, 51);
        Debug.Log("Gears dropped: " + gearsToDrop);

        // Thêm bánh răng vào GearManager
        if (gearManager != null)
        {
            gearManager.AddGears(gearsToDrop);
        }
        else
        {
            Debug.LogWarning("GearManager không tìm thấy!");
        }

        StartCoroutine(FadeOutAndDestroy());
    }

    IEnumerator FadeOutAndDestroy()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            Color color = renderer.material.color;
            for (float t = 0; t < 1; t += Time.deltaTime / 2f)
            {
                color.a = Mathf.Lerp(1, 0, t);
                renderer.material.color = color;
                yield return null;
            }
            color.a = 0;
            renderer.material.color = color;
        }

        Destroy(gameObject);
    }

    //void OnMouseDown()
    //{
    //    TakeDamage(50);
    //}
}
