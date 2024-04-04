using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SmallFishState : MonoBehaviour
{
    public abstract SmallFishState RunCurrentState();
}
