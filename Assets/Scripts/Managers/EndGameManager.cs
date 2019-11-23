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
    public static bool gameIsFinished = false;
    public CanvasGroup bossCanvasGroup;

    private PlayerSouls playerSouls;
    private CanvasGroup canvasGroup;
    private Animator anim;

    void Awake()
    {
        gameIsFinished = false;
        anim = GetComponent<Animator>();
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            Debug.LogError("CanvasGroup not found.");
        }
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        playerSouls = playerObject.GetComponent<PlayerSouls>();
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
        bossCanvasGroup.alpha = 0;
        string won = win ? "win" : "lose";
        //Logger.WriteToFile("Game ended in a : " + won + ".");
        //Logger.WriteToFile("Souls at end : " + playerSouls.souls + ".");
        gameIsFinished = true;
        GeneralManager.UpdateGameState(true);

        if (win)
        {
            headerText.text = "You Win";
            winText.text = "You beat Billy!";
        } else
        {
            headerText.text = "You Lose";
            winText.text = "You got stomped by Billy!";
        }

        soulsText.text = "Souls: " + playerSouls.souls;

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
