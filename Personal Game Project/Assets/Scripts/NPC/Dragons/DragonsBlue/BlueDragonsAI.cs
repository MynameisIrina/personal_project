using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Security.Cryptography;
using UnityEngine;

public class BlueDragonsAI : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private ParticleSystem p;
    [SerializeField] private ParticleSystem fire1;
    [SerializeField] private ParticleSystem fire2;
    private Animator dragonAnimator1;
    private Animator dragonAnimator2;
    // private ParticleSystem fire1;
    // private ParticleSystem fire2;
    private Dictionary<Transform, uint> flyingDragons; // Key = Dragon; Value = speed
    private uint uniqueSpeed;
    
    // Timer
    private float timer;
    private int waitingTime;
    
    void Start()
    {
        timer = 0;
        waitingTime = 15;

        flyingDragons = new Dictionary<Transform, uint>();
        // 2 flying dragons; chasing player
        GameObject blueDr = GameObject.Find("BlueDragons");
        Transform dragon1 = blueDr.transform.Find("FlyingDragon1");
        dragonAnimator1 = dragon1.GetComponent<Animator>();
        //fire1 = dragon1.GetComponent<ParticleSystem>();
        //fire1 = dragon1.Find("Root_Pelvis/Spine/Chest/Neck/Head/UpperMouth/Fire1").GetComponent<ParticleSystem>();
        Transform dragon2 = blueDr.transform.Find("FlyingDragon2");
        dragonAnimator2 = dragon2.GetComponent<Animator>();
        //fire2 = dragon2.GetComponent<ParticleSystem>();
        //fire2 = dragon2.Find("Root_Pelvis/Spine/Chest/Neck/Head/UpperMouth/Fire2").GetComponent<ParticleSystem>();
        flyingDragons.Add(dragon1, 5);
        flyingDragons.Add(dragon2, 3);
        
        // 2 walking dragons; chasing player

    }

    // Update is called once per frame
    void Update()
    {
        /*
         * iterate through all flying dragons and make them chase a player
         * with a unique speed, that is defined in a dictionary;
         */
        timer += Time.deltaTime;
        if (timer >= waitingTime)
        {
            dragonAnimator1.SetBool("fire", true);
            fire1.Play();
            fire1.transform.LookAt(player.transform);
            dragonAnimator2.SetBool("fire", true);
            fire2.Play();
            fire2.transform.LookAt(player.transform);
            timer = 0;
        
        }
        else
        {
            dragonAnimator1.SetBool("fire", false);
            dragonAnimator2.SetBool("fire", false);

        }
        foreach (var dragon in flyingDragons)
        {
            timer += Time.deltaTime;
            uniqueSpeed = dragon.Value;
            dragon.Key.LookAt(player.transform);
            dragon.Key.Translate(Vector3.forward * Time.deltaTime * uniqueSpeed);

        }
    }
    
}
