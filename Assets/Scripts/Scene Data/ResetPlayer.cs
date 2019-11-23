using UnityEngine;

public class ResetPlayer : MonoBehaviour
{
    private SceneData InfoSaver;

    //Avoid having leftover data from previous game session.
    void Awake()
    {
        InfoSaver = GameObject.FindGameObjectWithTag("InfoSaver").GetComponent<SceneData>();
        Debug.Log("Ran reset player/boss");
        GlobalControl.ResetPlayer();
        InfoSaver.ResetBoss();
    }
}
