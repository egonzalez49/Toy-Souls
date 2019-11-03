using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralManager : MonoBehaviour
{
    public static bool gamePausedOrDone;

    // Start is called before the first frame update
    void Awake()
    {
        gamePausedOrDone = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // if game is paused or done
    public static void UpdateGameState(bool state)
    {
        gamePausedOrDone = state;
    }
}
