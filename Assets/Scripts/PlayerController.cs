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


    public float DashDuration = 1.0f;
    private float dashTimer = 0f;


    //public float gravity = 9.81f;
    public float verticalVelocity = 0f;

    private bool IsDashing;

    private Vector3 externalForce;
    public float pushDecay = 6f;

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

        inputs.Player.Sprint.performed += OnDash;
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

            //moveDir = (transform.forward * DashForce * (dashTimer / DashDuration));
            Vector3 dashDir = (transform.forward * DashForce * (dashTimer / DashDuration));

            moveDir.x = dashDir.x;
            moveDir.z = dashDir.z;


            dashTimer -= Time.deltaTime;

            if(dashTimer <= 0)
            {
                IsDashing = false;
            }
        }

        moveDir += externalForce;
        controller.Move(moveDir*Time.deltaTime);
        externalForce = Vector3.Lerp(externalForce, Vector3.zero, pushDecay * Time.deltaTime);
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
        if( hit.rigidbody != null )
        {

            Vector3 pushDir = (transform.position - hit.transform.position).normalized;

            pushDir.y = 0; // evitar que salga volando

            externalForce += pushDir * 5f; // ajusta este valor
            Debug.Log(hit.gameObject.name);
        }
    }
    /*
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {

        /*Vector3 PushDir = (hit.transform.position - transform.position).normalized;
       

        if (hit.rigidbody!= null && hit.rigidbody.linearVelocity == Vector3.zero)
        {
            hit.rigidbody.AddForce(PushDir * PushForce, ForceMode.Impulse);



            Debug.Log(hit.gameObject.name);
        }

        Vector3 PushDir = (transform.position - hit.transform.position).normalized;

        Vector3 moveDir = transform.forward * moveInput.y * MoveSpeed;

        if (IsKnockback)
        {
            //convertir dash a un barrido

            moveDir = -(PushDir * DashForce * (dashTimer / DashDuration));


            dashTimer -= Time.deltaTime;

            if(dashTimer <= 0)
            {
                IsKnockback = false;
            }
        }


        controller.Move(moveDir*Time.deltaTime);

    }*/
    /*public void OnsimpleMove()
    {
        transform.Rotate(Vector3.up * moveInput.x * RotationSpeed * Time.deltaTime);

        Vector3 moveDir = transform.forward * moveInput.y * MoveSpeed;

        Vector3 JumpDir = transform.up * moveInput.y * JumpForce; 

        controller.SimpleMove(moveDir);
    }
    */


}
