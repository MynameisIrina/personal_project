using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveArrow : MonoBehaviour
{
    private Rigidbody arrowRB;
    void Start()
    {
        arrowRB = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame

    private void OnCollisionEnter(Collision collision)
    {
        arrowRB.velocity = Vector3.zero;
        arrowRB.angularVelocity = Vector3.zero;
    }
}
