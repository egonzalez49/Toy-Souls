using PC;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class PauseManager : MonoBehaviour
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

    void Update()
    {
        // TODO: Change to include controller input.
        if (!EndGameManager.gameIsFinished && Input.GetKeyUp(KeyCode.Escape) || Input.GetButton(StaticStrings.Start))
        {
            // If the menu is open, close it.
            if (canvasGroup.interactable)
            {
                canvasGroup.interactable = false;
                canvasGroup.blocksRaycasts = false;
                canvasGroup.alpha = 0f;
                Time.timeScale = 1f;
            }
            else
            {
                canvasGroup.interactable = true;
                canvasGroup.blocksRaycasts = true;
                canvasGroup.alpha = 1f;
                Time.timeScale = 0f;
            }
        }
    }
}
