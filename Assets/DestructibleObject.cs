using System.Collections;
using System.Collections.Generic;

public class DestructibleObject : MonoBehaviour
{
    public GameObject destroyedVersion;

    private void OnTriggerEnter(Collider col) {
        Instantiate(destroyedVersion, transform.position, transform.rotation);
        Destroy(gameObject);       
    }
}
