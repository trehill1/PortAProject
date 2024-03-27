using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float jumpCooldown = 1f;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private bool canJump = true;
    private float jumpTimer;

    public Animator animator;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        // Move left and right
        float horizontalInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);

        // Flip sprite based on movement direction
        if (horizontalInput < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (horizontalInput > 0)
        {
            spriteRenderer.flipX = false;
        }

        // Jump
        if (Input.GetKeyDown(KeyCode.W) && canJump)
        {
            Jump();
            canJump = false;
            jumpTimer = jumpCooldown;
        }

        // Update jump cooldown timer
        if (!canJump)
        {
            jumpTimer -= Time.deltaTime;
            if (jumpTimer <= 0)
            {
                canJump = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0))
        {
            PlayPunchAnimation();
        }
        else if (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.Q) || Input.GetMouseButtonDown(1))
        {
            PlayKickAnimation();
        }
    }

    private void PlayPunchAnimation()
    {
        animator.SetBool("Punch", true);
    }

    private void PlayKickAnimation()
    {
        animator.SetBool("Kick", true);
    }

    private void OnAnimationFinished()
    {
        animator.SetBool("Punch", false);
        animator.SetBool("Kick", false);
    }

    private void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
}