using UnityEngine;

// class to keep track of general game states like whether the game is paused
public class GeneralManager : MonoBehaviour
{
    public static bool gamePausedOrDone;
    public static bool lockedMouse;

    // Start is called before the first frame update
    void Awake()
    {
        gamePausedOrDone = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        lockedMouse = false;
    }

    // Update is called once per frame
    void Update()
    {
        Cursor.lockState = lockedMouse ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = lockedMouse;
    }

    // if game is paused or done
    public static void UpdateGameState(bool state)
    {
        if (state)
        {
            lockedMouse = true;
        } else
        {
            lockedMouse = false;
        }
        gamePausedOrDone = state;
    }
}
