using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionBehavior : MonoBehaviour
{
    public static CollisionBehavior Instance { get; private set; }
    CameraController CameraController;
    PlayerMovement PlayerMovement;

    private void Start()
    {
        Instance = this;
    }
    private void OnCollisionEnter(Collision collision)
    {
        
       
        if (collision.gameObject.CompareTag("HiddenObject"))
        {
            //PlayerMovement.RightRotate();
            
            
        }
    }
}
