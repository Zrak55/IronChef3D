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
    float baseAcceleration;
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
    private bool sprintToggled = false;


    protected Vector3 inputDirection;
    protected Vector3 direction;


    GameObject cam;
    PlayerCamControl camControl;

    public GameObject model;
    private float modelRotSpeed = 360f;

    float targetRollWeight;

    [HideInInspector]public Quaternion targetRotation;
    Vector3 currentMove;
    Vector3 targetMoveSpeed;
    [Space]
    public Transform CamFollowPoint;
    public Transform CamLookPoint;

    [HideInInspector]
    public bool MouseOff;

    bool knockbackIframe = false;

    [Space]
    public List<Material> AllowedWalkMaterials;
    [Header("Effects")]
    public ParticleSystem LeftFootEffect;
    public ParticleSystem RightFootEffect;
    public ParticleSystem SwampWalkEffect;

    

    private void Awake()
    {


        controller = GetComponent<CharacterController>();
        stats = GetComponent<PlayerStats>();
        costmanager = GetComponent<PlayerCostCooldownManager>();
        hitpoints = GetComponent<PlayerHitpoints>();
        animator = GetComponentInChildren<Animator>();
        speed = baseSpeed;

        baseAcceleration = acceleration;

        rolling = false;


        currentMove = new Vector3(0, 0, 0);

        cam = FindObjectOfType<Camera>().gameObject;
        camControl = cam.GetComponentInParent<PlayerCamControl>();

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    private void FixedUpdate()
    {


        InWaterCheck();
    }

    void Update()
    {
        CheckStuck();

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
        targetMoveSpeed.y = 0f;


        TryRoll();
        //TryJump();
        TrySprint();


        currentMove = Vector3.MoveTowards(currentMove, targetMoveSpeed, acceleration * Time.deltaTime);
        currentMove.y = -48f;
        var currentHorizontalMove = currentMove;
        currentHorizontalMove.y = 0;

        animator.SetFloat("Speed", currentHorizontalMove.magnitude);

        TryMove();

        currentMove = currentHorizontalMove;


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

    public void CheckStuck()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position + Vector3.up, Vector3.down, out hit, 1000, 1 << LayerMask.NameToLayer("Terrain")))
        {
            if (!AllowedWalkMaterials.Contains(hit.collider.gameObject.GetComponent<MeshRenderer>().sharedMaterial))
            {
                Debug.Log("Attempting unstuck procedure!");

                //We are stuck
                bool unstucked = false;

                int dist = 1;
                while (!unstucked)
                {
                    for (int i = 0; i < 360 && !unstucked; i += 30)
                    {
                        Vector3 checkUnstuckPos = transform.position + (new Vector3(Mathf.Cos(Mathf.Deg2Rad * i), 0, Mathf.Sin(Mathf.Deg2Rad * i)).normalized * dist);
                        if (Physics.Raycast(checkUnstuckPos + Vector3.up*100, Vector3.down, out hit, 1000, 1 << LayerMask.NameToLayer("Terrain")))
                        {
                            if (AllowedWalkMaterials.Contains(hit.collider.gameObject.GetComponent<MeshRenderer>().sharedMaterial))
                            {
                                //Found a place to go
                                transform.position = hit.point;
                                unstucked = true;
                            }
                        }
                    }

                    dist++;
                }

            }
        }
    }


    public void TryMove()
    {
        var oldPos = transform.position;



        controller.Move(currentMove * Time.deltaTime);


        /*
        //This solution looks bad ingame. Perhaps there's a better answer that involves a coroutine where transform is changed every update to look more natural
        Collider[] enemy;
        Vector3 movement;
        do
        {
            enemy = Physics.OverlapSphere(transform.position, 1, 1 << LayerMask.NameToLayer("Enemy"));
            if (enemy == null || enemy.Length == 0)
                break;
            movement = Vector3.Cross(enemy[0].bounds.center, transform.position).normalized;
            movement.y = 0;
            Debug.Log(movement);
            controller.Move(movement);
        } while (enemy == null || enemy.Length == 0);
        */


        //Check illegal area to walk on
        RaycastHit hit;
        if(Physics.Raycast(transform.position + Vector3.up, Vector3.down, out hit, 1000, 1 << LayerMask.NameToLayer("Terrain")))
        {
            if(!AllowedWalkMaterials.Contains(hit.collider.gameObject.GetComponent<MeshRenderer>().sharedMaterial))
            {
                controller.Move(oldPos - transform.position);
            }
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
            if (costmanager.JumpOnCooldown == false && stats.TrySpendStamina(costmanager.JumpCost))
            {
                currentMove.y = jumpSpeed;
                costmanager.SetJumpCD();
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
                animator.SetTrigger("Roll");
                targetRollWeight = 1;
                Invoke("UndoRoll", (7f / 6f));
            }
        }

        //TryUndoRoll();
    }
    public void UndoRoll()
    {
        rolling = false;
        targetRollWeight = 0;
    }

    private void TrySprint()
    {
        if(InputControls.controls.Gameplay.Sprint.triggered)
        {
            sprintToggled = !sprintToggled;
        }

        if(sprintToggled && canSprint && inputDirection != Vector3.zero)
        {
                if (PlayerHitpoints.InCombat() == false || stats.TrySpendStamina(costmanager.SprintCostPerSecond * Time.deltaTime))
                {
                    sprinting = true;
                }
                else
                {
                    sprinting = false;
                    sprintToggled = false;
                    StartCoroutine(sprintDelay());
                }
            
        }
        else
        {
            sprinting = false;
            sprintToggled = false;
        }
    }

    public float GetBaseAcceleration()
    {
        return baseAcceleration;
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

    public void MakeFootstepEffect(bool isLeftFoot)
    {
        ParticleSystem effect;
        if(isLeftFoot)
        {
            effect = LeftFootEffect;
        }
        else
        {
            effect = RightFootEffect;
        }

        //TODO: Determine what material I am walking on here

        if (effect.isPlaying)
            effect.Stop();
        effect.Play();
    }

    public CharacterController getController()
    {
        return controller;
    }

    private void InWaterCheck()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position + 3 * Vector3.up, Vector3.down, out hit, 5, 1 << LayerMask.NameToLayer("WaterWading")))
        {
            SwampWalkEffect.transform.position = new Vector3(SwampWalkEffect.transform.position.x, hit.point.y, SwampWalkEffect.transform.position.z);
            var em = SwampWalkEffect.emission;
            var main = SwampWalkEffect.main;
            em.rateOverTime = currentMove.magnitude * 0.6f + 1.39f;

            main.startSize = 5.25f + currentMove.magnitude * 0.25f;
            main.startLifetime = 3f - Mathf.Clamp(currentMove.magnitude * 0.05f, 0, 2f);

            



            if (SwampWalkEffect.isPlaying == false)
                SwampWalkEffect.Play();
        }
        else
        {
            //SwampWalkEffect.transform.position = transform.position + Vector3.up * 2;
            if (SwampWalkEffect.isPlaying == true)
                SwampWalkEffect.Stop();
        }
    }
}
