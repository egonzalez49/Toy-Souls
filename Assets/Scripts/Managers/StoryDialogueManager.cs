using PC;
using UnityEngine;

public class StoryDialogueManager : MonoBehaviour
{
    private CanvasGroup canvasGroup;

    void Awake()
    {
        Time.timeScale = 0f;
        GeneralManager.UpdateGameState(true);
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            Debug.LogError("CanvasGroup not found.");
        }
    }

    void Update()
    {
        // TODO: Change to include controller input.
        if (Input.GetKeyUp("space"))
        {
            GeneralManager.UpdateGameState(false);
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
            canvasGroup.alpha = 0f;
            Time.timeScale = 1f;

        }
    }
}
