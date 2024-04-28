using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float jumpCooldown = 1f;
    public float maxHealth = 100f;
    public float knockbackForce = 10f;
    public float knockbackDuration = 0.5f;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private bool canJump = true;
    private float jumpTimer;
    public Animator animator;
    private float currentHealth;
    private bool isKnockback = false;
    private float knockbackTimer;

    public GameObject enemy;
    public Image healthBarImage;
    public TextMeshProUGUI deathText;

    public bool isTouchingFish = false;

    public AudioSource src;
    public AudioClip playerHit;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (!isKnockback)
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
            src.clip = playerHit;
            src.Play();
        }
        else if (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.Q) || Input.GetMouseButtonDown(1))
        {
            PlayKickAnimation();
            src.clip = playerHit;
            src.Play();
        }

        // Update knockback timer
        if (isKnockback)
        {
            knockbackTimer -= Time.deltaTime;
            if (knockbackTimer <= 0)
            {
                isKnockback = false;
            }
        }
        UpdatePlayerHealth();

        if (isTouchingFish)
        {
            if (enemy != null)
            {
                BigFish bigFish = enemy.gameObject.GetComponent<BigFish>();
                SmallFish smallFish = enemy.gameObject.GetComponent<SmallFish>();

                // Check for player attack input and apply knockback to the enemy
                if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0))
                {
                    if (bigFish != null)
                    {
                        bigFish.ApplyKnockback(transform.position);
                        bigFish.TakeDamage(10);
                    }
                    else if (smallFish != null)
                    {
                        smallFish.ApplyKnockback(transform.position);
                        smallFish.TakeDamage(20);
                    }
                }
                else if (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.Q) || Input.GetMouseButtonDown(1))
                {
                    if (bigFish != null)
                    {
                        bigFish.ApplyKnockback(transform.position);
                        bigFish.TakeDamage(10);
                    }
                    else if (smallFish != null)
                    {
                        smallFish.ApplyKnockback(transform.position);
                        smallFish.TakeDamage(20);
                    }
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            isTouchingFish = true;

            enemy = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            isTouchingFish = false;
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
        currentHealth -= damageAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void ApplyKnockback(Vector3 enemyPosition)
    {
        isKnockback = true;
        knockbackTimer = knockbackDuration;

        Vector2 knockbackDirection = (transform.position - enemyPosition).normalized;
        rb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
    }

    private void UpdatePlayerHealth()
    {
        float fillAmount = currentHealth / maxHealth;
        healthBarImage.fillAmount = fillAmount;

    }

    private void Die()
    {
        // Handle player death
        deathText.gameObject.SetActive(true);
        animator.SetBool("Died", true);
        Debug.Log("Player died!");
    }
}