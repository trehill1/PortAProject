using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float jumpCooldown = 1f;
    public int maxHealth = 100;
    public float invincibilityDuration = .5f;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private bool canJump = true;
    private float jumpTimer;
    public Animator animator;
    private int currentHealth;
    private bool isInvincible = false;
    private float invincibilityTimer;

    public GameObject enemy;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentHealth = maxHealth;
    }

    private void Update()
    {
        // Move left and right
        float horizontalInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);

        // Flip sprite to face the enemy
        if (enemy != null)
        {
            if (transform.position.x < enemy.transform.position.x)
            {
                spriteRenderer.flipX = false;
            }
            else
            {
                spriteRenderer.flipX = true;
            }
        }

        // Flip sprite based on movement direction
        //This will be removed when a enemy is in scene
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

        // Update invincibility timer
        if (isInvincible)
        {
            invincibilityTimer -= Time.deltaTime;
            if (invincibilityTimer <= 0)
            {
                isInvincible = false;
                spriteRenderer.color = Color.white;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(10);
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

    public void TakeDamage(int damageAmount)
    {
        if (!isInvincible)
        {
            currentHealth -= damageAmount;
            if (currentHealth <= 0)
            {
                Die();
            }
            else
            {
                StartInvincibility();
            }
        }
    }

    private void StartInvincibility()
    {
        isInvincible = true;
        invincibilityTimer = invincibilityDuration;
        spriteRenderer.color = Color.red;
    }

    private void Die()
    {
        // Handle player death
        Debug.Log("Player died!");
    }
}