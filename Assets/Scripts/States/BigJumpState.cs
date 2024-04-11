using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigJumpState : BigFishState
{
    public BigIdleState bigIdleState;
    public BigFish bigFish;

    private Rigidbody2D rb;
    private bool hasJumped = false;

    public float jumpForce = 1000f; // Force applied for jumping
    public float jumpDuration = 1.5f; // Duration of jumping in seconds

    private float timer = 0f;
    private float transitionTime = 2f;

    public override BigFishState BigRunCurrentState()
    {
        if (!hasJumped)
        {
            rb = bigFish.GetComponent<Rigidbody2D>();

            // Apply the jump force vertically
            rb.AddForce(Vector2.up * jumpForce);

            hasJumped = true;
        }

        // Check if the BigFish has landed
        if (rb.velocity.y <= 0 && Mathf.Approximately(rb.velocity.x, 0f))
        {
            timer += Time.deltaTime;

            if (timer >= transitionTime)
            {
                timer = 0f;
                hasJumped = false;
                rb.velocity = Vector2.zero; // Reset velocity when transitioning to idle
                return bigIdleState;
            }
        }

        return null; // Stay in BigJumpState until landing and transition time are completed
    }
}

