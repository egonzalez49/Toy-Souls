using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : MonoBehaviour
{

    public float speed;
    public bool delayedDestruction;
    private float destroyTimer = 0f;

    // Update is called once per frame
    void Update()
    {
        destroyTimer += Time.deltaTime;
        if (delayedDestruction && destroyTimer >= 10.0f)
        {
            Destroy(gameObject);
        }
        if (speed != 0)
        {
            transform.position += transform.forward * (speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // do damage
        Vector3 hitPoint = other.transform.position;
        if (other.gameObject.tag == "Player")
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(15, hitPoint);
            Destroy(gameObject);
        }
        else if (other.gameObject.tag != "Ted" && other.gameObject.tag != "Boss")
        {
            Destroy(gameObject);
        }
    }
}
