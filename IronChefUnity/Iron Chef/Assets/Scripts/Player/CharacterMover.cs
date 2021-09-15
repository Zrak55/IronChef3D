using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerStats))]
[RequireComponent(typeof(PlayerCostCooldownManager))]
[RequireComponent(typeof(PlayerHitpoints))]
public class CharacterMover : MonoBehaviour
{
    [Tooltip("The base speed of the player")]
    public float baseSpeed;
    public float sprintSpeed;
    public float acceleration;
    public float jumpSpeed;
    public float rollSpeed;

    [Tooltip("The player's current speed")]
    public float speed;

    [Space]

    private CharacterController controller;
    private PlayerStats stats;
    private PlayerCostCooldownManager costmanager;
    private PlayerHitpoints hitpoints;

    private bool rolling;
    private bool sprinting;

    private bool canSprint = true;


    protected Vector3 inputDirection;
    protected Vector3 direction;


    GameObject cam;

    public GameObject model;
    private float modelRotSpeed = 360f;


    Quaternion targetRotation;
    Vector3 currentMove;
    Vector3 targetMoveSpeed;
    [Space]
    public Transform CamFollowPoint;
    public Transform CamLookPoint;

    [HideInInspector]
    public bool MouseOff;

    private void Awake()
    {


        controller = GetComponent<CharacterController>();
        stats = GetComponent<PlayerStats>();
        costmanager = GetComponent<PlayerCostCooldownManager>();
        hitpoints = GetComponent<PlayerHitpoints>();

        speed = baseSpeed;

        rolling = false;


        currentMove = new Vector3(0, 0, 0);

        cam = FindObjectOfType<Camera>().gameObject;

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        inputDirection = getMovementInputVector();

        //Movement of character
        Vector3 camFacing = cam.transform.forward;
        camFacing.y = 0;
        camFacing = camFacing.normalized;

        if(!rolling)
        {
            direction = IronChefUtils.RotateFlatVector3(inputDirection, camFacing);
        }
        direction = direction.normalized * speed;
        targetMoveSpeed = direction;
        targetMoveSpeed.y = -9.8f;


        TryRoll();
        TryJump();
        TrySprint();


        currentMove = Vector3.MoveTowards(currentMove, targetMoveSpeed, acceleration * Time.deltaTime);



        controller.Move(currentMove * Time.deltaTime);


        //Rotation of model
        if (direction != Vector3.zero)
        {
            var oldRot = model.transform.rotation;
            model.transform.LookAt(transform.position + direction);
            targetRotation = model.transform.rotation;
            model.transform.rotation = oldRot;
        }
        model.transform.rotation = Quaternion.RotateTowards(model.transform.rotation, targetRotation, modelRotSpeed * Time.deltaTime);

        if(MouseOff && IronChefUtils.MouseIsHidden() == false)
        {
            IronChefUtils.HideMouse();
        }
    }

    private Vector3 getMovementInputVector()
    {
        if(rolling)
        {
            return direction.normalized;
        }
        else
        {
            Vector3 input = InputControls.controls.Gameplay.Move.ReadValue<Vector2>();
            input = input.normalized;
            input.z = input.y;
            input.y = 0;
            return input;

        }
    }

    private void TryJump()
    {
        //Jump
        if (InputControls.controls.Gameplay.Jump.triggered)
        {
            if (stats.TrySpendStamina(costmanager.JumpCost))
            {
                currentMove.y = jumpSpeed;

            }
        }
    }
    private void TryRoll()
    {
        //Roll
        if (InputControls.controls.Gameplay.Roll.triggered)
        {
            if (!rolling && stats.TrySpendStamina(costmanager.RollCost) && inputDirection != Vector3.zero)
            {
                direction = direction.normalized * rollSpeed;
                currentMove.x = direction.x;
                currentMove.z = direction.z;
                rolling = true;
                hitpoints.InvincibilityFrame(0.75f);
            }
        }

        TryUndoRoll();
    }
    private void TryUndoRoll()
    {
        if (rolling)
        {
            Vector3 horizontalMove = currentMove;
            horizontalMove.y = 0;
            if (horizontalMove.magnitude <= speed * 1.1f)
                rolling = false;
        }
    }

    private void TrySprint()
    {
        float sprintVal = InputControls.controls.Gameplay.Sprint.ReadValue<float>();
        if(sprintVal > 0 && !rolling && canSprint)
        {
            if(stats.TrySpendStamina(costmanager.SpringCostPerSecond * Time.deltaTime))
            {
                sprinting = true;
            }
            else
            {
                sprinting = false;
                StartCoroutine(sprintDelay());
            }
        }
        else
        {
            sprinting = false;
        }
    }

    public float GetBaseSpeed()
    {
        if(sprinting)
        {
            return sprintSpeed;
        }
        else
        {
            return baseSpeed;
        }
    }

    public bool IsSprinting()
    {
        return sprinting;
    }

    public bool IsRolling()
    {
        return rolling;
    }

    private IEnumerator sprintDelay()
    {
        canSprint = false;
        yield return new WaitForSeconds(1f);
        canSprint = true;
    }
}
