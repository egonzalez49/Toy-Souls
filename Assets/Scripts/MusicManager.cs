using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public float maxVolume = 0.15f;
    public float speed = 0.01f;

    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0f;
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        float audioVolume = audioSource.volume;
        if (audioVolume < maxVolume)
        {
            audioVolume += speed * Time.deltaTime;
            audioSource.volume = audioVolume;
        }
    }
}
