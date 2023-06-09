using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterImage : MonoBehaviour
{
    GameObject targetObj;
    // Start is called before the first frame update
    void Start()
    {
        targetObj = GameObject.FindGameObjectWithTag("PlayerAnim");
        gameObject.GetComponent<SpriteRenderer>().sprite = targetObj.GetComponent<SpriteRenderer>().sprite;
        Destroy(gameObject, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
