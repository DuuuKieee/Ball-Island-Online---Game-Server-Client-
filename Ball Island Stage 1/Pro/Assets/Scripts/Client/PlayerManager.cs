using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerManager : MonoBehaviour
{
    public int id;
    public string username;
    [SerializeField] public TextMeshPro nameHeader;
    void Start()
    {
        nameHeader.text = username;
    }
    public void Initialize(int _id, string _username)
    {
        id = _id;
        username = _username;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "bush"||collision.gameObject.tag == "coconut")
        {
            nameHeader.text = "";
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.tag =="bush"||other.gameObject.tag == "coconut")
        { nameHeader.text = username;}
    }

    
}
