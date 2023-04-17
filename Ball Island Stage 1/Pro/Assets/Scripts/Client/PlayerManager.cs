using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerManager : MonoBehaviour
{
    public int id;
    public string username;
    [SerializeField] GameObject USER;
    void Start()
    {
    USER.GetComponent<TextMeshPro>().SetText(username); 
    }
}
