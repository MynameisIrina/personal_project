using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;

public class BlueDragonsAI : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private ParticleSystem fire;
    [SerializeField] uint uniqueSpeed;
    private Animator dragonAnimator;
    [SerializeField] Transform dragonTransform;

    // Timer
    private float timer;
    private int waitingTime;
    
    void Start()
    {
        timer = 0;
        waitingTime = 15;
        GameObject blueDragon = GameObject.Find("BlueDragons/Blue2/FlyingDragon2");
        dragonAnimator = blueDragon.GetComponent<Animator>();

    }
    
    void Update()
    {
        /*
         * iterate through all flying dragons and make them chase a player
         * with a unique speed, that is defined in a dictionary;
         */
        timer += Time.deltaTime;
        if (timer >= waitingTime)
        {
            dragonAnimator.SetBool("fire", true);
            fire.Play();
            fire.transform.LookAt(player.transform);
            timer = 0;
        
        }
        else
        {
            dragonAnimator.SetBool("fire", false);
            fire.Stop();
        }
        
        timer += Time.deltaTime;
        //dragonTransform.LookAt(player.transform);
        Rigidbody rb = this.gameObject.GetComponent<Rigidbody>();
        Vector3 dragonPosition = this.gameObject.transform.position;
        Vector3 direction = (player.transform.position - dragonPosition).normalized;
        Quaternion rotationTowardsPlayer = Quaternion.LookRotation(direction, Vector3.up);
        dragonTransform.rotation = rotationTowardsPlayer;
        rb.MovePosition(dragonPosition + direction * Time.deltaTime * uniqueSpeed);

    }
}
