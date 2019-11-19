using UnityEngine;
using UnityEngine.SceneManagement;
using PC;

public class IntroManager : MonoBehaviour
{
    private float timerToLoadGameScene;

    void Awake()
    {
        timerToLoadGameScene = 30f;
    }

    // Update is called once per frame
    void Update()
    {
        timerToLoadGameScene -= Time.deltaTime;
        // TODO: Add controller input.
        // Skip the intro and load the game scene.
        if (Input.GetKeyUp("space") || Input.GetButton(StaticStrings.AButton) || timerToLoadGameScene <= 0)
        {
            SceneManager.LoadScene("BetaScene");
        }
    }
}
