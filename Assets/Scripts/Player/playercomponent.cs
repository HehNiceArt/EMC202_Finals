using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class playercomponent : MonoBehaviour
{
 
    [Header("Terrain")]
    [SerializeField] float groundDistance;
    [SerializeField] LayerMask terrainLayer;
    [Header("Player")]
    [SerializeField] float speed;
    [SerializeField] Rigidbody rb;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] CharacterController controller;
    [SerializeField] CustomInput playerController;
    [SerializeField] float footstepOffset;
    [Header("Camera")]
    [SerializeField] Transform cameraObject;
    //[SerializeField] private Vector2 movementInput = Vector2.zero;
    //[SerializeField] float verticalInput;
    //[SerializeField] float horizontalInput;

    //[SerializeField] float rotationSpeed;

    float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        #region RayCast
        //RaycastHit hit;
        //Vector3 castPos = transform.position;
        //castPos.y += 1;

        //if(Physics.Raycast(castPos, -transform.up, out hit, Mathf.Infinity, terrainLayer))
        //{
        //    if( hit.collider != null )
        //    {
        //        Vector3 movePos = transform.position;
        //        movePos.y = hit.point.y + groundDistance;
        //        transform.position = movePos;
        //        Debug.Log("hit.collider != null");
        //    }
        //    Debug.Log("is hit");
        //}
        #endregion
        RayCast();
        PlayerMovement();
        #region Flip CharacterSprite depending on Rotation
        //if (horizontalInput != 0 && horizontalInput < 0)
        //{
        //    spriteRenderer.flipX = true;
        //}
        //else if (horizontalInput != 0 && horizontalInput > 0)
        //{
        //    spriteRenderer.flipX = false;
        //}
       #endregion
    }

    private void RayCast()
    {
        //RaycastHit hit;
        //Vector3 castPos = transform.position;
        //castPos.y += 1;

        //if (Physics.Raycast(castPos, -transform.up, out hit, Mathf.Infinity, terrainLayer))
        //{
        //    if (hit.collider != null)
        //    {
        //        Vector3 movePos = transform.position;
        //        movePos.y = hit.point.y + groundDistance;
        //        transform.position = movePos;
        //    }
        //}
        Ray ray = new Ray(transform.position, -transform.up);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit))
        {
            if(controller.isGrounded)
            transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
            Vector3 targetDir = (transform.position - hit.point).normalized;
            transform.position = hit.point + targetDir * 1f;
        }
    }

    private void PlayerMovement()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        //it should be the 0 that's doing the issue
        Vector3 moveDir = new Vector3(x, 0, y).normalized;
        Debug.DrawLine(transform.position, moveDir);
        if (moveDir.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(moveDir.x, moveDir.z) * Mathf.Rad2Deg + cameraObject.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            
            controller.Move(moveDirection.normalized * speed * Time.deltaTime);
        }
    }

    #region Old Movement System
    //private void FixedUpdate()
    //{
    //    HandleAllMovement();
    //}
    //private void HandleAllMovement()
    //{
    //    MovePlayer();
    //    HandleRotation();
    //}
    //public void OnMove(InputAction.CallbackContext value)
    //{
    // movementInput = value.ReadValue<Vector2>(); 
    //}
    //public void MovePlayer()
    //{
    //    Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput);
    //    movement.y = 0;
    //    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(TargetDirection()), 1f);
    //    transform.Translate(MovementDirection(), Space.World);
    //    Debug.Log(movement);
    //}
    //private Vector3 MovementDirection()
    //{
    //    Vector3 camForward = cameraObject.forward;
    //    Vector3 camRight = cameraObject.right;
    //    camForward.y = 0;
    //    camRight.y = 0;

    //    Vector3 forwardRelative = verticalInput * camForward;
    //    Vector3 rightRelative = horizontalInput * camRight;
    //    Vector3 movementDirection = forwardRelative + rightRelative;
    //    movementDirection = movementDirection.normalized * speed * Time.deltaTime;
    //    return movementDirection;
    //}
    //private Vector3 TargetDirection()
    //{
    //    Vector3 targetDirection;
    //    targetDirection = cameraObject.forward * verticalInput;
    //    targetDirection = targetDirection + cameraObject.right * horizontalInput;
    //    targetDirection.Normalize();
    //    targetDirection.y = 0f;
    //    if (targetDirection == Vector3.zero) { targetDirection = transform.forward; }
    //    return targetDirection;
    //}
    //private void HandleRotation()
    //{
    //    Quaternion targetRotation = Quaternion.LookRotation(TargetDirection());
    //    Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed);
    //    transform.rotation = playerRotation;
    //}
    #endregion
}
