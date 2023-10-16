using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance { get; private set; }
    public Transform player;
    public Vector3 offset;
    private Vector3 velocity = Vector3.zero;
    [Range(0f, 1f)]
    public float smoothSpeed;

    public Camera cameraTest;

    private void Start()
    {
        Instance = this;
    }
    void Update()
    {
        if (player != null)
        {
            Vector3 desiredPosition = player.position + offset;

            transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);
        }
        
    }


}
