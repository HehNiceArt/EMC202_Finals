using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform player;
    public Vector3 offset;
    private Vector3 velocity = Vector3.zero;
    [Range(0f, 1f)]
    public float smoothSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
       
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            Vector3 desiredPosition = player.position + offset;

            transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);
        }
        
    }
}
