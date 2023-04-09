using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GoalUI goalUI;
    
    private Queue<Goal> goals;
    private Goal currentGoal = null;
    private bool displayedInitialGoal = false;
    public bool collectedBeer = false;
    public bool collectedBanana = false;
    
    [SerializeField] 
    private GGetAnotherBeer getAnotherBeer;
    [SerializeField]
    private GGoToBathroom goToBathroom;
    [SerializeField] 
    private GGetBanana getBanana;

    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        goals = new Queue<Goal>();
        
        //instantiate goals, must do here or params won't populate
        getAnotherBeer = new GGetAnotherBeer();
        goToBathroom = new GGoToBathroom();
        getBanana = new GGetBanana();
        
        //Add goals here:
        goals.Enqueue(getAnotherBeer);
        goals.Enqueue(goToBathroom);
        goals.Enqueue(getBanana);
        currentGoal = goals.Peek();
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
                SceneManager.LoadScene("Win Screen");
            }
        }
    }

    private void DisplayCurrentGoal()
    {
        //goalUI.DisplayGoalText(currentGoal);
    }

    public bool GoalIsType(string T)
    {
        return goals.Peek().className == T;
    }

    public bool IsPlayerNearGoal()
    {
        Vector3 goalDest = goals.Peek().destination;
        RaycastHit hit;

        if (Physics.SphereCast(goalDest, 2f, Vector3.up, out hit, 0.1f))
        {
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                return true;
            }
        }
        
        return false;
    }
}
