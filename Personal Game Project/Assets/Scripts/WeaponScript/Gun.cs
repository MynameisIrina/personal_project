using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    private bool fireInput;
    public float range = 999f;
    public Transform spawnPoint;
    public GameObject bulletPrefab;

    public Camera fpsCam;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (fireInput)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        RaycastHit hit;
        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Vector3 direction = hit.point - this.transform.position;
            this.transform.rotation = Quaternion.LookRotation(direction);
            GameObject bullet = Instantiate(bulletPrefab, spawnPoint.position, spawnPoint.rotation);
            bullet.GetComponent<Rigidbody>().velocity = spawnPoint.forward * 10;
        }
    }

    public void receiveFireInput(bool fire_input)
    {
        fireInput = fire_input;
    }
}
