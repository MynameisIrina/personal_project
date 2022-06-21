using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AINavigationFemale : MonoBehaviour
{
    public GameObject destination;
    private NavMeshAgent NPCfemale;
    private Animator npcAnimator;

    // Start is called before the first frame update
    void Start()
    {
        NPCfemale = GetComponent<NavMeshAgent>();
        npcAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        npcAnimator.SetBool("run", true);
        NPCfemale.SetDestination(destination.transform.position);
        // if (destination.transform.position == new Vector3(186 ,3, 375)) // last destination
        // {
        //     npcAnimator.SetBool("run", false);
        // }
    }

    
}
