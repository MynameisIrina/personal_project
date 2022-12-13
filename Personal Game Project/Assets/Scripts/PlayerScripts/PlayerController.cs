using System;
using System.Collections;
using System.Linq.Expressions;
using Cinemachine.Utility;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class PlayerController : MonoBehaviour
{
    private Vector2 move_value;
    private Animator animator;
    public Vector3 new_position { get; private set; }
    private Vector3 newCam_position;
    private Vector3 target;
    // move
    private Rigidbody rb;
    [SerializeField] private float speed;
    private float input_magnitude;
    // jump
    [SerializeField] private float amount_to_jump;
    private float distance_to_ground;
    private bool jump;
    // rotate
    private Quaternion new_camera_position;
    // look
    private Vector2 look_input;
    float distance_to_target = 5f;
    // aim
    private bool aim_input;
    // pick up/put away
    public bool pickUp { get; private set; }
    public bool putAway { get; private set; }
    private Inventory inventory;
    [SerializeField] private UI_Inventory uiInventory;
    private Item.ItemType currentItem;



    // sword attack
    [SerializeField] private GameObject sword;
    private bool swordAttack;
    
    // arrow
    [SerializeField] private GameObject arrow;
    [SerializeField] private GameObject crosshair;
    
    private Quaternion initialRot;
    private bool wasAiming;
    private SelectItem selectItem;
    
    // GUN ATTRIBUTES
    [SerializeField] private GameObject gun;
    
    [SerializeField] private float arrowMovingSpeed;
    [SerializeField] private CameraController cameraController;
    private Vector3 lookInput;
    
    

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        animator = this.GetComponent<Animator>();
        rb.detectCollisions = true;
        inventory = new Inventory();
        crosshair.SetActive(false);
        gun.SetActive(false);
        uiInventory.SetInventory(inventory);
        GetComponent<OpenNote>().enabled = false;
        GetComponent<PickUpWeapon>().enabled = false;
        GetComponent<ClimbLadder>().enabled = false;
        wasAiming = false;
        selectItem = gameObject.GetComponent<SelectItem>();
    }
    

    private void FixedUpdate()
    {
        checkAimingInput();
        MovePlayer();
        checkSwordAttackAnimation();
    }

    private void checkSwordAttackAnimation()
    {
        if (sword.activeSelf && swordAttack)
        {
            animator.SetBool("swordAttack", true);
        }
        else
        {
            animator.SetBool("swordAttack", false);
        }
    }

    private void checkAimingInput()
    {
        if (aim_input && currentItem == Item.ItemType.Arrow && selectItem.ifItemisSelected())
        {
            animator.SetBool("arrowAim", true);
            if (currentItem == Item.ItemType.Arrow)
            {
                if (new Vector2(move_value.x, move_value.y) != Vector2.zero)
                {
                    animator.SetBool("arrowMove", true);
                    animator.SetFloat("velocityX", move_value.x);
                    animator.SetFloat("veloctyZ", move_value.y);
                }
                else
                {
                    animator.SetBool("arrowMove", false);
                }
            }
            AimingRotation();
            wasAiming = true;
        }
        else if (aim_input && currentItem == Item.ItemType.Gun && selectItem.ifItemisSelected() )
        {
            AimingRotation();
            crosshair.SetActive(true);
            animator.SetBool("gunAim", true);
            wasAiming = true;
        }
        else
        {
            if (wasAiming)
            {
                // set deafult position after aiming
                rb.rotation = Quaternion.Euler(0f, rb.rotation.y, rb.rotation.z);
                wasAiming = false;
            }
            
            animator.SetBool("gunAim", false);
            animator.SetBool("arrowAim", false);
            crosshair.SetActive(false);
        }
    }

    private void AimingRotation()
    {
        rb.rotation = Quaternion.Euler(rb.rotation.eulerAngles 
                                              + new Vector3(-lookInput.y, lookInput.x, 0f));
        
    }
    
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
        if (!aim_input && new_position != Vector3.zero)
        {
            Quaternion turn = Quaternion.LookRotation(new_position);
            Quaternion target_rotation = Quaternion.RotateTowards(rb.rotation, turn, 360);
            rb.MoveRotation(target_rotation);
        }

        SetWalkAnimations();
        adjustPlayerSpeedAndMove();

    }

    private void adjustPlayerSpeedAndMove()
    {
        if (!aim_input)
        {
            rb.MovePosition(rb.position + (new_position * speed * Time.fixedDeltaTime));
        }
        else
        {
            rb.MovePosition(rb.position + (new_position * arrowMovingSpeed * Time.fixedDeltaTime));
        }
    }

    public Vector2 getMoveValues()
    {
        return new Vector2(move_value.x, move_value.y);
    }


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
        
    }
    
    

    /* calculate length of the vector to interpolate
       between walk and run animations. See BlendTree.*/
    private void SetWalkAnimations()
    {
        float speed_blendtree = Mathf.Clamp01(new_position.magnitude); 
        animator.SetFloat("Walk Magnitude", speed_blendtree);
        
        if (speed_blendtree > 0f)
        {
            animator.SetBool("forward", true);
        }
        else
        {
            animator.SetBool("forward", false);
        }
    }
    

    public void setCurrentItem(Item.ItemType item)
    {
        currentItem = item;
    }

    public Item.ItemType getCurrentItem()
    {
        return currentItem;
    }

    public void ReceiveInputMovement(Vector2 _movement)
    {
        move_value = _movement;
    }

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
    }
    

    public void ReceivePickUpInput(bool _pick_up)
    {
        pickUp = _pick_up;
    }

    public void ReceivePutAwayInput(bool _put_away)
    {
        putAway = _put_away;
    }

    [SerializeField] private GameObject arrowInHand;
    public void ReceiveFireInput(bool fire_input)
    {
        if (fire_input && getMoveValues() == Vector2.zero)
        {
            arrowInHand.SetActive(false);
            animator.SetTrigger("arrowFire");
            StartCoroutine(waitForAnimation());
        }
    }
    
    void TakeArrow()
    {
        arrowInHand.SetActive(true);
    }

    IEnumerator waitForAnimation()
    {
        yield return new WaitForSeconds(1);
        animator.ResetTrigger("arrowFire");
    }
    

    public Inventory GetInventory()
    {
        return inventory;
    }

    













}
