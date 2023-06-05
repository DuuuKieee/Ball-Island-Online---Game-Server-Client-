using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontRotate : MonoBehaviour
{
    Quaternion rotation;
    void Awake()
    {
        rotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = rotation;
    }
}
