using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigIdleState : BigFishState
{
    public BigMoveState bigMoveState;
    public BigJumpState bigJumpState;

    private float timer = 0f;

    public override BigFishState BigRunCurrentState()
    {
            float idleTime = Random.Range(1f, 1.5f); // Idle time in seconds
            timer += Time.deltaTime;

        if (timer >= idleTime)
        {
            timer = 0f;
            int randomValue = Random.Range(0,10); // Get a random value between 0 and 10

            if (randomValue > 1) // 90% chance for move state
            {
                return bigMoveState;
            }
            else // 10% chance for jump state
            {
                return bigJumpState;
            } 
        }

        return null; // Stay in BigIdleState
    }
}
