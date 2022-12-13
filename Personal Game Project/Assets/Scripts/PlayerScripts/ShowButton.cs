using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowButton : MonoBehaviour
{
    [SerializeField] private GameObject button_triangle;
    [SerializeField] private GameObject note;

    private bool show_button;
    // Start is called before the first frame update
    void Start()
    {
        //note.gameObject.SetActive(false);
        button_triangle.GetComponent<Canvas>().enabled = false;
        //button_triangle.SetActive(false);
        show_button = false;
    }
    

    // Update is called once per frame
    void Update()
    {
        if (show_button)
        {
            //note.gameObject.SetActive(true);
            button_triangle.GetComponent<Canvas>().enabled = true;
            //button_triangle.SetActive(true);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            show_button = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        show_button = false; 
        button_triangle.GetComponent<Canvas>().enabled = false;
        //button_triangle.SetActive(false);
        //note.gameObject.SetActive(false);

    }
}
