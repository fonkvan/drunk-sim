using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GGetAnotherBeer : Goal
{
    void Start()
    {
        description = "Throw down another beer";
    }
    public override bool IsFulfilled()
    {
        return GameManager.Instance.collectedBeer;
    }
}
