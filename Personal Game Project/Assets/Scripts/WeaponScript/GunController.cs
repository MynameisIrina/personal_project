using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.UI;

public class GunController : MonoBehaviour
{
    private bool fireInput;
    private float range = 999f;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private Camera fpsCam;
    [SerializeField] private AudioSource gunShotSound;
    [SerializeField] private GameObject healthBarEnemy;
    [SerializeField] private ParticleSystem bloodEffect;
    [SerializeField] private Animator enemyAnimator;


    private void Update()
    {
        if (healthBarEnemy.GetComponent<Image>().fillAmount <= 0f)
        {
            enemyAnimator.GetComponent<Animator>().SetBool("isDying", true);
        }
    }

    public void Shoot()
    {
        RaycastHit hit;
        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Vector3 direction = hit.point - this.transform.position;
            this.transform.rotation = Quaternion.LookRotation(direction);
            muzzleFlash.Play();
            GameObject bullet = Instantiate(bulletPrefab, spawnPoint.position, spawnPoint.rotation);
            gunShotSound.time = 0.6f;
            gunShotSound.Play();
            bullet.GetComponent<Rigidbody>().velocity = spawnPoint.forward * 100;
            if (hit.collider.CompareTag("Enemy"))
            {
                Instantiate(bloodEffect, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
                healthBarEnemy.GetComponent<Image>().fillAmount -= 0.3f;
                hit.collider.gameObject.GetComponent<Animator>().SetBool("isGettingHit", true);
                StartCoroutine(ResetBoolAttacking(hit.collider));
            }
        }
    }
    
    IEnumerator ResetBoolAttacking(Collider other)
    {
        yield return new WaitForSeconds(1f);
        other.GetComponent<Animator>().SetBool("isGettingHit", false);
    }
    
}
