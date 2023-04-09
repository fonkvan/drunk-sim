using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GGetBanana : Goal
{

    public GGetBanana()
    {
        className = "GGetBanana";
        description = "Grab a banana";
    }
    
    public override bool IsFulfilled()
    {
        return GameManager.Instance.collectedBanana;
    }
}
