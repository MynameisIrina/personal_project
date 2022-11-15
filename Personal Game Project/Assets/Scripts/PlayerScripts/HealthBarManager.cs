using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarManager : MonoBehaviour
{
    [SerializeField] private GameObject healthBar;
    [SerializeField] private float healthAmonuntToReduce_Fire;
    [SerializeField] private float healthAmountToReduce_Enemy;
    [SerializeField] private float healthAmountToHeal;
    private bool onFire;

    [SerializeField] private Animator enemy1;
    private bool getDamageEnemy;


    // Update is called once per frame
    void Update()
    {
        if (onFire && healthBar.GetComponent<Image>().fillAmount != 0f)
        {
            healthBar.GetComponent<Image>().fillAmount -= healthAmonuntToReduce_Fire;
        }
        else
        {
            // TODO: add aid kit
            healthBar.GetComponent<Image>().fillAmount += healthAmountToHeal;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fire"))
        {
            onFire = true;
        }
        
        if (other.gameObject.CompareTag("EnemyClaws"))
        {
            if (enemy1.GetBool("isAttacking"))
            {
                healthBar.GetComponent<Image>().fillAmount -= healthAmountToReduce_Enemy;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Fire"))
        {
            onFire = false;
        }
    }
}
