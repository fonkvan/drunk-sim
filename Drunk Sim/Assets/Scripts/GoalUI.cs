using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GoalUI : MonoBehaviour
{
    public TextMeshProUGUI goalText;

    public void DisplayGoalText(Goal goal)
    {
        goalText.text = goal.description;
    }
}
