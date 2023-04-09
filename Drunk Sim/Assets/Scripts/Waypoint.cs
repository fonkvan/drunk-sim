using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{

    public string goal; //should be goal class name (ex. GGoToBathroom)
    private bool setMeshActive = false;
    private bool goalWasActive = false;
    
    void Update()
    {
        //goal was previously active, but it has been completed
        if (goalWasActive && !GameManager.Instance.GoalIsType(goal))
        {
            GetComponent<MeshRenderer>().enabled = false;
        }
        
        if (!setMeshActive)
        {
            if (GameManager.Instance.GoalIsType(goal))
            {
                goalWasActive = true;
                setMeshActive = true;
                GetComponent<MeshRenderer>().enabled = true;
            }
        }
    }
}
