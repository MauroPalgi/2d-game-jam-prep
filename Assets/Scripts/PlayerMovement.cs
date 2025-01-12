using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rigidbody;

    [SerializeField]
    private BoxCollider2D groundCollider;

    [SerializeField]
    private LayerMask groundMask;

    [SerializeField]
    private float groundSpeed = 5f;

    [SerializeField]
    private float jumpForce = 10f;

    [SerializeField]
    private bool grounded;

    [SerializeField, Range(0f, 1f)]
    private float drag = 0.9f;

    [SerializeField]
    private Animator animator;

    private float xInput;
    private float yInput;

    private float lastDirection;

    void Start()
    {
        if (!rigidbody)
        {
            rigidbody = GetComponent<Rigidbody2D>();
        }
    }

    void Update()
    {
        GetInput();
        MoveWithInput();
    }

    private void MoveWithInput()
    {
        // Horizontal movement
        if (animator)
        {
            animator.SetBool("Running", Mathf.Abs(xInput) != 0.0f);
        }
        if (Mathf.Abs(xInput) > 0)
        {
            rigidbody.velocity = new Vector2(xInput * groundSpeed, rigidbody.velocity.y);
            float direction = Mathf.Sign(xInput);
            if (lastDirection != direction)
            {
                lastDirection = direction;
                float newDirection = direction < 0 ? -180 : 180;
                transform.Rotate(1, newDirection, 1);
            }
        }
        HandleJump();
    }

    private void HandleJump()
    {
        // Jumping
        if ((Input.GetButtonDown("Jump") || Mathf.Abs(yInput) > 0) && grounded)
        {
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpForce);
        }
    }

    private void GetInput()
    {
        xInput = Input.GetAxis("Horizontal");
        yInput = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        CheckGround();
        // Apply horizontal friction when grounded
        if (grounded && Mathf.Abs(xInput) == 0 && rigidbody.velocity.x <= 0)
        {
            rigidbody.velocity = new Vector2(rigidbody.velocity.x * drag, rigidbody.velocity.y);
        }

    }

    private void CheckGround()
    {
        grounded =
            Physics2D
                .OverlapAreaAll(groundCollider.bounds.min, groundCollider.bounds.max, groundMask)
                .Length > 0;
    }
}
