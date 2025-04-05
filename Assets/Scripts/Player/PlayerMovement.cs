using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine.Windows;
using System.Xml;
using Unity.Cinemachine;
using System;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Values")]
    public float walkSpeed;
    public float sprintSpeed, crouchSpeed;
    public float jumpHeight;
    public float gravity = -9.81f;

    private bool isSprinting, isCrouching;
    private bool holdToSprint, holdToCrouch;

    public float xSens, ySens;
    private CinemachineInputAxisController axisController;

    [Header("Crouching")]
    public float height;
    public float crouchHeight;
    public float centre; 
    public float crouchCentre;

    Vector3 vertVelocity = Vector3.zero; //used for vertical movement

    private float turnSmoothTime = 0.2f;
    private float turnSmoothVelocity;

    [Header("Groundcheck")]    
    public Transform groundCheck;
    public float groundDistance;
    public LayerMask groundMask;
    private bool isGrounded;

    //Input
    private GameInput gameInput;
    private Vector2 moveInput;

    //references
    private CharacterController cc;
    public Transform cam;
    private Animator animator;

    #region Events
    private void OnEnable()
    {
        EventManager.InputSettingsChanged += EMInputSettingsChanged;
    }

    private void OnDisable()
    {
        EventManager.InputSettingsChanged -=EMInputSettingsChanged;
    }

    private void EMInputSettingsChanged()
    {
        holdToSprint = PlayerPrefs.GetInt("HoldToSprint") == 1;
        holdToCrouch = PlayerPrefs.GetInt("HoldToCrouch") == 1;
        xSens = PlayerPrefs.GetFloat("XSensitivity");
        ySens = PlayerPrefs.GetFloat("YSensitivity");
        
    }

    void EMSensitivityChanged()
    {
        axisController = GetComponentInChildren<CinemachineInputAxisController>();
        foreach (var c in axisController.Controllers)
        {
            
            if (c.Name == "Look Orbit X")
            {             
                c.Input.Gain = xSens;
            }
            if (c.Name == "Look Orbit Y")
            {             
                c.Input.Gain = ySens;
            }
        }
    }

    #endregion

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        gameInput = new GameInput();       
        cc = GetComponent<CharacterController>();      
        animator = GetComponentInChildren<Animator>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        EMInputSettingsChanged(); //gets data about hold to sprint and crouch values
        EMSensitivityChanged(); //gets data about hold to sprint and crouch values
    }

    // Update is called once per frame
    void Update()
    {
        Gravity();
        Animation();
        Movement(); 
    }

    #region Input
    public void JumpInput(InputAction.CallbackContext ct)
    {     
        if (ct.performed && isGrounded && !isCrouching)
        {            
            Jump();
        }
    }

    public void MoveInput(InputAction.CallbackContext ct)
    {
        moveInput = ct.ReadValue<Vector2>();
    }

    public void SprintInput(InputAction.CallbackContext ct)
    {
        if (isCrouching)
        {
            isCrouching = false; //prevents crouch + sprint
        }

        //hold to sprint
        if (holdToSprint)
        {
            if (ct.performed)
            {
                isSprinting = true;
            }
            if (ct.canceled)
            {
                isSprinting = false;
            }
        }

        else
        {
            //toggle sprint
            if (ct.performed)
            {
                isSprinting = isSprinting ? false : true;
            }
        }
        
    }

    public void CrouchInput(InputAction.CallbackContext ct)
    {
        if (isSprinting)
        {
            isSprinting = false; //prevents crouch + sprint
        }

        //hold to crouch
        if (holdToSprint)
        {
            if (ct.performed)
            {
                isCrouching = true;
            }
            if (ct.canceled)
            {
                isCrouching = false;
            }
        }

        else
        {
            //toggle crouch
            if (ct.performed)
            {
                isCrouching = isCrouching ? false : true;
            }
        }        
    }
    #endregion

    void Movement()
    {        
        //crouching
        if (isCrouching)
        {
            cc.height = crouchHeight;
            cc.center = new Vector3(0, crouchCentre, 0);
        }
        else
        {
            cc.height = height;
            cc.center = new Vector3(0, centre, 0);
        }


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

            cc.Move(moveDir.normalized * targetSpeed * Time.deltaTime);

        }
    }

    void Jump() => vertVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity); //lazy code lol

    void Gravity()
    {
        // Ground check
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);        

        //gravity
        vertVelocity.y += gravity * Time.deltaTime;
        cc.Move(vertVelocity * Time.deltaTime);

        // Conditions for landing
        if (isGrounded && vertVelocity.y <= 0)
        {
            vertVelocity.y = -2f;
        }
    }  
    
    void Animation()
    {    
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
