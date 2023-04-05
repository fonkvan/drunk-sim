using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public GoalUI goalUI;
    
    private Queue<Goal> goals;
    private Goal currentGoal;
    private bool displayedInitialGoal = false;
    public bool collectedBeer = false;
    
    [SerializeField] 
    private GGetAnotherBeer getAnotherBeer;// = new GGetAnotherBeer();

    public static GameManager Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        currentGoal = goals.Peek();
        //Add goals here:
        //goals.Enqueue(new GGetAnotherBeer());
    }

    private void Update()
    {
        if (!displayedInitialGoal)
        {
            DisplayCurrentGoal();
            displayedInitialGoal = true;
        }
        
        if (currentGoal.IsFulfilled())
        {
            goals.Dequeue();
            if (goals.Count > 0)
            {
                currentGoal = goals.Peek();
                DisplayCurrentGoal();
            }
            else
            {
                //win?
            }
        }
    }

    private void DisplayCurrentGoal()
    {
        goalUI.DisplayGoalText(currentGoal);
    }
}
