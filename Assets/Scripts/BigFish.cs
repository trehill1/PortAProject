using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class BigFish : MonoBehaviour
{
    public BigFishState currentState;

    public BigIdleState bigIdleState;
    public BigHurtState bigHurtState;
    public BigJumpState bigJumpState;
    public BigMoveState bigMoveState;

    public float maxHealth = 100f;
    public float currentHealth;

    public float knockbackForce = 10f;
    public float knockbackDuration = 0.5f;
    private bool isKnockback = false;
    private float knockbackTimer;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    public Animator animator;

    public Image healthBarImage;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentHealth = maxHealth;

        GameObject healthBarObject = GameObject.FindWithTag("FishHealthBar");
        if (healthBarObject != null)
        {
            healthBarImage = healthBarObject.GetComponent<Image>();
            print("health bar set");
        }
        else
        {
            Debug.LogError("Fish health bar not found in the scene!");
        }
    }

    void Update()
    {
        BigRunStateMachine();

        if (isKnockback)
        {
            knockbackTimer -= Time.deltaTime;
            if (knockbackTimer <= 0)
            {
                isKnockback = false;
            }
        }
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player playerScript = collision.gameObject.GetComponent<Player>();
            if (playerScript != null)
            {
                float bigFishAction = Random.Range(0, 10);

                if (bigFishAction < 10)
                {
                    
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

    public void ApplyKnockback(Vector3 enemyPosition)
    {
        isKnockback = true;
        knockbackTimer = knockbackDuration;

        Vector2 knockbackDirection = (transform.position - enemyPosition).normalized;
        Debug.Log(knockbackForce);

        rb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        print(currentHealth);
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
        UpdateFishHealth();
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void UpdateFishHealth()
    {
        float fillAmount = currentHealth / maxHealth;
        healthBarImage.fillAmount = fillAmount;

    }

    private void Die()
    {
        MainMenu.FishKillCount += 1;
        animator.SetBool("Died", true);
        Debug.Log("Fish died!");
        SceneManager.LoadScene(0);
    }
}
