using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class EndGameManager : MonoBehaviour
{
    public Text soulsText;
    public Text winText;
    public Text headerText;
    public SoulScript soulScript;

    private CanvasGroup canvasGroup;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            Debug.LogError("CanvasGroup not found.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp("space"))
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("TitleScreenScene");
        }
    }

    public void EndScreen(bool win)
    {
        if (win)
        {
            headerText.text = "You Win";
            winText.text = "You beat Luigi!";
        } else
        {
            headerText.text = "You Lose";
            winText.text = "You got rolled by Luigi!";
        }

        soulsText.text = "Souls: " + soulScript.GetSouls();

        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f;
        Time.timeScale = 0f;
    }
}
