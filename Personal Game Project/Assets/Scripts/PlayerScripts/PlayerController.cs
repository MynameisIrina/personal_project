using System;
using System.Linq.Expressions;
using Cinemachine.Utility;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class PlayerController : MonoBehaviour
{
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
    private bool stairs;
    
    // PICK UP/PUT AWAY ATTRIBUTES
    public bool pick_up { get; private set; }
    public bool put_away { get; private set; }
    private Inventory inventory;
    [SerializeField] private UI_Inventory uiInventory;
    
    // CLIMB
    [SerializeField] private GameObject rayGeneral;
    private GameObject objectInFrontOfPlayer;
    private bool ladderCollision;
    private bool climbLadder;
    
    
    [Header("Player Step Climb:")]
    [SerializeField] private GameObject lower_ray;
    [SerializeField] private GameObject higher_ray;
    [SerializeField] private float step;
    private RaycastHit hit;
    [SerializeField] private GameObject rayCastOrigin;
    
    // SWORD ATTACK ATTRIBUTES
    [SerializeField] private GameObject sword;
    private bool swordAttack;
    
    // ARROW ATTRIBUTES
    [SerializeField] private GameObject arrow;
    [SerializeField] private GameObject crosshair;


    private Vector3 zeroVector = new Vector3(0, 0, 0);
    private Quaternion initialRot;
    
    // GUN ATTRIBUTES
    [SerializeField] private GameObject gun;
    
    

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        animator = this.GetComponent<Animator>();
        rb.detectCollisions = true;
        inventory = new Inventory();
        crosshair.SetActive(false);
        gun.SetActive(true);
        uiInventory.SetInventory(inventory);
        GetComponent<OpenNote>().enabled = false;
        GetComponent<PickUpWeapon>().enabled = false;
        GetComponent<ClimbLadder>().enabled = false;
    }
    

    private void FixedUpdate()
    {
        if (aim_input)
        {
            gun.SetActive(true);
            crosshair.SetActive(true);
            Aiming();
        }
        else
        {
            animator.SetBool("gunaAim", false);
            gun.SetActive(false);
            crosshair.SetActive(false);
            
        }
        
        bool grounded = Physics.Raycast(rayCastOrigin.transform.position, Vector3.down, 0.5f);
        Debug.DrawRay(rayCastOrigin.transform.position, Vector3.down);
        if (!grounded)
        {
            rb.AddForce(Physics.gravity * 2.5f, ForceMode.Acceleration);
        }

        MovePlayer();

        if (sword.activeSelf && swordAttack)
        {
            animator.SetBool("swordAttack", true);
        }
        else
        {
            animator.SetBool("swordAttack", false);

        }
        
        
        
    }
    private void Update()
    {

        // detect objects in front of the player
        RaycastHit objectsHit;
        if (Physics.Raycast(rayGeneral.transform.position, transform.TransformDirection(Vector3.forward), out objectsHit,
                7f))
        {
            objectInFrontOfPlayer = objectsHit.collider.gameObject;
        }

        if (fire_input)
        {
            arrow.transform.position += Vector3.forward;
        }
        
    }

    [SerializeField] private GameObject mock;
    [SerializeField] private Transform spawn;
    

    private void Aiming()
    {
        rb.rotation = Quaternion.Euler(rb.rotation.eulerAngles 
                                              + new Vector3(-lookInput.y, lookInput.x, 0f));
        // if (newDirection != Vector3.zero)
        // {
        //     animator.SetBool("arrowMove", true);
        //     animator.SetFloat("velocityX", move_value.x);
        //     animator.SetFloat("veloctyZ", move_value.y);
        // }
        // else
        // {
        //     animator.SetBool("arrowMove", false);
        //
        // }
        

    }

    [SerializeField] private float arrowMovingSpeed;
    [SerializeField] private CameraController cameraController;
    private Vector3 lookInput;
    private void MovePlayer()
    {
        Vector3 camF = cameraController.getCamera().transform.forward;
        Vector3 camR = cameraController.getCamera().transform.right;
        camF.y = 0;
        camR.y = 0;
        camF = camF.normalized;
        camR = camR.normalized;
        
        new_position = (move_value.x * camR + move_value.y * camF);

        // rotate player regardless of camera position
        if (!aim_input && new_position != zeroVector)
        {
            Quaternion turn = Quaternion.LookRotation(new_position);
            Quaternion target_rotation = Quaternion.RotateTowards(rb.rotation, turn, 360);
            rb.MoveRotation(target_rotation);
            
            /* calculate length of the vector to interpolate
         between walk and run animations. See BlendTree.
         */
            
        }

        float speed_blendtree = Mathf.Clamp01(new_position.magnitude); 
        animator.SetFloat("Walk Magnitude", speed_blendtree);
        
        SetAnimations(speed_blendtree);
        if (aim_input!)
        {
            rb.MovePosition(rb.position + (new_position * speed * Time.fixedDeltaTime));
        }
        else
        {
            rb.MovePosition(rb.position + (new_position * arrowMovingSpeed * Time.fixedDeltaTime));

        }

    }

    // private void ClimbStairs()
    // {
    //     RaycastHit lowerHit;
    //     if (Physics.Raycast(lower_ray.transform.position, transform.TransformDirection(Vector3.forward), out lowerHit, 1.5f))
    //     {
    //         RaycastHit higherHit;
    //         if (!Physics.Raycast(higher_ray.transform.position, transform.TransformDirection(Vector3.forward), out higherHit, 1.7f))
    //         {
    //             stairs = true;
    //             if (animator.GetBool("forward"))
    //             {
    //                 rb.position -= new Vector3(0, -step, 0);
    //             }
    //         }
    //     }
    //     else
    //     {
    //         stairs = false;
    //     }
    //     
    //     Debug.DrawRay(lower_ray.transform.position, transform.TransformDirection(Vector3.forward), Color.red);

    //}
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Door"))
        {
            gameObject.GetComponent<OpenDoor>().enabled = true;
        }

        if (other.CompareTag("Note"))
        {
            gameObject.GetComponent<OpenNote>().enabled = true;
        }

        if (other.CompareTag("Weapon"))
        {
            gameObject.GetComponent<PickUpWeapon>().enabled = true;
            
        }
        if (other.CompareTag("Ladder"))
        {
            ladderCollision = true;
        }

        if (other.CompareTag("LadderEnd"))
        {
            climbLadder = false;
            ladderCollision = false;
            animator.SetBool("climb", false);
            animator.SetBool("clamber", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Door"))
        {
            gameObject.GetComponent<OpenDoor>().enabled = false;
        }
        if (other.CompareTag("Note"))
        {
            gameObject.GetComponent<OpenNote>().enabled = false;
        }
        if (other.CompareTag("Weapon"))
        {
            gameObject.GetComponent<PickUpWeapon>().enabled = false;
        }
        if (other.CompareTag("Ladder"))
        {
            ladderCollision = false;
            animator.SetBool("climb", false);
        }

        if (other.CompareTag("LadderEnd"))
        {
            //rb.useGravity = true;
            animator.SetBool("clamber", false);
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
          // Quaternion rotation = Quaternion.Euler(0, camera.transform.eulerAngles.y, 0);
          // rb.MoveRotation(rotation);

    }

    public void ReceiveInputMovement(Vector2 _movement)
    {
        move_value = _movement;
    }

    // public void ReceiveInputJump(bool _jump)
    // {
    //     jump = _jump;
    //     if (jump && !animator.GetBool("jump"))
    //     {
    //         animator.SetBool("jump", true);
    //         Vector3 jump_vector = Vector3.up * amount_to_jump;
    //         rb.AddForce(jump_vector, ForceMode.Impulse);
    //
    //     }
    //     else
    //     {
    //         animator.SetBool("jump", false);
    //
    //     }
    // }

    public void ReceiveSwordAttackInput(bool sword_attack)
    {
        swordAttack = sword_attack;
        
    }
    public void receiveInputLook2(Vector2 _look)
    {
        lookInput = _look;
    }

    public bool getSwordAttack()
    {
        return swordAttack;
    }

    public void ReceiveAimInput(bool _aim)
    {
        aim_input = _aim;
        if (aim_input)
        {
            animator.SetBool("gunAim", true);
            //transform.RotateAround(gameObject.transform.position, Vector3.up, 90);
        }
        
        else
        {
            animator.SetBool("gunAim", false);

        }
    }

    public void ReceiveClimbInput(bool _climb)
    {
        climbLadder = _climb;
    }

    public void ReceivePickUpInput(bool _pick_up)
    {
        pick_up = _pick_up;
    }

    public void ReceivePutAwayInput(bool _put_away)
    {
        put_away = _put_away;
    }

    public void ReceiveFireInput(bool fire)
    {
        fire_input = fire;
    }

    public Inventory GetInventory()
    {
        return inventory;
    }

    













}
