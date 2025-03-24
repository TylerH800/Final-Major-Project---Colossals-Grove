using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine.Windows;

public class PlayerMovement : MonoBehaviour
{
    private GameInput gameInput;
    private Vector2 moveInput;
    private bool isSprinting, isCrouching;
    private bool isGrounded;

    public float walkSpeed, sprintSpeed, crouchSpeed;

    public float jumpHeight;
    public float gravity = -9.81f;
    Vector3 vertVelocity = Vector3.zero; //used for vertical movement
    public Transform groundCheck;
    public float groundDistance;
    public LayerMask groundMask;

    public bool holdToSprint;
    public bool sprintToggleOn;

    private float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;

    private CharacterController characterController;
    public Transform cam;
    private Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        gameInput = new GameInput();       
        characterController = GetComponent<CharacterController>();      
        animator = GetComponentInChildren<Animator>();

    }

    // Update is called once per frame
    void Update()
    {      
        HorizontalMovement();
        VerticalMovement();
        Animation();

    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        if (isGrounded)
        {
            vertVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    void OnSprint()
    {
       
    }

    public void HorizontalMovement()
    {     
        Vector3 direction = new Vector3(moveInput.x, 0f, moveInput.y).normalized;

        //changes targetspeed based on whether sprinting or crouching
        float targetSpeed = isSprinting ? sprintSpeed : (isCrouching) ? crouchSpeed : walkSpeed;

        // If there's horizontal input
        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            // Calculate the movement direction
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            characterController.Move(moveDir.normalized * targetSpeed * Time.deltaTime);

        }
        
    }    

    

    void VerticalMovement()
    {
        // Ground check
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);        

        //gravity
        vertVelocity.y += gravity * Time.deltaTime;
        characterController.Move(vertVelocity * Time.deltaTime);

        // Conditions for landing
        if (isGrounded && vertVelocity.y <= 0)
        {
            vertVelocity.y = -2f;
        }
    }  
    
    void Animation()
    {
        print(moveInput.magnitude);
        //jump bool
        if (!isGrounded)
        {
            animator.SetBool("Jump", true);
        }
        else
        {
            animator.SetBool("Jump", false);
        }

        //walk bool
        if (moveInput.magnitude > 0)
        {
            
            animator.SetBool("Walk", true);
        }
        else
        {
            animator.SetBool("Walk", false);
        }

        //run bool
        if (isSprinting)
        {
            animator.SetBool("Run", true);
        }
        else
        {
            animator.SetBool("Run", false);
        }

        //crouch bool
        if (isCrouching)
        {
            animator.SetBool("Crouch", true);
        }
        else
        {
            animator.SetBool("Crouch", false);
        }
    }
}
