﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CanvasGroup))]
public class MenuManager : MonoBehaviour
{
    private CanvasGroup canvasGroup;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            Debug.LogError("CanvasGroup not found.");
        }
    }

    //Start the game whenever the user clicks 'New Game'
    public void StartGame()
    {
        /*
         * TODO: Add the name of the main scene to load after clicking 'New Game's
         */
        //SceneManager.LoadScene("SCENE_NAME");
        EventSystem.current.GetComponent<EventSystem>().SetSelectedGameObject(null);
    }

    //Close application whenever the user clicks 'Quit'
    public void QuitGame()
    {
        Application.Quit();
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("TitleScreenScene");
        EventSystem.current.GetComponent<EventSystem>().SetSelectedGameObject(null);
    }

    public void ResumeGame()
    {
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0f;
        Time.timeScale = 1f;
        EventSystem.current.GetComponent<EventSystem>().SetSelectedGameObject(null);
    }
}
