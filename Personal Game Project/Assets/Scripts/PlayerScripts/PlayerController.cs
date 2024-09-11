using System;
using System.Collections;
using System.Linq.Expressions;
using Cinemachine.Utility;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Debug = UnityEngine.Debug;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class PlayerController : MonoBehaviour
{
    private CharacterController characterController;

    [Header("Animator Attributes")] private Animator animator;

    [Header("Movement Attributes")] private Vector3 move;
    private float gravity = -9.81f;
    private float velocity;
    public Vector3 new_position { get; private set; }
    private Vector2 move_value;
    [SerializeField] private float speed;
    private float input_magnitude;

    [Header("Jump Attributes")] [SerializeField]
    private float amount_to_jump;

    private bool jump;

    [Header("Rotation Attributes")] private Quaternion new_camera_position;

    [Header("Looking Attributes")] private Vector3 look_input;

    [Header("Aiming Attributes")] private bool aim_input;
    private bool wasAiming;

    [Header("Inventory Attributes")] [SerializeField]
    private UI_Inventory uiInventory;

    private Inventory inventory;
    private Item.ItemType currentItem;
    private SelectItem selectItem;

    [Header("Sword Attributes")] [SerializeField]
    private GameObject sword;

    private bool swordAttack;

    [Header("Arrow Attributes")] [SerializeField]
    private GameObject crosshair;

    [SerializeField] private float arrowMovingSpeed;


    [Header("Gun Attributes")] [SerializeField]
    private GameObject gun;

    [SerializeField] private CameraController cameraController;

    public bool pickUp { get; private set; }
    public bool putAway { get; private set; }


    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = this.GetComponent<Animator>();
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

    private void Update()
    {
        СheckAimingInput();
        CheckSwordAttackAnimation();
        RotatePlayer();
        //ApplyGravity();
        SetWalkAnimations();
        MovePlayer();
    }


    private void CheckSwordAttackAnimation()
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


    private void СheckAimingInput()
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
        else if (aim_input && currentItem == Item.ItemType.Gun && selectItem.ifItemisSelected())
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
                // set default position after aiming
                transform.Rotate(new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z));
                animator.SetBool("arrowMove", false);
                animator.SetFloat("velocityX", 0);
                animator.SetFloat("veloctyZ", 0);
                wasAiming = false;
            }

            animator.SetBool("gunAim", false);
            animator.SetBool("arrowAim", false);
            crosshair.SetActive(false);
        }
    }

    private void AimingRotation()
    {
        Vector3 rotation = Vector3.up;
        transform.Rotate(rotation * look_input.x * 50f * Time.deltaTime);
    }

    private void RotatePlayer()
    {
        Vector3 camF = cameraController.GetCamera().transform.forward;
        Vector3 camR = cameraController.GetCamera().transform.right;
        camF.y = 0;
        camR.y = 0;
        camF = camF.normalized;
        camR = camR.normalized;

        new_position = (move_value.x * camR + move_value.y * camF);

        // rotate player regardless of camera position
        if (!aim_input && new_position != Vector3.zero)
        {
            Quaternion turn = Quaternion.LookRotation(new_position);
            Quaternion target_rotation = Quaternion.RotateTowards(transform.rotation, turn, 360);
            transform.rotation = Quaternion.Slerp(transform.rotation, turn, Time.deltaTime * 3f);
        }
    }

    private void ApplyGravity()
    {
        if (!characterController.isGrounded)
        {
            velocity += gravity * Time.deltaTime;
        }
        else
        {
            velocity = -1f;
        }

        move.y = velocity;
    }

    private void MovePlayer()
    {
        move = new Vector3(move_value.x, 0, move_value.y);
        ApplyGravity();

        if (!aim_input)
        {
            characterController.Move(move * Time.deltaTime * speed);
        }
        else
        {
            characterController.Move(move * Time.deltaTime * arrowMovingSpeed);
        }
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


    public void SetCurrentItem(Item.ItemType item)
    {
        currentItem = item;
    }

    public Item.ItemType GetCurrentItem()
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

    public void ReceiveInputLook2(Vector2 _look)
    {
        look_input = _look;
    }

    public void ReceiveJumpInput(bool _jump)
    {
        animator.SetBool("jump", _jump);
    }

    public Vector2 GetMoveValues()
    {
        return new Vector2(move_value.x, move_value.y);
    }

    public bool GetSwordAttack()
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
        if (fire_input && GetMoveValues() == Vector2.zero)
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