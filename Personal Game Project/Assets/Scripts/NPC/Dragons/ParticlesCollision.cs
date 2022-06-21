using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesCollision : MonoBehaviour
{
    private void OnParticleCollision(GameObject other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("HIT");

        }
        this.gameObject.GetComponent<ParticleSystem>().Stop();
    }
}
