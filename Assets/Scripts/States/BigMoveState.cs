using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using static System.TimeZoneInfo;

public class BigMoveState : BigFishState
{
    public BigIdleState bigIdleState;
    public BigFish bigFish;

    private Rigidbody2D rb;
    private bool hasMoved = false;

    public float maxWalkForce = 1f;

    private float transitionTime = 2f;

    private bool hasFinishedMoving = false;

    public override BigFishState BigRunCurrentState()
    {
        if (!hasMoved)
        {
            rb = bigFish.GetComponent<Rigidbody2D>();

            float randomMovement = Random.Range(-0.75f, 0.75f); // Random value between -1 and 1

            // Calculate the walk force based on the random movement and clamp it within the max walk force range
            float walkForce = Mathf.Clamp(randomMovement * maxWalkForce, -maxWalkForce, maxWalkForce);

            // Randomize move duration within a specified range using Random.Range
            float moveDuration = Random.Range(1f, 1.5f); // Minimum and maximum duration of walking in seconds

            // Apply the calculated walk force over multiple frames using a coroutine with the randomized duration
            bigFish.StartCoroutine(WalkCoroutine(walkForce, moveDuration));

            hasMoved = true;
        }

        if (hasFinishedMoving)
        {
            hasFinishedMoving = false;
            hasMoved = false;
            return bigIdleState;
        }


  
        return null; // Stay in BigMoveState
    }

    private IEnumerator WalkCoroutine(float walkForce, float duration)
    {
        float timer = 0f;

        while (timer < duration)
        {
            // Apply the walk force horizontally each frame
            rb.AddForce(new Vector2(walkForce, 0f));
            timer += Time.deltaTime;
        }

        // Reset velocity at the end of the coroutine

        rb.velocity = Vector2.zero; // Reset velocity when transitioning to idle
        if (timer >= transitionTime)
        {
            timer = 0f;
        }

        hasFinishedMoving = true;

        yield return null;
    }
}
