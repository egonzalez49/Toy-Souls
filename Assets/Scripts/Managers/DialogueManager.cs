using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public float typingSpeed = 0.02f;
    public string[] sentences;

    //private float timer;
    //private bool activeTimer;
    private bool showMessage;
    private Text message;
    //private Animator anim;
    private CanvasGroup canvasGroup;
    private IEnumerator cRoutine = null;

    void Awake()
    {
        message = this.gameObject.transform.GetChild(0).gameObject.GetComponent<Text>();
        //anim = GetComponent<Animator>();
        canvasGroup = GetComponent<CanvasGroup>();
        //activeTimer = false;
        showMessage = false;
    }

    // Update is called once per frame
    void Update()
    {
        //if (activeTimer)
        //{
        //    timer -= Time.deltaTime;
        //    if (timer <= 0)
        //    {
        //        showMessage = false;
        //        timer = 0;
        //        activeTimer = false;
        //    }
        //}

        if (showMessage)
        {
            canvasGroup.alpha = 1f;
        }
        else
        {
            if (cRoutine != null)
            {
                StopCoroutine(cRoutine);
            }
            canvasGroup.alpha = 0f;
        }
    }

    public void generatePopupMessage(string[] x, int index)
    {
        showMessage = true;
        sentences = x;
        cRoutine = Type(index);
        StartCoroutine(cRoutine);
        //anim.SetTrigger("Show");
    }

    //public void generateTimedPopupMessage(string text, float time)
    //{
    //    activeTimer = true;
    //    showMessage = true;
    //    timer = time;
    //}

    IEnumerator Type(int index)
    {
        foreach (char letter in sentences[index].ToCharArray())
        {
            message.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void hideMessage()
    {
        //anim.SetTrigger("Hide");
        showMessage = false;
        StopCoroutine(cRoutine);
        message.text = "";
        //activeTimer = false;
        //timer = 0;
    }
}
