using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TakeDamage : MonoBehaviour
{
    [SerializeField] private Canvas healthBar;

    private uint healthAmonuntToReduce;
    // Start is called before the first frame update
    void Start()
    {
        healthAmonuntToReduce = 2;
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Tag: " + collision.gameObject.tag);
        if (collision.gameObject.tag == "Weapon")
        {
            healthBar.GetComponent<Image>().fillAmount -= healthAmonuntToReduce;
        }
    }
}
