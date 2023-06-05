using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationAnim : MonoBehaviour
{
    // Start is called before the first frame update
    public Animation NotificationAnimation;
    void Start()
    {
        NotificationAnimation = gameObject.GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
