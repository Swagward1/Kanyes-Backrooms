using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    #region Variables
    [Header("Stamina")]
    [SerializeField] float currentStamina;
    [SerializeField] float totalStamina = 10;
    [SerializeField] Sprite fullSlot;
    [SerializeField] Sprite emptySlot;
    [SerializeField] Image[] staminaBar;

    [Header("Movement")]
    public float moveSpeed = 6f;
    public float speedPowerUp = 1f;
    public float speedMulti = 10f;
    public float airMulti = .4f;
    float horizontalMovement;
    float verticalMovement;
    Vector3 moveDirection;
    Vector3 slopeMoveDirection;

    [Header("Jumping/Ground Dectection")]
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundMask;
    public float jumpForce = 15f;
    bool isGrounded;
    float groundDistance = .4f;

    [Header("Keybinds")]
    [SerializeField] KeyCode jumpKey = KeyCode.Space;
    [SerializeField] KeyCode crouchKey = KeyCode.LeftShift;
    [SerializeField] KeyCode leapKey = KeyCode.F;
    
    [Header("Crouch & Sliding")]
    bool crouching;
    private Vector3 crouchScale = new Vector3(1, .5f, 1);
    private Vector3 playerScale;
    public float slideForce = 1.5f;
    public float slideMulti = 100f;

    [Header("Leap")]
    public bool canLeap = false;
    [SerializeField] float leapForce = 8f;
    [SerializeField] float leapMulti = 10f;

    [Header("Script References")]
    public CamControl cam;
    public Camera camFPS;
    public SFXManager sfx;
    public Timer time;
    public FinishLevel fin;
    
    [Header("Misc")]
    public Transform orientation;
    public GameObject timerTrig;
    Rigidbody rb;
    RaycastHit slopeHit;
    float playerHeight = 2f;
    public float groundDrag = 6f;
    public float airDrag = 2f;
    #endregion 


    void Start()
    {
        currentStamina = 2;

        rb = GetComponent<Rigidbody>();
        playerScale = transform.localScale;
        rb.freezeRotation = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight / 2 + .5f))
            if (slopeHit.normal != Vector3.up)
                return true;
            else
                return false;
        else
            return false;
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        PlayerInput();
        ControlDrag();

        if (Input.GetKeyDown(jumpKey) && isGrounded)
            Jump();

        slopeMoveDirection = Vector3.ProjectOnPlane(moveDirection, slopeHit.normal);

        //UpdateStamina();
    }

    void PlayerInput()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal") * speedPowerUp; //returns -1/1 when A or D is pressed (0 when idle)
        verticalMovement = Input.GetAxisRaw("Vertical") * speedPowerUp; //returns -1/1 when W or S is pressed (0 when idle)

        moveDirection = orientation.forward * verticalMovement + orientation.right * horizontalMovement;

        crouching = Input.GetKey(KeyCode.LeftShift);

        if (Input.GetKeyDown(crouchKey))
            StartCrouch();
        if (Input.GetKeyUp(crouchKey))
            StopCrouch();

        if (Input.GetKeyDown(leapKey) && canLeap)
            Leap();
    }

    void StartCrouch()
    {
        if (Time.timeScale == 1)
        {
            transform.localScale = crouchScale;
            transform.position = new Vector3(transform.position.x, transform.position.y - .5f, transform.position.z);
        }
    }

    void StopCrouch()
    {
        if (Time.timeScale == 1)
        {
            transform.localScale = playerScale;
            transform.position = new Vector3(transform.position.x, transform.position.y + .5f, transform.position.z);
        }
    }

    void Leap()
    {
        if (Time.timeScale == 1)
        {
            if (currentStamina >= 7)
            {
                rb.AddForce(camFPS.transform.forward * leapForce * leapMulti, ForceMode.Acceleration);
                currentStamina = currentStamina - 6;
            }
        }
    }

    void Jump()
    {
        if (crouching)
        {
            sfx.PlayJump();
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(transform.up * jumpForce / 1.25f, ForceMode.Impulse);
        }
        else
        {
            sfx.PlayJump();
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }
    }

    void ControlDrag()
    {
        if (isGrounded)
            rb.drag = groundDrag;
        else
            rb.drag = airDrag;
    }

    void FixedUpdate() //physics based updates handled here
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        if (isGrounded && !OnSlope())
            rb.AddForce(moveDirection * moveSpeed * speedMulti, ForceMode.Acceleration);
        else if (!crouching && isGrounded && OnSlope())
            rb.AddForce(slopeMoveDirection * moveSpeed * speedMulti, ForceMode.Acceleration);
        else if (crouching && isGrounded && OnSlope())
        {
            //Debug.Log("Slope Time");
            rb.AddForce(slopeMoveDirection * slideForce * slideMulti, ForceMode.Acceleration);
        }
        else if (!isGrounded)
            rb.AddForce(moveDirection * moveSpeed * speedMulti * airMulti, ForceMode.Acceleration);
    }

    IEnumerator UpdateStamina()
    {
        while(true)
        {
            currentStamina += Time.deltaTime;
            
            for (int i = 0; i < staminaBar.Length; i++)
            {
                if (currentStamina > totalStamina)
                    currentStamina = totalStamina; //restrict stamina to 10
                if (currentStamina < 0)
                    currentStamina = 0; //restrict so cant go below 0

                //determine if stamina should be full or empty
                if (i < currentStamina)
                    staminaBar[i].sprite = fullSlot;
                else
                    staminaBar[i].sprite = emptySlot;

                //show number of stamina slots
                if (i < totalStamina)
                    staminaBar[i].enabled = true;
                else
                    staminaBar[i].enabled = false;
            }
            yield return null;
        }
    }

    IEnumerator StopSpeedUp()
    {
        yield return new WaitForSeconds(10f);

        speedPowerUp = 1;
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.CompareTag("Start"))
        {
            time.StartCoroutine("Stopwatch");
            StartCoroutine("UpdateStamina");
            Destroy(timerTrig, .1f);
        }
        if(col.gameObject.CompareTag("Finish"))
            fin.End();    

        if(col.gameObject.CompareTag("LeapTrue"))
            canLeap = true;

        if(col.gameObject.CompareTag("AddSpeed"))
        {
            speedPowerUp = speedPowerUp + .1f;
            StartCoroutine(StopSpeedUp());
        }
    }
}