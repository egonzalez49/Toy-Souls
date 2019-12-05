using PC;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class PauseManager : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    private bool allowAction;

    void Awake()
    {
        allowAction = false;
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            Debug.LogError("CanvasGroup not found.");
        }
    }

    void Update()
    {
        // TODO: Change to include controller input.
        if ((!GeneralManager.gamePausedOrDone || allowAction) && Input.GetKeyUp(KeyCode.Escape) || Input.GetButtonUp(StaticStrings.Start))
        {
            // If the menu is open, close it.
            if (canvasGroup.interactable)
            {
                allowAction = false;
                GeneralManager.UpdateGameState(false);
                canvasGroup.interactable = false;
                canvasGroup.blocksRaycasts = false;
                canvasGroup.alpha = 0f;
                Time.timeScale = 1f;
            }
            else
            {
                allowAction = true;
                GeneralManager.UpdateGameState(true);
                canvasGroup.interactable = true;
                canvasGroup.blocksRaycasts = true;
                canvasGroup.alpha = 1f;
                Time.timeScale = 0f;
            }
        }
    }
}
