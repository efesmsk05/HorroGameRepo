using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Threading;
using TMPro;
using UnityEngine;

public class playerController : MonoBehaviour
{

    // En kýsa sürede daha derli toplu þekile geçilecektir

    public static playerController instance;// diðer scriptlerden veri çaðýrmayý kolaylaþtýyor


    [Header("Movement")]

    public GameObject playerTransform;
    public float moveSpeed;
    [SerializeField] float groundDrag;
    public bool isWalking = false;
    public bool idle;
    public bool walkingTimerControl = false;

    float walkingTimer = 0f;

    private float horizontalInput;
    private float verticalInput;

    Vector3 moveDirection;
    public Transform orientation;

    CharacterController characterController;

    public AudioSource walkSound; //yürüme sesi
    public AudioSource runSound; //koþma sesi

    [Header("KeyInput")]
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;
    [SerializeField] private KeyCode runKey = KeyCode.LeftShift;
    [SerializeField] private KeyCode crouchKey = KeyCode.LeftControl;


    [Header("GroundControl")]
    public LayerMask ground;
    public bool grounded;
    private float playerHeight = 2f;

    [Header("JumpSystem")]
    [SerializeField] private float jumpForce;
    [SerializeField] private float airMultiplier;
    [SerializeField] private float jumpHeight = 3f;
    bool readyToJump = true;
    public bool isJumping = false;
    float gravity = -9.81f;
    Vector3 velocity;
    float gravityMultiplier = 2f;

    [Header("RunSystem")]
    [SerializeField] private float maxRunSpeed;
    float runSpeed = 0f;
    bool runAccleartion = false;

    [SerializeField] private float runCoolDown;
    [SerializeField] private float runJumpForce;
    public bool isRunning = false;
    public bool readyToRun = true;

    [Header("CoruchSystem")]
    public float crouchSpeedMultiplier;
    bool readyCrouch = true;
    [SerializeField] float crouchHeight;
    private bool crouchControl;
    public bool isCrouching;


