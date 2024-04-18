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

    public float maxHealth = 100f;
    public float currentHealth;

    public float knockbackForce = 10f;
    public float knockbackDuration = 0.5f;
    private bool isKnockback = false;
    private float knockbackTimer;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }

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

    public void ApplyKnockback(Vector3 enemyPosition)
    {
        isKnockback = true;
        knockbackTimer = knockbackDuration;

        Vector2 knockbackDirection = (transform.position - enemyPosition).normalized;
        rb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        //animator.SetBool("Died", true);
        Debug.Log("Fish died!");
    }
}
