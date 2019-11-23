using UnityEngine;
using UnityEngine.UI;

public class PlayerSouls : MonoBehaviour
{
    public int souls;
    public Text text;

    void Awake()
    {
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

        //Logger.WriteToFile("Souls earned: " + souls + ".");
    }
}
