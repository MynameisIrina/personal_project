using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCRun : MonoBehaviour
{
    [SerializeField] private AINavigationFemale _aiNavigationFemale;
    // Start is called before the first frame update
    void Start()
    {
        _aiNavigationFemale.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            _aiNavigationFemale.enabled = true;
        }
    }
}
