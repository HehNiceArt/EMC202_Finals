using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Lumin;
using UnityEngine.Rendering.Universal;

public class playercomponent : MonoBehaviour
{
    [Header("Terrain")]
    [SerializeField] private float groundDistance;

    [SerializeField] private LayerMask terrainLayer;

    [Header("Slope")]
    [SerializeField] private float maxSlopeHeight;

    private RaycastHit slopeHit;
    [SerializeField] private float maxSlopeAngle;
    private bool exitingSlope;

    [Header("Player")]
    [SerializeField] private float walkSpeed;

    [SerializeField] private float runSpeed;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private CharacterController controller;
    [SerializeField] private CustomInput playerController;
    [SerializeField] private float playerHeight;

    [Header("Dash")]
    [SerializeField] float dashTime;
    [SerializeField] float dashSpeed;
    [SerializeField] float dashCooldown;
    bool canDash = true;
    bool isDashing;

    #region Jump Header
    //[Header("Jump")]
    //[SerializeField] private float jumpForce;
    //[SerializeField] private float jumpCoolDown;
    //[SerializeField] private float airMultiplier;
    //private bool readyToJump;
    #endregion

    private Vector3 moveDir;
    // [SerializeField] float footstepOffset;

    [Header("Camera")]
    [SerializeField] private Transform cameraObject;

    //[SerializeField] Camera cam;
    //[SerializeField] private Vector2 movementInput = Vector2.zero;
    //[SerializeField] float verticalInput;
    //[SerializeField] float horizontalInput;

    //[SerializeField] float rotationSpeed;

    private float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;

    // Start is called before the first frame update
    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void Update()
    {
        if(isDashing) { return; }
        ShiftRun();
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

        #endregion Flip CharacterSprite depending on Rotation
    }
    private void FixedUpdate()
    {
        if(isDashing) { return; }
        RayCast();
    }

    private void RayCast()
    {
        #region Old Code

        RaycastHit hit;
        Vector3 castPos = transform.position;
        castPos.y += 1;

        if (Physics.Raycast(castPos, -transform.up, out hit, Mathf.Infinity, terrainLayer))
        {
            if (hit.collider != null)
            {
                // transform.rotation =  Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
                Vector3 movePos = transform.position;
                movePos.y = hit.point.y + groundDistance;
                transform.position = movePos;
            }
        }

        #endregion Old Code

        #region Deprecated

        //Ray ray = new Ray(transform.position, -transform.up);
        //RaycastHit hit;
        //// if (Physics.Raycast(transform.position + Vector3.up, -Vector3.up, out RaycastHit, 1f + rayLength))
        //if (Physics.Raycast(ray, out hit))
        //{
        //     //rotates the player in respect to the angle of the slope
        //    transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
        //    Vector3 targetDir = (transform.position - hit.point).normalized;
        //    transform.position = hit.point + targetDir * 1f;
        //}

        #endregion Deprecated
    }

    private bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.3f))
        {
            //transform.rotation = Quaternion.FromToRotation(transform.up, slopeHit.normal) * transform.rotation;
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }
        return false;
    }

    private Vector3 GetSlopeMoveDirection()
    {
        return Vector3.ProjectOnPlane(moveDir, slopeHit.normal).normalized;
    }

    private void PlayerMovement()
    {
        if (OnSlope() && !exitingSlope)
        {
            if(rb.velocity.magnitude > walkSpeed) 
            rb.AddForce(GetSlopeMoveDirection() * walkSpeed * Time.deltaTime, ForceMode.Force);
        }
        else
        {
            Vector3 flatVelocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            if(flatVelocity.magnitude > walkSpeed)
            {
                Vector3 limitedVel = flatVelocity.normalized * walkSpeed;
                rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
            }
        }
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        //it should be the 0 that's doing the issue
        moveDir = new Vector3(x, 0, y).normalized;

        #region Jump
        //if (Input.GetKeyDown(KeyCode.Space) && readyToJump)
        //{
        //    readyToJump = false;
        //    Debug.Log("test");
        //    Jump();
        //    Invoke(nameof(ResetJump), jumpCoolDown);
        //}
        #endregion

        if (moveDir.magnitude >= 0.1f)
        {
            MoveDirection();
        }
        if (Input.GetKeyDown(KeyCode.Space) && canDash)
        {
            StartCoroutine(Dash());
        }

    }

    Vector3 moveDirection;
    private CharacterController MoveDirection()
    {
        float targetAngle = Mathf.Atan2(moveDir.x, moveDir.z) * Mathf.Rad2Deg + cameraObject.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);
        moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        controller.Move(moveDirection.normalized * walkSpeed * Time.deltaTime);
        return controller;
    }
    private float ShiftRun()
    {
        float defaultSpeed = 150f;
        if (Input.GetKeyDown(KeyCode.LeftShift)) { walkSpeed = runSpeed; }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        walkSpeed = defaultSpeed;
        return walkSpeed;
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float startTime = Time.time;
        while (Time.time < startTime + dashTime)
        {
            controller.Move(moveDirection * dashSpeed * Time.deltaTime); 
            yield return null;
        }
        isDashing = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
    #region Jump Code
    //private void Jump()
    //{
    //    exitingSlope = true;
    //    rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
    //    rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    //}

    //private void ResetJump()
    //{
    //    readyToJump = true;
    //    exitingSlope = false;
    //}
    #endregion

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

    #endregion Old Movement System
}