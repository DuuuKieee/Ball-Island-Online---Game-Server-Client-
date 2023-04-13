using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTile : MonoBehaviour
{
    public int direct; //0: len, 1: phai, 2: xuong, 3:trai
    public float pushForce;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //print("In arrow tile");
        if (direct == 0) collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector3(0, pushForce, 0));
        if (direct == 1) collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector3(pushForce, 0, 0));
        if (direct == 2) collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector3(0, -pushForce, 0));
        if (direct == 3) collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector3(-pushForce, 0, 0));
    }
}
