using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            PlayGame();
        }

        if(Input.GetKeyDown(KeyCode.Q))
        {
            QuitGame();
        }
    }


    public void PlayGame()
    {
        SceneManager.LoadScene("Living Room");
    }

    public void QuitGame()
    {
        print("Quitting");
        Application.Quit();
    }
}
