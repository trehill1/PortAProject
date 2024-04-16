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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player playerScript = collision.gameObject.GetComponent<Player>();
            if (playerScript != null)
            {
                float bigFishAction = Random.Range(0, 10);

                if (bigFishAction < 5)
                {
                    Debug.Log("Small Fish does not Attack");
                }
                else
                {
                    playerScript.TakeDamage(10);
                    playerScript.ApplyKnockback(transform.position);
                }
            }
        }
    }
}
