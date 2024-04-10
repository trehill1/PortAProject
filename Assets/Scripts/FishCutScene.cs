using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FishCutScene : MonoBehaviour
{
    private float animationDuration = 3f;
    private float elapsedTime = 0f;

    private void Update()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= animationDuration)
        {
            SceneManager.LoadScene(1);
        }
    }
}
