using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleObject : MonoBehaviour
{
    public GameObject destroyedVersion;
    public AudioSource crateAudio;

    private void Awake()
    {
        crateAudio = GetComponentInParent<AudioSource>();
    }

    private void OnTriggerEnter(Collider col) {
        if(crateAudio != null)
        {
            crateAudio.Play();
        }
        Instantiate(destroyedVersion, transform.position, transform.rotation);
        Destroy(gameObject);       
    }
}
