using UnityEngine;

public class ResetPlayer : MonoBehaviour
{
    //Avoid having leftover data from previous game session.
    void Awake()
    {
        GlobalControl.ResetPlayer();
    }
}
