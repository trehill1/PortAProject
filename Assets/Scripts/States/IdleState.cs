using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : SmallFishState
{
    public FlopState flopState;

    private float timer = 0f;
    private float transitionTime = 1f; // Time after which to transition to FlopState

    public override SmallFishState RunCurrentState()
    {
        timer += Time.deltaTime;

        if (timer >= transitionTime)
        {
            return flopState;
        }

        return null; // Stay in IdleState
    }
}
