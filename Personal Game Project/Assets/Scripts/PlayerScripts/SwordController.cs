using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwordController : MonoBehaviour
{
    [SerializeField] private Animator enemyAnimator;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Canvas enemyHealthBar;
    [SerializeField] private AudioSource swordSound;
    private bool isAttacking;

    private void Update()
    {
        if (playerController.GetSwordAttack())
        {
            isAttacking = true;
            swordSound.Play();
        }
        
        // check if enemy is dying
        if (enemyHealthBar.GetComponent<Image>().fillAmount <= 0f)
        {
            enemyAnimator.GetComponent<Animator>().SetBool("isDying", true);

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // check if enemy was attacked by a sword
        if (other.gameObject.CompareTag("Enemy") && isAttacking)
        {
            other.GetComponent<Animator>().SetBool("isGettingHit", true);
            enemyHealthBar.GetComponent<Image>().fillAmount -= 0.05f;
            
            // wait for the animation to end before attacking one more time
            StartCoroutine(ResetBoolAttacking(other));
        }
    }

    IEnumerator ResetBoolAttacking(Collider other)
    {
        yield return new WaitForSeconds(1f);
        isAttacking = false;
        other.GetComponent<Animator>().SetBool("isGettingHit", false);
    }
    

}
