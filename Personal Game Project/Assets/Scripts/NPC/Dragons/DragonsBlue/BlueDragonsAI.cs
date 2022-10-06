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
    [SerializeField] private ParticleSystem fire3;
    private Animator dragonAnimator1;
    private Animator dragonAnimator2;
    private Animator dragonAnimator3;
    private Dictionary<Transform, uint> flyingDragons; // Key = Dragon; Value = speed
    private uint uniqueSpeed;

    [SerializeField] GameObject ray1;
    [SerializeField] GameObject ray2;
    [SerializeField] GameObject ray3;

    
    
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
        Transform dragon2 = blueDr.transform.Find("FlyingDragon2");
        dragonAnimator2 = dragon2.GetComponent<Animator>();
        Transform dragon3 = blueDr.transform.Find("FlyingDragon3");
        dragonAnimator3 = dragon3.GetComponent<Animator>();
        flyingDragons.Add(dragon1, 4);
        flyingDragons.Add(dragon2, 3);
        flyingDragons.Add(dragon3, 6);

        // 3 walking dragons; chasing player

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
            dragonAnimator1.SetBool("fire", true);
            fire1.Play();
            fire1.transform.LookAt(player.transform);
            dragonAnimator2.SetBool("fire", true);
            fire2.Play();
            fire2.transform.LookAt(player.transform);
            dragonAnimator3.SetBool("fire", true);
            fire3.Play();
            fire3.transform.LookAt(player.transform);
            timer = 0;
        
        }
        else
        {
            dragonAnimator1.SetBool("fire", false);
            dragonAnimator2.SetBool("fire", false);
            dragonAnimator3.SetBool("fire", false);


        }
        foreach (var dragon in flyingDragons)
        {
            timer += Time.deltaTime;
            uniqueSpeed = dragon.Value;
            dragon.Key.LookAt(player.transform);
            dragon.Key.Translate(Vector3.forward * Time.deltaTime * uniqueSpeed);

        }
        
        bool checkRay1 = Physics.Raycast(ray1.transform.position, Vector3.right, 5f);
        Debug.DrawRay(ray1.transform.position, Vector3.forward);
        if (checkRay1)
        {
            dragonAnimator1.SetBool("Fly Float", false);
        }
        // else
        // {
        //     dragonAnimator1.SetBool("Fly Float", true);
        //
        // }
        bool checkRay2 = Physics.Raycast(ray2.transform.position, Vector3.right, 5f);
        if (checkRay2)
        {
            dragonAnimator2.SetBool("Fly Float", false);
        }
        // else
        // {
        //     dragonAnimator2.SetBool("Fly Float", true);
        //
        // }

    }
    
}
