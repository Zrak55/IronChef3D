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
    private Animator animator;

    private bool rolling;
    private bool sprinting;

    private bool canSprint = true;


    protected Vector3 inputDirection;
    protected Vector3 direction;


    GameObject cam;

    public GameObject model;
    private float modelRotSpeed = 360f;

    float targetRollWeight;

    Quaternion targetRotation;
    Vector3 currentMove;
    Vector3 targetMoveSpeed;
    [Space]
    public Transform CamFollowPoint;
    public Transform CamLookPoint;

    [HideInInspector]
    public bool MouseOff;

    bool knockbackIframe = false;

    private void Awake()
    {


        controller = GetComponent<CharacterController>();
        stats = GetComponent<PlayerStats>();
        costmanager = GetComponent<PlayerCostCooldownManager>();
        hitpoints = GetComponent<PlayerHitpoints>();
        animator = GetComponentInChildren<Animator>();
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
        var currentHorizontalMove = currentMove;
        currentHorizontalMove.y = 0;

        animator.SetFloat("Speed", currentHorizontalMove.magnitude);



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

        if(animator.GetLayerWeight(animator.GetLayerIndex("Roll Layer")) != targetRollWeight)
        {

            float tickRate = Time.deltaTime * 10;
            if (animator.GetLayerWeight(animator.GetLayerIndex("Roll Layer")) > targetRollWeight)
                tickRate *= -1;
            animator.SetLayerWeight(animator.GetLayerIndex("Roll Layer"), Mathf.Clamp(animator.GetLayerWeight(animator.GetLayerIndex("Roll Layer")) + tickRate, 0, 1));
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
            if (!rolling && inputDirection != Vector3.zero && stats.TrySpendStamina(costmanager.RollCost) )
            {
                direction = direction.normalized * rollSpeed;
                currentMove.x = direction.x;
                currentMove.z = direction.z;
                rolling = true;
                hitpoints.InvincibilityFrame(0.75f);
                KnockbackIframe(0.75f);
                animator.SetBool("Rolling", true);
                targetRollWeight = 1;
                Invoke("UndoRoll", (7f / 6f));
            }
        }

        //TryUndoRoll();
    }
    public void UndoRoll()
    {
        rolling = false;
        animator.SetBool("Rolling", false);
        targetRollWeight = 0;
    }

    private void TrySprint()
    {
        float sprintVal = InputControls.controls.Gameplay.Sprint.ReadValue<float>();
        if(sprintVal > 0 && !rolling && canSprint && inputDirection != Vector3.zero)
        {
            if(stats.TrySpendStamina(costmanager.SprintCostPerSecond * Time.deltaTime))
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

    public void ForceDirection(Vector3 force)
    {
        if(!knockbackIframe)
        {
            currentMove.x = force.x;
            currentMove.z = force.z;

            Invoke("AccelChange", 0.5f);
            Invoke("UndoAccelChange", 0.75f);

            KnockbackIframe(0.75f);
        
        
        }
    }

    void AccelChange()
    {

        acceleration *= 10;
    }
    void UndoAccelChange()
    {
        acceleration /= 10;
    }

    void KnockbackIframe(float time)
    {
        knockbackIframe = true;
        Invoke("RemoveKnockbackIframe", time);
    }
    void RemoveKnockbackIframe()
    {
        knockbackIframe = false;
    }

}
