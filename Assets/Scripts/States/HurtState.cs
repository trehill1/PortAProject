using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtState : SmallFishState
{
    public override SmallFishState RunCurrentState()
    {
        return this;
    }
}
