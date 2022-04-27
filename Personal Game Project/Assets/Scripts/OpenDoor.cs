using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    [SerializeField] private GameObject door;
    [SerializeField] private GameObject pivot_point;
    
    
    
    //[SerializeField] private GameObject player;
    // Start is called before the first frame update
    private void Start()
    {
        door.transform.RotateAround(pivot_point.transform.position, Vector3.up, 90f);
    }

    void Awake()
    {
        gameObject.GetComponent<OpenDoor>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
