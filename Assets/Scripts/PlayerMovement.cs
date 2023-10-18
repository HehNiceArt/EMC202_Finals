using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private CustomInput playerControl;
    [Header("Player States")]
    public Vector2 movementInput = Vector2.zero;
    public float verticalInput;
    public float horizontalInput;

    [Header("Player Speed")]
    public float moveSpeed;
    [Range(0f, 1f)]
    public float rotationSpeed;
    

    [Header("Camera")]
    public Transform cameraObject;

    private Rigidbody rb;
    private Vector3 moveDelta;
    Vector3 moveDirection;
    CameraController playerManager;
    private void Awake()
    {
       // cameraObject = Camera.main.transform;
        rb = GetComponentInChildren<Rigidbody>();
    }

    private void FixedUpdate()
    {
        //RotateIfNewDirection(moveDelta);
        //rb.MovePosition(rb.position + moveDelta);
        HandleAllMovement();
    }
    private void Update()
    {
        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;
       

      //  moveDelta = new Vector3(horizontalInput, 0.0f, verticalInput) * moveSpeed * Time.deltaTime;
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
        movement.y = 0;

       // movement = movement.normalized * moveSpeed;
        //rotates the player so it moves where the camera is looking
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(TargetDirection()), 1f);
        transform.Translate(MovementDirection(), Space.World);
       
        
    }
    public void HandleAllMovement()
    {
        MovePlayer();
        HandleRotation();
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
        movementDirection = movementDirection.normalized * moveSpeed * Time.deltaTime;
        return movementDirection;
    }

   
    
    private Vector3 TargetDirection()
    {
       // Debug.Log(cameraObject.forward + ", " + cameraObject.right);
        Vector3 targetRotation;
        targetRotation = cameraObject.forward * verticalInput;
        targetRotation = targetRotation + cameraObject.right * horizontalInput;
        targetRotation.Normalize();
        targetRotation.y = 0f;
        if (targetRotation == Vector3.zero) {  targetRotation = transform.forward; }
       // Debug.Log(targetRotation.ToString()); 
        return targetRotation;
       
    }
    private void HandleRotation()
    {
        Quaternion targetRotation = Quaternion.LookRotation(TargetDirection());
        Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed);
        transform.rotation = playerRotation;
    }
}
