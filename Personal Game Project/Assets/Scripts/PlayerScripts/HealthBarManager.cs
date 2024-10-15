using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarManager : MonoBehaviour
{
    [SerializeField] private GameObject healthBar;
    [SerializeField] private float healthAmonuntToReduce_Fire;
    [SerializeField] private float healthAmountToReduce_Enemy;
    [SerializeField] private float healthAmountToHeal;
    [SerializeField] private CheckPointObserver checkPoint;
    [SerializeField] private Animator enemy1;
    private bool getDamageEnemy;
    private bool onFire;

    
    void Update()
    {
        if (healthBar.GetComponent<Image>().fillAmount < 1f)
        {
            healthBar.GetComponent<Image>().fillAmount += healthAmountToHeal;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Fire"))
        {
            TakeDamage(healthAmonuntToReduce_Fire);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.CompareTag("EnemyClaws"))
        {
            if (enemy1.GetBool("isAttacking"))
            {
                TakeDamage(healthAmountToReduce_Enemy);
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

    public void TakeDamage(float damage)
    {
        healthBar.GetComponent<Image>().fillAmount -= damage;

        if (healthBar.GetComponent<Image>().fillAmount <= 0f)
        {
            checkPoint?.GoToLastCheckpoint();
        }
    }

    public void ReloadHealthBar()
    {
        healthBar.GetComponent<Image>().fillAmount = 1;
        onFire = false;
    }
    

    public float GetHealthAmount()
    {
        return healthBar.GetComponent<Image>().fillAmount;
    }
}
