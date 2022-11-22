
using System;
using UnityEngine;

public class ArrowMove : MonoBehaviour
{
    private Rigidbody bulletRigidbody;


    private void Awake()
    {
        bulletRigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        float speed = 1f;
        bulletRigidbody.velocity = transform.up * speed;
    }
}