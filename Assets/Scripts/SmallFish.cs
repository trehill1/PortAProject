using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class SmallFish : MonoBehaviour
{
    public SmallFishState currentState;

    public IdleState idleState;
    public FlopState flopState;
    public HurtState hurtState;

    void Update()
    {
        RunStateMachine();
    }

    private void RunStateMachine()
    {
        SmallFishState nextState = currentState?.RunCurrentState();

        if (nextState != null)
        {
            SwitchToTheNextState(nextState);
        }
    }

    private void SwitchToTheNextState(SmallFishState nextState)
    {
        currentState = nextState;
    }
}
