using UnityEngine;

public class GateSwinging : MonoBehaviour
{
    public bool playSound = true;

    private float upperBound = 0.052f;
    private AudioSource audioSource;
    private Rigidbody leftGate;
    private Rigidbody rightGate;
    private bool audioPlaying = false;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        leftGate = this.transform.GetChild(0).gameObject.GetComponent<Rigidbody>();
        rightGate = this.transform.GetChild(1).gameObject.GetComponent<Rigidbody>();
        audioPlaying = false;
    }

    void FixedUpdate()
    {
        if (!playSound) return;

        float leftMagnitude = leftGate.angularVelocity.magnitude;
        float rightMagnitude = rightGate.angularVelocity.magnitude;

        //bool gateMoving = leftMagnitude != 0 || rightMagnitude != 0;

        bool gateMoving = leftMagnitude > upperBound || rightMagnitude > upperBound;

        if (!gateMoving)
        {
            audioPlaying = false;
            audioSource.loop = false;
            //audioSource.Stop();
        }

        if (!audioPlaying && gateMoving)
        {
            audioPlaying = true;
            audioSource.loop = true;
            audioSource.Play();
        }
    }
}
