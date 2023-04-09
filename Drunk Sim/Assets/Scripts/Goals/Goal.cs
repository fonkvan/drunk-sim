using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//MUST MARK INHERITED CLASSES WITH [System.Serializable]
[System.Serializable]
public abstract class Goal
{
    public string className;
    public string description;
    public Vector3 destination;

    public abstract bool IsFulfilled();
}
