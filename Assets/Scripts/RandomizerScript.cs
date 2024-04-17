using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizerScript : MonoBehaviour
{
    public float Enemy;
    public GameObject Bigfish;
    public GameObject Smallfish;

    private void Start()
    {
        Enemy = PlayerPrefs.GetFloat("Enemy");

        if(Enemy < 1)
        {
            Vector3 spawnPosition = new Vector3(6f, -3.5f, 0f);
            Quaternion spawnRotation = Quaternion.identity;
            GameObject spawnedObject = Instantiate(Bigfish, spawnPosition, spawnRotation);
        }
        else
        {
            Vector3 spawnPosition = new Vector3(6f, -3.5f, 0f);
            Quaternion spawnRotation = Quaternion.identity;
            GameObject spawnedObject = Instantiate(Smallfish, spawnPosition, spawnRotation);
        }
    }
}