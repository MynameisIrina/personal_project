using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwordController : MonoBehaviour
{
    [SerializeField] private Canvas enemyHealthBar;
    [SerializeField] private Animator enemyAnimator;
    private int count;

    private bool hit;
    // Start is called before the first frame update
    void Start()
    {
        count = 0;
        hit = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (hit)
        {
            if (enemyHealthBar.GetComponent<Image>().fillAmount == 0)
            {
                enemyAnimator.SetBool("isDying", true);
            }
            enemyAnimator.SetBool("isGettingHit", true);
        }
        else
        {
            enemyAnimator.SetBool("isGettingHit", false);
        }

        hit = false;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            count++;
            hit = true;
            Debug.Log("HIT " + count);
            enemyHealthBar.GetComponent<Image>().fillAmount -= 0.15f;
        }
    }
}
