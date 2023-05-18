using UnityEngine;
using System.Collections;

public class CameraControler : MonoBehaviour
{
    // camera will follow this object
     private Vector3 offset = new Vector3(0f, 0f, -10f);
    private float smoothTime = 0.25f;
    private Vector3 velocity = Vector3.zero;

    public Transform Target;

    private void Update()
    {
        Target = GameObject.FindGameObjectWithTag("Player").transform;
        Vector3 targetPosition = Target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}


