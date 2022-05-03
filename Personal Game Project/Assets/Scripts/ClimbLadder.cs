using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbLadder : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("CLIMB TRUE");
        gameObject.GetComponent<Animator>().SetBool("climb", true);
        gameObject.GetComponent<Rigidbody>().MovePosition((gameObject.GetComponent<Rigidbody>().position + new Vector3(0,0.1f,0)));
    }
}
