using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Lumin;
using UnityEngine.Rendering.Universal;
using UnityEngine.UIElements;

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
    public float walkSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private CharacterController controller;
    [SerializeField] private CustomInput playerController;
    [SerializeField] private Animator anim;
    private float playerHeight;

    [Header("Player Stats")]
    public float playerHealth;
    public float playerDamage;

    [Header("Dash")]
    [SerializeField] float dashTime;
    [SerializeField] float dashSpeed;
    [SerializeField] float dashCooldown;
    bool canDash = true;
    bool isDashing;

    private Vector3 moveDir;

    [Header("Camera")]
    [SerializeField] private Transform cameraObject;

    private float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;
    
    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if(isDashing) { return; }
        ShiftRun();
        PlayerMovement();
        if(playerHealth <= 0)
        {
            Debug.Log("Player is dead");
        }
    }
    private void FixedUpdate()
    {
        if(isDashing) { return; }
        RayCast();
    }
    RaycastHit hit;
    private void RayCast()
    {
        Vector3 castPos = transform.position;
        castPos.y += 1;

        if (Physics.Raycast(castPos, -transform.up, out hit, Mathf.Infinity, terrainLayer))
        {
            if (hit.collider != null)
            {
                Vector3 movePos = transform.position;
                //transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal);
                movePos.y = hit.point.y + groundDistance;
                transform.position = movePos;
            }
        }
    }

    private bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.3f))
        {
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
        
        //Input movement
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        moveDir = new Vector3(x, 0, y).normalized;
        if (moveDir.magnitude >= 0.1f)
        {
            
            MoveDirection();
        }
        if(moveDir.magnitude <= 0.1f) { anim.SetBool("isWalking", false); }
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
        anim.SetBool("isWalking", true);
        return controller;
    }
    public float ShiftRun()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift)) 
        {
           
            walkSpeed = runSpeed;
            anim.SetBool("isRunning", true);
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            float defaultSpeed = 700f;
            anim.SetBool("isRunning", false);
            walkSpeed = defaultSpeed;
        }
            return walkSpeed;
        
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float startTime = Time.time;
        while (Time.time < startTime + dashTime)
        {
            controller.Move(moveDirection.normalized * dashSpeed * Time.deltaTime); 
            yield return controller;
        }
        isDashing = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
}