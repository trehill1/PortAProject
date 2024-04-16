using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigFish : MonoBehaviour
{
    public BigFishState currentState;

    public BigIdleState bigIdleState;
    public BigHurtState bigHurtState;
    public BigJumpState bigJumpState;
    public BigMoveState bigMoveState;

    public Animator animator;

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player playerScript = collision.gameObject.GetComponent<Player>();
            if (playerScript != null)
            {
                float bigFishAction = Random.Range(0, 10);

                if (bigFishAction < 4)
                {
                    Debug.Log("Big Fish does not Attack");
                }
                else if (bigFishAction < 7)
                {
                    animator.SetTrigger("TrPunch");
                    // Apply punch force to the player
                    playerScript.TakeDamage(10);
                    playerScript.ApplyKnockback(transform.position); // Pass BigFish's position to ApplyKnockback
                }
                else
                {
                    animator.SetTrigger("TrKick");
                    // Apply kick force to the player
                    playerScript.TakeDamage(20);
                    playerScript.ApplyKnockback(transform.position); // Pass BigFish's position to ApplyKnockback
                }
            }
        }
    }
}
