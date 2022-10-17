using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class ArrowMove : MonoBehaviour
{
    [SerializeField] private uint speed;
    private Vector3 target;

    private Vector3 nullVector = new Vector3(0, 0, 0);
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }

    public void SetTarget(Vector3 tar)
    {
        target = tar;
    }

    public Vector3 GetTarget()
    {
        return target;
    }
    
}