using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private CustomInput playerControl;
    public Vector2 movementInput = Vector2.zero;
    public float verticalInput;
    public float horizontalInput;
    public float moveSpeed;
    //Transform cameraObject;
   
    public Transform cameraObject;
   // private Rigidbody rb;
    Vector3 moveDirection;
    CameraController playerManager;
    private void Awake()
    {
       // cameraObject = Camera.main.transform;
    }

    private void Update()
    {
        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;
        MovePlayer();
        //RotationPlayer();
    }
    public void OnMove(InputAction.CallbackContext value)
    {
        movementInput = value.ReadValue<Vector2>();
    }
    //private void OnMovementCancelled(InputAction.CallbackContext value)
    //{
    //    movementInput = Vector2.zero;
    //}
    public void MovePlayer()
    {
        #region
        //camera direction
        //Vector3 camForward = cameraObject.forward;
        //Vector3 camRight = cameraObject.right;
        //camForward.y = 0;
        //camRight.y = 0;

        //Vector3 forwardRelative = verticalInput * camForward;
        //Vector3 rightRelative = horizontalInput * camRight;

        //Vector3 movementDirection = forwardRelative + rightRelative;
        #endregion
        //moves the player
        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput);
        //rotates the player so it moves where the camera is looking
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(MovementDirection()), 1f);
        transform.Translate(MovementDirection() * moveSpeed * Time.deltaTime, Space.World);

        
    }

    public Vector3 MovementDirection()
    {
        Vector3 camForward = cameraObject.forward;
        Vector3 camRight = cameraObject.right;
        camForward.y = 0;
        camRight.y = 0;

        Vector3 forwardRelative = verticalInput * camForward * 2f;
        Vector3 rightRelative = horizontalInput * camRight;
        Vector3 movementDirection = forwardRelative + rightRelative;

        return movementDirection;
    }


    //public void RotationPlayer()
    //{
    //    Vector3 targetRotation = Vector3.zero;
    //    targetRotation = cameraObject.forward * verticalInput;
    //    targetRotation = targetRotation + cameraObject.right * horizontalInput;
    //    targetRotation.Normalize();
    //    targetRotation.y = 0f;
    //    if(targetRotation == Vector3.zero)
    //    {
    //        targetRotation = transform.forward;
    //    }
    //    Quaternion targetRot = Quaternion.LookRotation(targetRotation);
    //    Quaternion playerRot = Quaternion.Slerp(transform.rotation, targetRot, moveSpeed);
    //    transform.rotation = playerRot;
    //}
}
