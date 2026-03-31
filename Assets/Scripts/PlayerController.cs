using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public InputSystem_Actions inputs;

    [SerializeField] private Vector2 moveInput;

    private CharacterController controller;

    public float MoveSpeed = 5f;
    public float RotationSpeed = 5f;
    public float JumpForce = 1.0f;
    public float gravity = 9.81f;
    public float verticalVelocity = 0f;



    private void Awake()
    {
        inputs = new();
        controller = GetComponent<CharacterController>();
    }

    void Start()
    {
        
    }

    private void OnEnable()
    {
        inputs.Enable();



        inputs.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        inputs.Player.Move.canceled += ctx => moveInput = Vector2.zero;

        inputs.Player.Jump.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
    }

    void Update()
    {

        OnMove();
        //OnsimpleMove();

    }
    public void OnMove()
    {

        transform.Rotate(Vector3.up * moveInput.x * RotationSpeed * Time.deltaTime);

        Vector3 moveDir = transform.forward * moveInput.y * MoveSpeed;

        verticalVelocity -= gravity * Time.deltaTime;

        //Vector3 JumpDir = transform.up * moveInput.y * JumpForce;

        moveDir.y = verticalVelocity;

        controller.Move(moveDir*Time.deltaTime);
   
    }

    /*public void OnsimpleMove()
    {
        transform.Rotate(Vector3.up * moveInput.x * RotationSpeed * Time.deltaTime);

        Vector3 moveDir = transform.forward * moveInput.y * MoveSpeed;

        Vector3 JumpDir = transform.up * moveInput.y * JumpForce; 

        controller.SimpleMove(moveDir);
    }
    */


}
