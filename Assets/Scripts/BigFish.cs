using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigFish : MonoBehaviour
{
    public BigFishState currentState;

    public BigIdleState bigIdleState;
    public BigHurtState bigHurtState;
    public BigJumpState bigJumpState;
    public BigKickState bigKickState;
    public BigMoveState bigMoveState;
    public BigPunchState bigPunchState;

    void Update()
    {
        BigRunStateMachine();
    }

    private void BigRunStateMachine()
    {
        BigFishState nextState = currentState?.BigRunCurrentState();

        if (nextState != null)
        {
            BigSwitchToTheNextState(nextState);
        }
    }

    private void BigSwitchToTheNextState(BigFishState nextState)
    {
        currentState = nextState;
    }
}
