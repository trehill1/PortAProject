using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigKickState : BigFishState
{
    public BigIdleState bigIdleState;

    public override BigFishState BigRunCurrentState()
    {
        Debug.Log("Big Fish Kicks!");
        return bigIdleState;
    }

}
