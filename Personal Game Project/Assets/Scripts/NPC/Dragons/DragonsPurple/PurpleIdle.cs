using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurpleIdle : MonoBehaviour
{
    private float timer;

    [SerializeField] private Transform player;

    [SerializeField] private Animator dragonAnimator;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 5)
        {
            dragonAnimator.SetBool("isPatrolling", true);
        }

        float distance = Vector3.Distance(player.position, this.transform.position);
        if (distance < 8)
        {
            dragonAnimator.SetBool("isChasing", true);
        }
        else
        {
            dragonAnimator.SetBool("isChasing", false);
        }
    }
}
