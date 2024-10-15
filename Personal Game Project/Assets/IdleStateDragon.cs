using UnityEngine;

public class IdleStateDragon : StateMachineBehaviour
{
    private float timer;
    private Transform player;
    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.gameObject.GetComponent<DragonController>().GetCurrentHealth() <= 0f)
        {
            animator.SetBool("isDead", true);
        }
        
        timer += Time.deltaTime;
        if (timer > 5)
        {
            animator.SetBool("isPatrolling", true);
        }
        float distance = Vector3.Distance(animator.transform.position, player.position);
        
        if (distance < 75)
        {
            animator.SetBool("isChasing", true);
        }
        else
        {
            animator.SetBool("isChasing", false);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
