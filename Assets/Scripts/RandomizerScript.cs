using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizerScript : MonoBehaviour
{
    public GameObject Bigfish;
    public GameObject Smallfish;
    //public CutSceneFishing csFishing;

    private void Start()
    {
        float Enemy = CutSceneFishing.Enemy;
        print("Enemy is: " + Enemy);

        if(Enemy < 1)
        {
            Vector3 spawnPosition = new Vector3(6f, -1.53f, 0f);
            Quaternion spawnRotation = Quaternion.identity;
            GameObject spawnedObject = Instantiate(Bigfish, spawnPosition, spawnRotation);
        }
        else
        {
            Vector3 spawnPosition = new Vector3(6f, -1.53f, 0f);
            Quaternion spawnRotation = Quaternion.identity;
            GameObject spawnedObject = Instantiate(Smallfish, spawnPosition, spawnRotation);
        }
    }
}
