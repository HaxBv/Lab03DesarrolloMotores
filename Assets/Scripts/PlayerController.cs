using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public InputSystem_Actions inputs;

    [SerializeField] private Vector2 moveInput;

    private CharacterController controller;

    public float MoveSpeed = 5f;
    public float RotationSpeed = 5f;
    public float JumpForce = 10f;
    public float PushForce = 4f;
    public float DashForce = 1.0f;





    private bool IsRunning;


    public float DashDuration = 1.0f;
    private float dashTimer = 0f;


    //public float gravity = 9.81f;
    public float verticalVelocity = 0f;

    private bool IsDashing;

    private bool IsKnockback;
    private Vector3 PushDir;
    public float KnockbackForce;

    public float KnockbackDuration = 1.0f;
    private float knockbackTimer = 0f;


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

        inputs.Player.Jump.performed += OnJump;

        inputs.Player.Sprint.performed += ctx => MoveSpeed = MoveSpeed*2;
        inputs.Player.Sprint.canceled += ctx => MoveSpeed = MoveSpeed*0.5f;

        inputs.Player.Dash.performed += OnDash;
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

        verticalVelocity += Physics.gravity.y * Time.deltaTime;

        if(controller.isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f;
        }


        //Vector3 JumpDir = transform.up * moveInput.y * JumpForce;

        moveDir.y = verticalVelocity;




        if(IsDashing)
        {
            //convertir dash a un barrido

            moveDir = (transform.forward * DashForce * (dashTimer / DashDuration));


            dashTimer -= Time.deltaTime;

            if(dashTimer <= 0)
            {
                IsDashing = false;
            }
        }





        if (IsKnockback)
        {
            //convertir dash a un barrido

            moveDir = (PushDir * KnockbackForce * (knockbackTimer / KnockbackDuration));


            knockbackTimer -= Time.deltaTime;

            if (knockbackTimer <= 0)
            {
                IsKnockback = false;
            }
        }


        controller.Move(moveDir*Time.deltaTime);
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        if (!controller.isGrounded)return;

        verticalVelocity = JumpForce;


        
    }

    private void OnDash(InputAction.CallbackContext context)
    {
        IsDashing = true;
        dashTimer = DashDuration;

    }
  
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {

        if (hit.rigidbody != null)
        {

            PushDir = (transform.position - hit.transform.position).normalized;
            IsKnockback = true;
            knockbackTimer = KnockbackDuration;

            Debug.Log(hit.gameObject.name);

        }
       
        



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
