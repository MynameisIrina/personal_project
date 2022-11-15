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
    private bool isAttacking;

    private void Update()
    {
        if (playerController.getSwordAttack())
        {
            isAttacking = true;
        }
        
        if (enemyHealthBar.GetComponent<Image>().fillAmount <= 0f)
        {
            enemyAnimator.GetComponent<Animator>().SetBool("isDying", true);

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") && isAttacking)
        {
            other.GetComponent<Animator>().SetBool("isGettingHit", true);
            enemyHealthBar.GetComponent<Image>().fillAmount -= 0.05f;
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
