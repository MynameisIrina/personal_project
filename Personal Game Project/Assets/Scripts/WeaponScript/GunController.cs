using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class GunController : MonoBehaviour
{
    private bool fireInput;
    private float range = 999f;
    [SerializeField] private Transform spawnPoint;
    //[SerializeField] private GameObject bulletPrefab;
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private Camera fpsCam;


    public void Shoot()
    {
        RaycastHit hit;
        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Vector3 direction = hit.point - this.transform.position;
            this.transform.rotation = Quaternion.LookRotation(direction);
            muzzleFlash.Play();
            //GameObject bullet = Instantiate(bulletPrefab, spawnPoint.position, spawnPoint.rotation);
            //bullet.GetComponent<Rigidbody>().velocity = spawnPoint.forward * 10;
        }
    }
    
}
