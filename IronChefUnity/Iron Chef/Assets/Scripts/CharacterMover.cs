using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class CharacterMover : MonoBehaviour
{
    [Tooltip("The base speed of the player")]
    public float baseSpeed = 8;
    public float acceleration = 16;

    [Tooltip("The player's current speed")]
    public float speed;

    [Space]
    [SerializeField]
    private IronChefControls controls;

    private CharacterController controller;

    protected Vector3 inputDirection;


    public GameObject cam;

    public GameObject model;
    private float modelRotSpeed = 360f;


    Quaternion targetRotation;
    Vector3 currentMove;
    Vector3 targetMoveSpeed;


    private void Awake()
    {
        controls = new IronChefControls();
        controls.Enable();

        controller = GetComponent<CharacterController>();

        speed = baseSpeed;

        currentMove = new Vector3(0, 0, 0);
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
        //TODO: Acceleration vs instant velocity
        Vector3 camFacing = cam.transform.forward;
        camFacing.y = 0;
        camFacing = camFacing.normalized;

        var direction = IronChefUtils.RotateFlatVector3(inputDirection, camFacing);
        direction *= speed;
        targetMoveSpeed = direction;
        currentMove = Vector3.MoveTowards(currentMove, targetMoveSpeed, acceleration * Time.deltaTime);

        controller.SimpleMove(currentMove);

        //Rotation of model
        if(direction == Vector3.zero)
        {
            var oldRot = model.transform.rotation;
            model.transform.LookAt(transform.position + direction);
            targetRotation = model.transform.rotation;
            model.transform.rotation = oldRot;
        }        
        model.transform.rotation = Quaternion.RotateTowards(model.transform.rotation, model.transform.rotation, modelRotSpeed * Time.deltaTime);

    }

    private Vector3 getMovementInputVector()
    {
        Vector3 input = controls.Gameplay.Move.ReadValue<Vector2>();
        input = input.normalized;
        input.z = input.y;
        input.y = 0;
        return input;
    }



}
