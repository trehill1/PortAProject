using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FlopState : SmallFishState
{
    public IdleState idleState;
    public SmallFish smallFish;

    private bool hasFlopped = false;
    public float flopForce = 575;

    public float movementSpeed = 5; // Speed of horizontal movement

    private float timer = 0f;
    private float transitionTime = 2f;

    public SpriteRenderer smallFishSprite;

    private bool flipFish = true;
    private float flipFishTimer = 0f;

    public override SmallFishState RunCurrentState()
    {
        Rigidbody2D rb = smallFish.GetComponent<Rigidbody2D>();

        if (!hasFlopped)
        {
            rb.AddForce(new Vector2(rb.velocity.x, flopForce));

            float randomMovement = Random.Range(-1f, 1f); // Random value between -1 and 1
            rb.velocity = new Vector2(randomMovement * movementSpeed, rb.velocity.y);


            hasFlopped = true;
        }

        if (flipFish)
        {
            if (flipFishTimer <= 0f)
            {
                smallFishSprite.flipX = false; // Face right
                flipFish = false; // Set flag to false for next flip
                flipFishTimer = 0.5f; // Set a random time for next flip
            }
        }
        else
        {
            if (flipFishTimer <= 0f)
            {
                smallFishSprite.flipX = true; // Face left
                flipFish = true; // Set flag to true for next flip
                flipFishTimer = 0.5f; // Set a random time for next flip
            }
        }

        // Decrease the timer each frame
        flipFishTimer -= Time.deltaTime;

        timer += Time.deltaTime;

        if (timer >= transitionTime)
        {
            timer = 0f;
            hasFlopped = false;
            return idleState;
        }



        return null;
    }
}
