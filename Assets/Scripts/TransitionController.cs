using UnityEngine;

public class TransitionController : MonoBehaviour
{
    public bool fadeInOnAwake;
    public bool dontPlayOnAwake;

    private Animator animator;

    void Awake()
    {
        animator = GameObject.FindGameObjectWithTag("Fade").GetComponent<Animator>();

        if (dontPlayOnAwake) return;

        if (fadeInOnAwake)
        {
            fadeIn();
        } else
        {
            fadeOut();
        }
    }

    public void fadeIn()
    {
        animator.SetTrigger("FadeIn");
    }

    public void fadeOut()
    {
        animator.SetTrigger("FadeOut");
    }
}
