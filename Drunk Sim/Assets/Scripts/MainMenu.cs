using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

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
        #if UNITY_EDITOR
            print("Quitting");
            
            //Application.Quit() only works for built games, not in engine
            //below also must be encapsulated by preprocessor directive, as you can't build game
            //with following line of code without it
            UnityEditor.EditorApplication.isPlaying = false; 
        #endif
        
        Application.Quit();
    }
}
