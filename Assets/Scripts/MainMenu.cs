using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{

    public static int FishKillCount = 0;
    public TextMeshProUGUI fishKillCountText;

    public void Start()
    {
        UpdateFishKillCountText();
    }

    public void PlayGame()
    {
        FishKillCount = 0;
        Player.currentHealth = 100;
        SceneManager.LoadScene(2);
    }

    private void UpdateFishKillCountText()
    {
        if (fishKillCountText != null)
        {
            fishKillCountText.text = "Fish Killed: " + FishKillCount;
        }
    }
}
