﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    [Header("Movement")]
    public float movementSpeed = 5;
    public float gravity = -9.81f;
    public float dashDistance;
    public float dashTime;

    [Header("Ground Checking")]
    public Transform groundCheck;
    public float groundCheckLength;
    public LayerMask groundLayer;

    private CharacterController characterController;
    private Vector2 movementDirection;
    private Vector3 movementVelocity;
    private bool isGrounded;
    private float yVelocity = 0;
    private bool isDashing;
    private float dashTimer;
    Vector3 dashVelocity;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

   
    void OnDash()
    {
        isDashing = true;
        dashVelocity = (transform.forward * dashDistance * Time.fixedDeltaTime) / dashTime;
        dashTimer = dashTime;
    }

    private void FixedUpdate()
    {
        Dash();

        isGrounded = Physics.Raycast(groundCheck.position, Vector3.down, groundCheckLength, groundLayer);

        if (isGrounded)
        {
            yVelocity = -2;
            print("is grounded");
        }
        else
        {
            print("In Air");
            yVelocity += (gravity * Time.fixedDeltaTime);
        }
        movementDirection.Normalize();
        movementVelocity = new Vector3(movementDirection.x,0,movementDirection.y) * movementSpeed * Time.fixedDeltaTime;
        characterController.Move(new Vector3(movementVelocity.x, yVelocity * Time.fixedDeltaTime, movementVelocity.z));
    }

    void Dash ()
    {
        if (isDashing)
        {
            if (dashTimer <= 0)
            {
                isDashing = false;
            }
            else
            {
                dashTimer -= Time.fixedDeltaTime;
                characterController.Move(dashVelocity);
            }
        }    
        
    }

    private void OnMove(InputValue value)
    {
        movementDirection = value.Get<Vector2>();
        print(movementDirection);
    }
}
