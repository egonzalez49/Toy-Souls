using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenCrateManager : MonoBehaviour
{

    public float max_time;
    public float start_sink;
    public float life;

    // Start is called before the first frame update
    void Start()
    {
        life = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        life += Time.deltaTime;
        if (life > start_sink)
        {
            Vector3 pos = transform.position;
            pos.y -= 0.05f;
            transform.position = pos;
        }
        if (life > max_time)
        {
            Destroy(gameObject);
        }
    }
}
