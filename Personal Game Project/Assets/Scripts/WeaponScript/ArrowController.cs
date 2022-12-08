using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    private bool fireInput;
    private bool aimInput;
    private float range = 999f;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private Camera fpsCam;
    [SerializeField] private GameObject arrowInHand;
    
    // Start is called before the first frame update
    private void Update()
    {
    }

    public void Shoot()
    {
        RaycastHit hit;
        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Vector3 direction = hit.point - this.transform.position;
            this.transform.rotation = Quaternion.LookRotation(direction);
            GameObject arrow = Instantiate(arrowPrefab, spawnPoint.position, spawnPoint.rotation);
            arrow.GetComponent<Rigidbody>().velocity = -spawnPoint.forward * 20;
        }
    }
    
    public void ReceiveFireInput(bool fire_input)
    {
        fireInput = fire_input;
    }

    public void ReceiveAimInput(bool aim_input)
    {
        aimInput = aim_input;
    }
}
