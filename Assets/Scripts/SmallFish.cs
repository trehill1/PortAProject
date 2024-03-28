using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallFish : MonoBehaviour
{
    public Animator animator;
    public Rigidbody2D rb2D;

    public int maxHealth = 50;
    public int currentHealth;

    public float jumpForce = 400f;
    public float gravityScale = 2f;

    public float damageTaken;
    public float knockbackAngle;
    public float knockbackPower;
    public int hitstunFrames;
    public float hitboxDir;
    public bool gotHurt;

    private int frames;
    private bool isGrounded = false;

    void Start()
    {
        currentHealth = maxHealth;
        rb2D.gravityScale = gravityScale;
        SwitchState(State.Idle);
    }

    void Update()
    {
        HandleState();
    }

    void HandleState()
    {
        switch (currentState)
        {
            case State.Idle:
                HandleIdleState();
                break;
            case State.Flop:
                HandleFlopState();
                break;
            case State.Hurt:
                HandleHurtState();
                break;
            case State.Dead:
                // Handle dead state
                break;
        }
    }

    void HandleIdleState()
    {
        if (Random.Range(0, 100) < 2) // 2% chance to start flopping
        {
            SwitchState(State.Flop);
        }
    }

    void HandleFlopState()
    {
        if (isGrounded)
        {
            rb2D.AddForce(Vector2.up * jumpForce);
            isGrounded = false;
        }
    }

    void HandleHurtState()
    {
        rb2D.velocity = new Vector2(knockbackPower * Mathf.Cos(knockbackAngle * Mathf.Deg2Rad),
                                     knockbackPower * Mathf.Sin(knockbackAngle * Mathf.Deg2Rad));

        TakeDamage();
        frames = hitstunFrames;
    }

    void SwitchState(State newState)
    {
        currentState = newState;

        switch (newState)
        {
            case State.Idle:
                break;
            case State.Flop:
                break;
            case State.Hurt:
                break;
            case State.Dead:
                break;
        }
    }

    void TakeDamage()
    {
        currentHealth -= (int)damageTaken;
        Debug.Log("Fish: " + currentHealth + "/" + maxHealth);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    enum State
    {
        Idle,
        Flop,
        Hurt,
        Dead
    }

    private State currentState = State.Idle;
}
