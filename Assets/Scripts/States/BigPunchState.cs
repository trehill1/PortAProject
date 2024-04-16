using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigPunchState : BigFishState
{
    public BigIdleState bigIdleState;

    public override BigFishState BigRunCurrentState()
    {
        Debug.Log("Big Fish Punches!");
        return bigIdleState;
    }
}
