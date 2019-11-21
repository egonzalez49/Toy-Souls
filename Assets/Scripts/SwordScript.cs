using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordScript : MonoBehaviour
{
    public Mesh[] swordMesh;
    public int swordIndex;

    private MeshFilter meshFilter;
    private MeshCollider meshCollider;

    void Awake()
    {
        meshFilter = GetComponent<MeshFilter>();
        meshCollider = GetComponent<MeshCollider>();
        swordIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpgradeSword()
    {
        if (swordIndex < swordMesh.Length)
        {
            swordIndex++;
            meshFilter.mesh = swordMesh[swordIndex];
            meshCollider.sharedMesh = swordMesh[swordIndex];
        }
    }
}
