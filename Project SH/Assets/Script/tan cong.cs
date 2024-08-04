using UnityEngine;

public class BAttack : MonoBehaviour
{
    public Animator animator; // Animator của Boss

    void Start()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Target"))
        {
            TriggerAttackAnimation();
        }
    }

    void TriggerAttackAnimation()
    {
        if (animator != null)
        {
            animator.SetTrigger("attack"); // Kích hoạt animation tấn công
            Debug.Log("Boss đã tấn công!");
        }
        else
        {
            Debug.LogError("Animator chưa được gán!");
        }
    }
}
