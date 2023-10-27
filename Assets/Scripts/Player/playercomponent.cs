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

        [Header("Slope")]
        [SerializeField] float maxSlopeHeight;
        private RaycastHit slopeHit;
        [SerializeField] float maxSlopeAngle;
        bool exitingSlope;

        [Header("Player")]
        [SerializeField] float walkSpeed;
    [SerializeField] float runSpeed;
        [SerializeField] Rigidbody rb;
        [SerializeField] SpriteRenderer spriteRenderer;
        [SerializeField] CharacterController controller;
        [SerializeField] CustomInput playerController;
        [SerializeField] float playerHeight;
    Vector3 moveDir;
       // [SerializeField] float footstepOffset;

        [Header("Camera")]
        [SerializeField] Transform cameraObject;
        //[SerializeField] Camera cam;
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
            #endregion
        }
    //private void LateUpdate()
    //{
    //    var rot = cam.transform.rotation;
    //    transform.LookAt(transform.position + rot * Vector3.forward, rot * Vector3.up);
    //}
    private void FixedUpdate()
    {
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
        #endregion
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
        #endregion
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
            rb.AddForce(GetSlopeMoveDirection() * walkSpeed * Time.deltaTime, ForceMode.Force);
        }
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        //it should be the 0 that's doing the issue
        moveDir = new Vector3(x, 0, y).normalized;
        
        if (moveDir.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(moveDir.x, moveDir.z) * Mathf.Rad2Deg + cameraObject.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
           
            controller.Move(moveDirection.normalized * walkSpeed * Time.deltaTime);
        }
    }
    private float ShiftRun()
    {
        float defaultSpeed = 150f;
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            walkSpeed = runSpeed;
        }
       
        if(Input.GetKeyUp(KeyCode.LeftShift))
        walkSpeed = defaultSpeed;
        return walkSpeed;
        
        
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

