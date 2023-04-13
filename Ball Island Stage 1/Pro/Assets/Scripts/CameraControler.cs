using UnityEngine;

public class CameraControler : MonoBehaviour
{
    public GameObject playerPrefab;
    private GameObject playerInstance;
    public float smoothTime = 0.3f;
    private Vector3 velocity = Vector3.zero;

    void Start()
    {
        playerInstance = GameObject.FindWithTag("Player");
    }

    void FixedUpdate()
    {
        if (playerInstance != null)
        {
            Vector3 targetPosition = playerInstance.transform.TransformPoint(new Vector3(0, 0, -10));
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }
    }
}