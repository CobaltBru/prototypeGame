using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CController : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float jumpForce = 5.0f;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public float groundCheckRadius = 0.2f;
    public float groundFriction = 5.0f; // 바닥 마찰력
    public float slopeFriction = 0f; // 오르막길 등 경사면 마찰력

    private Rigidbody2D rb;
    private bool isGrounded;
    private bool isOnSlope;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.sharedMaterial = new PhysicsMaterial2D()
        {
            friction = groundFriction
        };
    }

    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (isGrounded)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1f, groundLayer);
            if (hit && Mathf.Abs(hit.normal.x) > 0.1f)
            {
                isOnSlope = true;
                rb.sharedMaterial.friction = slopeFriction;
            }
            else
            {
                isOnSlope = false;
                rb.sharedMaterial.friction = groundFriction;
            }
        }
        else
        {
            isOnSlope = false;
            rb.sharedMaterial.friction = 0f;
        }

        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
}
