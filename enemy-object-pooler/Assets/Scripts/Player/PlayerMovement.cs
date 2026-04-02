using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    private CharacterController characterController;

    [Header("Input")]
    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction sprintAction;
    private Vector2 direction;

    [Header("Movement Setings")]
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float sprintSpeed = 10f;
    [SerializeField] private float sprintTransitSpeed = 5f;
    [SerializeField] private float gravity = 9.81f;
    [SerializeField] private float jumpHeight = 2f;

    private float verticalVelocity;
    private float speed;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions.FindAction("Move");
        jumpAction = playerInput.actions.FindAction("Jump");
        sprintAction = playerInput.actions.FindAction("Sprint");
    }

    void Update()
    {
        InputManagement();
        Movement();
    }

    private void InputManagement()
    {
        direction = moveAction.ReadValue<Vector2>();
    }

    private void Movement()
    {
        GroundMovement();
    }

    private void GroundMovement()
    {
        Vector3 move = transform.right * direction.x + transform.forward * direction.y;
        
        if(sprintAction.IsPressed())
        {
            speed = Mathf.Lerp(speed, sprintSpeed, sprintTransitSpeed* Time.deltaTime);
        }
        else
        {
            speed = Mathf.Lerp(speed, walkSpeed, sprintTransitSpeed * Time.deltaTime);
        }

        move *= speed;
        move.y = VerticalForceCalculation();

        characterController.Move(move * Time.deltaTime);
    }

    private float VerticalForceCalculation()
    {
        if (characterController.isGrounded)
        {
            verticalVelocity = -1f;

            if (jumpAction.triggered)
            {
                verticalVelocity = Mathf.Sqrt(jumpHeight * 2 * gravity);
            }
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }
        return verticalVelocity;
    }
}