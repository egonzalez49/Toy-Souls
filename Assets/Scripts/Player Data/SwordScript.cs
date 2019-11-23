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
        SetSword(swordIndex);
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

    public void SetSword(int i)
    {
        swordIndex = i;
        //meshFilter.mesh = swordMesh[swordIndex];
        //meshCollider.sharedMesh = swordMesh[swordIndex];
    }
}
