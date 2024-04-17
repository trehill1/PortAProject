using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutSceneFishing : MonoBehaviour
{ 
    public GameObject Animation;
    public Animator animator;

    private bool isAnimationFinished = false;
    private float animationDuration = 2.5f; 
    private float elapsedTime = 0f;
    public float Enemy;

    private void Awake()
    {
        // Enable the cut scene objects
        Animation.SetActive(true);

        // Play Animation
        PlayFishingAnimation();
    }
    private void Update()
    {
        if (!isAnimationFinished)
        {
            elapsedTime += Time.deltaTime;

            if (elapsedTime >= animationDuration)
            {
                isAnimationFinished = true;
                OnAnimationFinished();
            }
        }
    }



    private void PlayFishingAnimation()
    {
        animator.SetBool("run", true);
    }

    private void OnAnimationFinished()
    {
        animator.SetBool("run", false);

        Enemy = Random.Range(0, 2);

        if (Enemy < 1)
        {
            SceneManager.LoadScene(3);
        }
        else
        {
            SceneManager.LoadScene(4);
        }
    }
}
