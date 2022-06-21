using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarManager : MonoBehaviour
{
    [SerializeField] private GameObject healthBar;
    [SerializeField] private float healthAmonuntToReduce;
    [SerializeField] private float healthAmountToHeal;
    private bool onFire;


    // Update is called once per frame
    void Update()
    {
        if (onFire && healthBar.GetComponent<Image>().fillAmount != 0f)
        {
            healthBar.GetComponent<Image>().fillAmount -= healthAmonuntToReduce;
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
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Fire"))
        {
            onFire = false;
        }
    }
}
