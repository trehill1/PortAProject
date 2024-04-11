using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigIdleState : BigFishState
{
    public BigMoveState bigMoveState;
    public BigJumpState bigJumpState;

    private float timer = 0f;
    private float idleTime = Random.Range(4f, 5f); // Idle time in seconds

    public override BigFishState BigRunCurrentState()
    {
        timer += Time.deltaTime;

        if (timer >= idleTime)
        {
            timer = 0f;
            int randomValue = Random.Range(0,10); // Get a random value between 0 and 10

            if (randomValue > 2) // 80% chance for move state
            {
                return bigMoveState;
            }
            else // 20% chance for jump state
            {
                return bigJumpState;
            } 
        }

        return null; // Stay in BigIdleState
    }
}
