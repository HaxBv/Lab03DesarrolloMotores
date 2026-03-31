using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public InputSystem_Actions inputs;

    [SerializeField] private Vector2 moveInput;

    private CharacterController controller;

    public float MoveSpeed = 5f;
    public float RotationSpeed = 5f;

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
    }

    void Update()
    {
        transform.Rotate(Vector3.up * moveInput.x * RotationSpeed * Time.deltaTime);

        Vector3 moveDir = transform.forward * moveInput.y * MoveSpeed * Time.deltaTime;

        controller.Move(moveDir);



    }
}
