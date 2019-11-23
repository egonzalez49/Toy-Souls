using UnityEngine;
using UnityEngine.UI;

public class PopupManager : MonoBehaviour
{
    private float timer;
    private bool activeTimer;
    private bool showMessage;
    private Text message;
    private Animator anim;
    private CanvasGroup canvasGroup;

    void Awake()
    {
        message = gameObject.transform.GetChild(0).gameObject.GetComponent<Text>();
        anim = GetComponent<Animator>();
        canvasGroup = GetComponent<CanvasGroup>();
        activeTimer = false;
        showMessage = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (activeTimer)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                showMessage = false;
                timer = 0;
                activeTimer = false;
            }
        }

        if (showMessage)
        {
            canvasGroup.alpha = 1f;
        } else
        {
            canvasGroup.alpha = 0f;
        }
    }

    public void generatePopupMessage(string text)
    {
        message.text = text;
        showMessage = true;
        anim.SetTrigger("Show");
    }

    public void generateTimedPopupMessage(string text, float time)
    {
        //transform.localScale = new Vector3(0, 0, 0);
        message.text = text;
        activeTimer = true;
        showMessage = true;
        //anim.SetTrigger("Show");
        timer = time;
    }

    public void hideMessage()
    {
        //anim.SetTrigger("Hide");
        //transform.localScale = new Vector3(0, 0, 0);
        showMessage = false;
        activeTimer = false;
        timer = 0;
    }
}
