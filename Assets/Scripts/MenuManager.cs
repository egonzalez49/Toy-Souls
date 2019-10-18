using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    //Start the game whenever the user clicks 'New Game'
    public void StartGame()
    {
        /*
         * TODO: Add the name of the main scene to load after clicking 'New Game's
         */
        //SceneManager.LoadScene("SCENE_NAME");
    }

    //Close application whenever the user clicks 'Quit'
    public void QuitGame()
    {
        Application.Quit();
    }
}
