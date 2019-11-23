using UnityEngine;

public class SwordScript : MonoBehaviour
{
    public Mesh[] swordMesh;
    public int swordIndex;

    private int prevIndex;
    private MeshFilter meshFilter;
    private MeshCollider meshCollider;

    void Awake()
    {
        meshFilter = GetComponent<MeshFilter>();
        meshCollider = GetComponent<MeshCollider>();
        //SetSword(swordIndex);
        prevIndex = 0;
    }

    private void Start()
    {
        //prevIndex = swordIndex;
        //SetSword(swordIndex);
    }

    // Update is called once per frame
    void Update()
    {
        if (prevIndex != swordIndex)
        {
            SetSword(swordIndex);
        }
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
        prevIndex = swordIndex;
        meshFilter.mesh = swordMesh[swordIndex];
        meshCollider.sharedMesh = swordMesh[swordIndex];
    }
}
