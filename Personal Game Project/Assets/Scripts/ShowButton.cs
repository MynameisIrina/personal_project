using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowButton : MonoBehaviour
{
    [SerializeField] private GameObject button_triangle;

    private bool show_button;
    // Start is called before the first frame update
    void Start()
    {
        button_triangle.GetComponent<Canvas>().enabled = false;
        show_button = false;
    }

    private void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (show_button)
        {
            button_triangle.GetComponent<Canvas>().enabled = true;

        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            show_button = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        show_button = false; 
        button_triangle.GetComponent<Canvas>().enabled = false;
        //gameObject.GetComponent<OpenNote>().enabled = false;

    }
}
