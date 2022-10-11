using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenNote : MonoBehaviour
{

    [SerializeField] private GameObject note;
    [SerializeField] private PlayerController playerController;
    
    
    // Start is called before the first frame update
    void Start()
    {
        note.GetComponent<Canvas>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.pick_up)
        {
            note.GetComponent<Canvas>().enabled = true;
        }

        if (playerController.put_away)
        {
           note.GetComponent<Canvas>().enabled = false;
        }
    }

    
}
