using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class Movement : MonoBehaviour
{
    private Rigidbody characterRB;
    private Vector3 movementInput;
    public Vector3 movementVector;

    [SerializeField] private float baseSpeed = 5f;
    [SerializeField] private float sprintMultiplier = 1.5f;
    [SerializeField] private float crouchMultiplier = 0.5f;

    [SerializeField] private float jumpForce = 8f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.2f;

    private bool isGrounded;
    private bool isSprinting = false;
    private bool isCrouching = false;
    private float currentSpeedMultiplier = 1f;

    void Start()
    {
        characterRB = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);

        if (movementInput != Vector3.zero)
        {
            movementVector = transform.right * movementInput.x + transform.forward * movementInput.z;
            movementVector.y = 0;
        }

        UpdateSpeedMultiplier();

        Vector3 velocity = new Vector3(
            movementVector.x * baseSpeed * currentSpeedMultiplier,
            characterRB.velocity.y,
            movementVector.z * baseSpeed * currentSpeedMultiplier
        );

        characterRB.velocity = velocity;
    }

    private void UpdateSpeedMultiplier()
    {
        if (isCrouching)
        {
            currentSpeedMultiplier = crouchMultiplier;
        }
        else if (isSprinting)
        {
            currentSpeedMultiplier = sprintMultiplier;
        }
        else
        {
            currentSpeedMultiplier = 1f;
        }
    }

    private void OnMovement(InputValue input)
    {
        movementInput = new Vector3(input.Get<Vector2>().x, 0, input.Get<Vector2>().y);
    }

    private void OnMovementStop(InputValue input)
    {
        movementVector = Vector3.zero;
    }

    private void OnJump(InputValue input)
    {
        if (isGrounded)
        {
            characterRB.velocity = new Vector3(characterRB.velocity.x, 0f, characterRB.velocity.z);
            characterRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void OnSprint(InputValue input)
    {
        isSprinting = true;
    }

    private void OnSprintStop(InputValue input)
    {
        isSprinting = false;
    }

    private void OnCrouch(InputValue input)
    {
        isCrouching = true;
    }

    private void OnCrouchStop(InputValue input)
    {
        isCrouching = false;
    }
}
