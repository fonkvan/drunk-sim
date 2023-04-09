using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class GGoToBathroom : Goal
{
    public GGoToBathroom()
    {
        className = "GGoToBathroom";
        description = "Go to the bathroom";
        destination = new Vector3(-32.18f, 6.527f, -26.129f);
    }

    public override bool IsFulfilled()
    {
        if (SceneManager.GetActiveScene().name == "Bathroom")
        {
            return GameManager.Instance.IsPlayerNearGoal();
        }
        return false;
    }
}