    [Header("Triger Collider")]
    [SerializeField] private TextMeshProUGUI pickUp;



    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

        }
        
        Cursor.lockState = CursorLockMode.Locked;

        Cursor.visible = false; // cros görünüþünü kapatýyor


    }


    void Start()
    {

        
        characterController = GetComponent<CharacterController>();

        //DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        Gravity();
        InputManager();
        Movement();

    }
    private void FixedUpdate()
    {
        isGrounded();// ground Control
        CrouchControl();


    }

    private void InputManager()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        #region Jump
        ////jump
        //if (Input.GetKey(jumpKey) && grounded && readyToJump && readyCrouch)
        //{
        //    Jump();
        //    readyToJump = false;
        //    isJumping = true;

        //}

        //if (Input.GetKeyUp(jumpKey))
        //{
        //    readyToJump = true;
        //    isJumping = false;
        //    isJumping = false;


        //}
        #endregion

        #region Run
        if (Input.GetKey(runKey) && grounded && readyToRun  && PlayerStatsUiManager.instance.stamineOver == false && readyCrouch) // koþma
        {
            if (!idle)
            {
                Run();
                isRunning = true;
                isWalking = false;
                //sounds
                runSound.enabled = true;
                walkSound.enabled = false;
            }
            

        }
        if (Input.GetKeyUp(runKey))// Run reset
        {
            isWalking = true;

            Invoke(nameof(ResetRun), .1f);
            // sounds
            runSound.enabled = false;


        }

        //if (Input.GetKey(runKey) && !readyToJump )// koþarken zýplayýnca daha öne gitmemizi saðlýyor
        //{
        //    characterController.Move(moveDirection * moveSpeed * (airMultiplier + .3f) * Time.deltaTime);
        //    gravityMultiplier = 2.2f;
        //    isJumping = true;

        //}

        #endregion

        #region Crouch

        if (Input.GetKey(crouchKey) && readyCrouch && !isRunning && !crouchControl) // eðilme
        {
            //crouch mekanik
            moveSpeed -= crouchSpeedMultiplier;
            readyCrouch = false;
            transform.localScale = new Vector3(transform.localScale.x , crouchHeight, transform.localScale.z);
            isCrouching = true;
            
        }
        else if (Input.GetKeyUp(crouchKey ) && !crouchControl ) 
        {

            transform.localScale = new Vector3(transform.localScale.x, 1f, transform.localScale.z);
            moveSpeed += crouchSpeedMultiplier;
            Invoke(nameof(ResetCrouch),.1f);
        }

        if(!crouchControl && !readyCrouch && transform.localScale.y == crouchHeight && !Input.GetKey(crouchKey)) // zeminin atlýndasýn ve elini tuþtan çekince dýþarý çýkýlýdýðýnda otomatik kalkmasýný saðlýyor
        {
            transform.localScale = new Vector3(transform.localScale.x, 1f, transform.localScale.z);
            moveSpeed += crouchSpeedMultiplier;
            Invoke(nameof(ResetCrouch), .1f);
        }

        #endregion
    }

    private void Movement() // yürüme
    {
        // baktýðýmýz yöne doðru haraket etmemizi saðlýyor 
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        RunSpeedControl();

        if(grounded) 
        {
            gravityMultiplier = 2f;
            characterController.Move(moveDirection.normalized * (moveSpeed + runSpeed) * Time.deltaTime);

            #region WalkingCameraNoiseTimeControl
            if (idle == false && walkingTimerControl == false)
            {
                if(walkingTimer >= .5)
                {
                    walkingTimer = 0;
                    walkingTimerControl = true;
                }
                walkingTimer += Time.deltaTime;

            }
            #endregion

            if (!isRunning && !isCrouching)
            {
                walkSound.enabled = true;
            }
            else if (isCrouching)
            {
                walkSound.enabled = false;
            }
        }
        else
        {
            characterController.Move(moveDirection.normalized * moveSpeed *  airMultiplier * Time.deltaTime);
            //hýzýmýzým yüzde 10 u kadar ileri atar momentum muhabeti

            isWalking = false;
            walkSound.enabled = false;
        }


        #region IDLE Control
        if ( moveDirection.x == 0f && grounded)// Duruyorsan IDLE
        {
            idle = true;
            walkingTimerControl = false;    

            isWalking = false ;
            isRunning = false;

            walkSound.enabled = false;
        }
        else
        {
            idle = false;
        }

        #endregion

        //walking control
        if (moveDirection.x != 0f && grounded && !isRunning && !isJumping)
        {
            isWalking = true;
        }
        
    }

    private void Run() // koþma komutu !! move speedi deðiþtiricekseniz ResetRun() daki deðeri de deðiþtirin
    {

        // run speed 0.16 
        //hýz yavaþça artma kotnrolü
            characterController.Move(moveDirection * (moveSpeed + runSpeed) * Time.deltaTime);

    }
    void ResetRun()
    {
        isRunning = false;
        readyToRun = true;
        runAccleartion = false;

    }

    void RunSpeedControl()
    {
        //run ++ acceleration
        if(isRunning == true)
        {
            if (runAccleartion == false)
            {
                if (runSpeed >= maxRunSpeed)
                {
                    runSpeed = maxRunSpeed;
                    runAccleartion = true;
                }
                else
                {
                    runSpeed += .1f * Time.deltaTime;
                }
            }
        }
        else if (isRunning == false) // run -- Acceleration
        {
            if(runSpeed <= 0f)
            {
                runSpeed = 0f;
            }
            else
            {
                runSpeed -= Time.deltaTime * .1f;

            }
        }

    }

    void ResetCrouch()
    {
        readyCrouch = true;
        isCrouching = false;
    }
    private void Jump() // zýplama komutu 
    {
        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }


    private bool  isGrounded()
    {
        //karakterimizin boyunun biraz altýný raycasttle sürekli kontrol ediyor
        return grounded = Physics.Raycast(transform.position , Vector3.down , playerHeight * .5f + .2f,ground); //ground Control Raycast
    }
    private bool CrouchControl()
    {
        return crouchControl = Physics.Raycast(transform.position ,Vector3.up , playerHeight * .7f+ .2f, ground);
    }

    void Gravity()
    {
        velocity.y += gravity * Time.deltaTime * gravityMultiplier;
        characterController.Move(velocity * Time.deltaTime);
    }

    

}
