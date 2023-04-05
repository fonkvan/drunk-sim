using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//MUST MARK INHERITED CLASSES WITH [System.Serializable]
[System.Serializable]
public abstract class Goal
{
    public string description;
    public Transform destination;
    public GameObject goalObject; //optional
    private bool fulfilled = false;

    public abstract bool IsFulfilled();
}
