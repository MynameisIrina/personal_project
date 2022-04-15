using System;
using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class PlayerController : MonoBehaviour
{
    //private Vector2 move_value;
    [SerializeField] private GameObject camera;
    private Vector2 move_value;
    private Animator animator;
    public Vector3 new_position { get; private set; }
    private Vector3 newCam_position;
    private Vector3 target;

    // MOVE ATTRIBUTES
    private Rigidbody rb;
    [SerializeField] private float speed;
    private float input_magnitude;

    // JUMP ATTRIBUTES
    [SerializeField] private float amount_to_jump;
    private float distance_to_ground;
    private bool jump;
    
    // ROTATION ATTRIBUTES
    private Quaternion new_camera_position;
    
    //LOOK ATTRIBUTES 
    private Vector2 look_input;
    float distance_to_target = 5f;

    // AIMING ATTRIBUTES
    private bool aim_input;

    // FIRE ATTRIBUTES
    private bool fire_input;
    private Canvas crosshair;
    private bool stairs;
    
    // PICK UP/PUT AWAY ATTRIBUTES
    public bool pick_up { get; private set; }
    public bool put_away { get; private set; }
    

    [Header("Player Step Climb:")]
    [SerializeField] private GameObject lower_ray;
    [SerializeField] private GameObject higher_ray;
    [SerializeField] private float step;
    private RaycastHit hit;
    [SerializeField] private GameObject rayCastOrigin;



    void Awake()
    {


    }

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        animator = this.GetComponent<Animator>();
        rb.detectCollisions = true;
        crosshair = FindObjectOfType<Canvas>();
        crosshair.enabled = false;
    }


    private void FixedUpdate()
    {
        if (aim_input)
        {
            RotateAim();
        }
        
        bool grounded = Physics.Raycast(rayCastOrigin.transform.position, Vector3.down, 0.5f);
        Debug.DrawRay(rayCastOrigin.transform.position, Vector3.down);
        if (!grounded)
        {
            rb.AddForce(Physics.gravity * 2.5f, ForceMode.Acceleration);
        }

        MovePlayer();
        
        //ability to climb up the stairs using rigidbody
        ClimbStairs();
        

    }

    private void MovePlayer()
    {
        Vector3 camF = camera.transform.forward;
        Vector3 camR = camera.transform.right;
        camF.y = 0;
        camR.y = 0;
        camF = camF.normalized;
        camR = camR.normalized;
        
        new_position = (move_value.x * camR + move_value.y * camF);


        // rotate player regardless of camera position.
        if (!aim_input)
        {
            Quaternion turn = Quaternion.LookRotation(new_position);
            Quaternion target_rotation = Quaternion.RotateTowards(rb.rotation, turn, 360);
            rb.MoveRotation(target_rotation);
        }


        /* calculate length of the vector to interpolate
         between walk and run animations. See BlendTree.
         */
            
        float speed_blendtree = Mathf.Clamp01(new_position.magnitude); 
        animator.SetFloat("Walk Magnitude", speed_blendtree);
        
        SetAnimations(speed_blendtree);
        rb.MovePosition(transform.position + (new_position * speed * Time.fixedDeltaTime));
    }
    
    

    private void ClimbStairs()
    {
        RaycastHit lowerHit;
        if (Physics.Raycast(lower_ray.transform.position, transform.TransformDirection(Vector3.forward), out lowerHit, 1.5f))
        {
            RaycastHit higherHit;
            if (!Physics.Raycast(higher_ray.transform.position, transform.TransformDirection(Vector3.forward), out higherHit, 1.7f))
            {

                stairs = true;
                if (animator.GetBool("forward"))
                {
                    rb.position -= new Vector3(0, -step, 0);
                }
            }
        }
        else
        {
            stairs = false;
        }
        
        Debug.DrawRay(lower_ray.transform.position, transform.TransformDirection(Vector3.forward), Color.red);
        //Debug.DrawRay(higher_ray.transform.position, transform.TransformDirection(Vector3.forward), Color.blue);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Door"))
        {
            gameObject.GetComponent<OpenDoor>().enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Door"))
        {
            gameObject.GetComponent<OpenDoor>().enabled = false;
        }
    }

    private void SetAnimations(float speed_blendtree)
    {
        if (speed_blendtree > 0f)
        {
            animator.SetBool("forward", true);

        }
        else
        {
            animator.SetBool("forward", false);
        }
    }

    public void RotateAim()
    {
        Quaternion rotation = Quaternion.Euler(camera.transform.eulerAngles.x, camera.transform.eulerAngles.y, 1);
        rb.MoveRotation(rotation);
    }

    public void ReceiveInputMovement(Vector2 _movement)
    {
        move_value = _movement;
    }

    public void ReceiveInputJump(bool _jump)
    {
        jump = _jump;
        if (jump && !animator.GetBool("jump"))
        {
            animator.SetBool("jump", true);
            Vector3 jump_vector = Vector3.up * amount_to_jump;
            rb.AddForce(jump_vector, ForceMode.Impulse);

        }
        else
        {
            animator.SetBool("jump", false);

        }
    }

    public void ReceiveAimInput(bool _aim)
    {
        aim_input = _aim;
        if (aim_input)
        {
            animator.SetBool("aim", true);
            crosshair.enabled = true;
        }
        else
        {
            animator.SetBool("aim", false);
            crosshair.enabled = false;

        }
    }

    public void ReceivePickUpInput(bool _pick_up)
    {
        pick_up = _pick_up;
    }

    public void ReceivePutAwayInput(bool _put_away)
    {
        put_away = _put_away;
    }
    













}
