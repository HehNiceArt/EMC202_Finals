using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //public static CameraController Instance { get; private set; }
    //public Transform player;
    //public Vector3 offset;
    //private Vector3 velocity = Vector3.zero;
    //[Range(0f, 1f)]
    //public float smoothSpeed;

    public Camera cameraTest;

    private void Start()
    {
        //Instance = this;
    }
    void Update()
    {
        float rotationY = transform.eulerAngles.y;
        float rotationX = transform.eulerAngles.x;


        rotationY = Mathf.Clamp(rotationY, -70, -100);
       
        rotationX = Mathf.Clamp(rotationX, 80, 100);
        Debug.Log("X: " + rotationX + "Y: " + rotationY);
        cameraTest.transform.eulerAngles = new Vector3(rotationX, rotationY, 0);
    }


}
