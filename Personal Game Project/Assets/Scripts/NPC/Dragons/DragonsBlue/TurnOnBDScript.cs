using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOnBDScript : MonoBehaviour
{
    [SerializeField] private GameObject blueDragons;

    private Boolean turnedOn;
    // Start is called before the first frame update
    void Start()
    {
        blueDragons.SetActive(false);
        turnedOn = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (turnedOn)
        {
            blueDragons.SetActive(true);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        turnedOn = true;
    }
}
