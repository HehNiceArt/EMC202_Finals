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
    Transform cameraObject;

   // private Rigidbody rb;
    Vector3 moveDirection;

    private void Awake()
    {
        cameraObject = Camera.main.transform;
    }
    //private void Awake()
    //{
    //    rb = GetComponentInChildren<Rigidbody>();
    //    playerControl = new CustomInput();
    //}
    //private void OnEnable()
    //{
    //    playerControl.Enable();
    //    playerControl.PlayerMovement.Movement.performed += OnMovementPerformed;
    //    playerControl.PlayerMovement.Movement.canceled += OnMovementCancelled;
    //}
    //private void OnDisable()
    //{
    //    playerControl.Disable();
    //    playerControl.PlayerMovement.Movement.performed -= OnMovementPerformed;
    //    playerControl.PlayerMovement.Movement.canceled -= OnMovementCancelled;
    //}

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
        //moves the player
        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput);
        //rotates the player so it moves where the camera is looking
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 1f);
        transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World);
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("aaa");
        cameraObject.transform.Rotate(0, 0, 180);
        GameObject.FindGameObjectWithTag("MainCamera");
    }
    public void OnCollisionEnter(Collision other)
    {
        Debug.Log("aaa");
        cameraObject.transform.Rotate(0, 0, 180);
        GameObject.FindGameObjectWithTag("MainCamera");
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
