using UnityEngine;

public class ObjectSoundScript : MonoBehaviour
{
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude >= 4.2 && !audioSource.isPlaying)
            audioSource.Play();
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.relativeVelocity.magnitude >= 3 && !audioSource.isPlaying)
            audioSource.Play();
    }
}
