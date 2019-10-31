using PC;
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
    public static bool gameIsFinished = false;

    private CanvasGroup canvasGroup;
    private Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            Debug.LogError("CanvasGroup not found.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (canvasGroup.alpha == 1f && (Input.GetKeyUp("space") || Input.GetButton(StaticStrings.AButton)))
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("TitleScreenScene");
        }
    }

    public void EndScreen(bool win)
    {
        gameIsFinished = true;

        if (win)
        {
            headerText.text = "You Win";
            winText.text = "You beat Billy!";
        } else
        {
            headerText.text = "You Lose";
            winText.text = "You got stomped by Billy!";
        }

        soulsText.text = "Souls: " + soulScript.GetSouls();

        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f;
        anim.SetTrigger("gameIsFinished");
    }

    public void SetTimeScaleOff()
    {
        Time.timeScale = 0f;
    }
}
