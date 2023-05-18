using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public int spawnerId;
    public bool hasItem;
    public SpriteRenderer itemModel;
    public Animator anim;
    public GameObject par, lightOb;
    private Vector3 basePosition; 

    // Start is called before the first frame update
    public void Initialize(int _spawnerId, bool _hasItem)
    {
        spawnerId = _spawnerId;
        hasItem = _hasItem;
        itemModel.enabled = _hasItem;

        basePosition = transform.position;
        
    }
     public void ItemSpawned()
    {
        hasItem = true;
        itemModel.enabled = true;
        anim.enabled = true;
        par.SetActive(true);
        lightOb.SetActive(true);
    }
    public void ItemPickedUp()
    {
        hasItem = false;
        itemModel.enabled = false;
        anim.enabled = false;
        par.SetActive(false);
        lightOb.SetActive(false);
    }
}
