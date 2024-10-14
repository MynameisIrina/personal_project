using System;
using UnityEngine;
using UnityEngine.UI;

public class DragonController : MonoBehaviour
{
    [SerializeField] private Image healthBar;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Bullet") || other.gameObject.CompareTag("Arrow") || other.gameObject.CompareTag("Sword"))
        {
            animator.SetBool("gotHit", true);
            if (other.gameObject.CompareTag("Bullet"))
            {
                String currentState = animator.GetCurrentAnimatorStateInfo(0).ToString();
                animator.SetBool(currentState, false);
                animator.SetBool("isChasing", true);
                TakeDamage(0.01f);
            }
            else if (other.gameObject.CompareTag("Arrow"))
            {
                TakeDamage(0.005f);
            }
            else if (other.gameObject.CompareTag("Sword"))
            {
                TakeDamage(0.1f);
            }
        }
        
        
    }

    private void TakeDamage(float damage)
    {
        healthBar.fillAmount -= damage;
        if (healthBar.fillAmount <= 0)
        {
            animator.SetBool("isDead", true);
        }
    }
}
