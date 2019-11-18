using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorDestruction : MonoBehaviour
{
    public bool destroy = false;
    private float sinkSpeed = 5.0f;
    private float timer = 0.0f;

    // Update is called once per frame
    void Update()
    {
        if (destroy)
        {
            // ... move the enemy down by the sinkSpeed per second.
            transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
            timer += Time.deltaTime;
            if (timer > 1.0f)
            {
                GetComponent<BoxCollider>().enabled = false;
                sinkSpeed += Time.deltaTime * 10;
            }
            if (timer > 4.0f)
            {
                GetComponent<MeshRenderer>().enabled = false;
                Destroy(this);
            }
        }
    }
}
