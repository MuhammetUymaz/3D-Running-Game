using UnityEngine;

public class movementCode : MonoBehaviour
{   

    [Tooltip("Assign player's Camera")]
    [SerializeField] Camera theCamera;
    [SerializeField] Transform playerBody;
    [SerializeField] Animator playerAnimator;

    [Header("Movement")]

    [Tooltip("Assign Character Controller of the player")]
    [SerializeField] CharacterController characterController;
    [SerializeField] float speed = 12f; //Movement Speed

    [SerializeField] float normalSpeed; //Walking speed
    [SerializeField] float sprintSpeed = 24;
    public Vector3 velocity;
    const float gravity = -9.81f;

    [Header("Jumping")]
    [Tooltip("How far player can jump to")]
    [SerializeField] float jumpHeight;

    [SerializeField] Transform groundCheck;
    [SerializeField] float groundDistance = 0.04f;
    [SerializeField] LayerMask groundMask;
    bool isGrounded;

    [Header("Rotating")]
    [Tooltip("How many speed the camera is rotated with")]
    [SerializeField] float rotatingSpeed = 1f;

    //Rotating With UI Controllers
    [HideInInspector]
    public bool rotateRight, rotateLeft;

    void Start() 
    {
        rotatingSpeed *= screenResolution.coefficientX;

        groundDistance *= screenResolution.coefficientX;
    }

    void Update()
    {
        //Player can look right/left and go when game is started
        if(!gameManagment.managment.isGameStarted)
            return;
        
        LookingAround();
        Movement();   
    }

    void LookingAround()
    {
//For just Editor -Rotating with keyboards       
#if UNITY_EDITOR
        if(Input.GetKey(KeyCode.D))
            rotateRight = true;
        else if(Input.GetKeyUp(KeyCode.D))
            rotateRight = false;

        if(Input.GetKey(KeyCode.A))
            rotateLeft = true;
        else if(Input.GetKeyUp(KeyCode.A))
            rotateLeft = false;
#endif

        //Rotating
        if(rotateRight)
            playerBody.Rotate(Vector3.up * rotatingSpeed * Time.deltaTime);
        
        if(rotateLeft)
            playerBody.Rotate(Vector3.up * -rotatingSpeed * Time.deltaTime);
    }

    void Movement()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        //Check walking sound
        if(!audioManager.instance.walkingAudioSource.isPlaying)
        {
            audioManager.instance.PlayMovingSprintingSound(isGrounded);
        }

//For just Editor -Sprint & Jumping
#if UNITY_EDITOR
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            Sprint(true);
            audioManager.instance.ChangeWalkingAudioSourcePitch(false);
        }
            
        else if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            Sprint(false);
            audioManager.instance.ChangeWalkingAudioSourcePitch(true);
        }

        if(Input.GetKeyDown(KeyCode.Space))
            Jumping();
#endif
            

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f * screenResolution.coefficientY;
        }

        characterController.Move(transform.forward * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime * screenResolution.coefficientY;
        
        //To avoid velocity's y to down too much
        if(velocity.y < -5)
            velocity.y = -5;
        
        
        characterController.Move(velocity * Time.deltaTime);

    }

    public void Sprint(bool controlling)
    {
        //Movement speed
        speed = (controlling) ? sprintSpeed : normalSpeed;

        //Animation speed
        float animationSpeed = (controlling) ? 2: 1;
        playerAnimator.SetFloat("speedValue", animationSpeed);
    }

    public void Jumping()
    {
        if(isGrounded)
        {
            //Sounds
            audioManager.instance.walkingAudioSource.Pause();
            audioManager.instance.JumpingSoundPlay();

            velocity.y = Mathf.Sqrt(jumpHeight * -2f * screenResolution.coefficientY * gravity);
        }
    }
}
