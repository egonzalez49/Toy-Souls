using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Collectible
{
    Bucket
}

public class PlayerCollectibles : MonoBehaviour
{
    private List<Collectible> items;
    
    void Awake()
    {
        items = new List<Collectible>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void collectItem(Collectible c)
    {
        items.Add(c);
    }

    public bool hasItem(Collectible c)
    {
        return items.Contains(c);
    }
}
