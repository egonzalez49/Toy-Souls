using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoulScript : MonoBehaviour
{
    public static int souls;

    private Text text;

    void Awake()
    {
        text = GetComponent<Text>();
        souls = 0;
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "Souls: " + souls;
    }

    public void AddSouls(int s)
    {
        souls += s;
    }

    public int GetSouls()
    {
        return souls;
    }
}
