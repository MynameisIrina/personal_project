using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private GameObject player;
    [SerializeField] private HealthBarManager healthBarManager;
    [SerializeField] Vector3 currentCheckPointPosition;
    private Rigidbody rb;

    [Header("Check Points")] [SerializeField] private GameObject point1, point2, point3;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (healthBarManager.getHealthAmount() < 0.1f)
        {
            player.transform.position = currentCheckPointPosition;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("checkpoint1"))
        {
            currentCheckPointPosition = point1.transform.position;
        }
        else if (other.CompareTag("checkpoint2"))
        {
            currentCheckPointPosition = point2.transform.position;
        }
        else if (other.CompareTag("checkpoint3"))
        {
            currentCheckPointPosition = point3.transform.position;
        }
    }
}
