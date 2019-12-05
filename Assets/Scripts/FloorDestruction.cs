using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorDestruction : MonoBehaviour
{
    public bool flashing = false;
    public bool destroy = false;
    private float sinkSpeed = 5.0f;
    private float timer = 0.0f;
    private float delta = 0.0f;
    public Color flashColor = new Color(1f, 0f, 0f, 0.1f);
    private float flashTimer = 0.0f;
    private float maxFlashingTime = 5.0f;
    private Color startColor;
    //public float flashSpeed = 5f;

    private void Awake()
    {
        startColor = GetComponent<MeshRenderer>().material.color;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        delta = Time.deltaTime;
        if (flashing)
        {
            flashTimer += delta;
            if (flashTimer < maxFlashingTime)
            {
                if ((int)(flashTimer * 5) % 2 == 0)
                {
                    GetComponent<MeshRenderer>().material.color = startColor;
                }
                else
                {
                    GetComponent<MeshRenderer>().material.color = flashColor;
                }
            }
            else
            {
                flashing = false;
                destroy = true;
            }
        }

        if (destroy)
        {
            // ... move the floor down by the sinkSpeed per second.
            transform.Translate(-Vector3.up * sinkSpeed * delta);
            timer += delta;
            if (timer > 1.0f)
            {
                GetComponent<BoxCollider>().enabled = false;
                sinkSpeed += delta * 10;
            }
            if (timer > 4.0f)
            {
                GetComponent<MeshRenderer>().enabled = false;
                Destroy(this);
            }
        }
    }
}
