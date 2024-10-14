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

    public void TakeDamage(float damage)
    {
        healthBar.GetComponent<Image>().fillAmount -= damage;
    }

    public void ReloadHealthBar()
    {
        healthBar.GetComponent<Image>().fillAmount = 1;
        onFire = false;
    }
    

    public float getHealthAmount()
    {
        return healthBar.GetComponent<Image>().fillAmount;
    }
}
