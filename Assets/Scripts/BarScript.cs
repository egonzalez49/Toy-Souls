using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarScript : MonoBehaviour
{
    public Image content; //health bar
    public int minHealth = 0;
    public int maxHealth = 100;

    private float fillAmount;
    private float lerpSpeed = 2f;

    // Update is called once per frame
    void Update()
    {
        HandleBar();
    }

    public void HandleBar()
    {
        content.fillAmount = Mathf.Lerp(content.fillAmount, Map(fillAmount, minHealth, maxHealth, 0, 1), Time.deltaTime * lerpSpeed);
    }

    public void SetFillAmount(float value)
    {
        fillAmount = value;
    }

    //takes a min and max IN value (say 0 and 230 health)
    //and outputs into range between min and max OUT value
    private float Map(float value, float inMin, float inMax, float outMin, float outMax)
    {
        return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
    }
}
