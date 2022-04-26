using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Camera cam;
    Rigidbody rb;
    RaycastHit slopeInfo;
    float height = 2;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftControl;

    [Header("Movement")]
    [SerializeField] Transform orientation;
    public float moveSpeed = 6;
    public float moveSpeedMulti = 10f;
    public float airMulti = .4f;
    public float walkFov = 90;
    public Vector3 moveDirection;
    public Vector3 moveSlopeDirection;
    float vMovement;
    float hMovement;

    [Header("Sprintint")]
    [SerializeField] float walkSpeed = 4f;
    [SerializeField] float sprintSpeed = 6f;
    [SerializeField] float accLerp = 10f;
    [SerializeField] float sprintFov = 110;

    [Header("Jumping")]
    [SerializeField] LayerMask defineGround;
    [SerializeField] Transform groundCheck;
    public bool isGrounded;
    public float jumpForce = 5f;
    float groundDist = .4f;

    [Header("Rigidbody")]
    public float groundDrag = 6f;
    public float airDrag = 2f;

    bool OnSlope()
    {
        if(Physics.Raycast(transform.position, Vector3.down, out slopeInfo, height / 2 + .5f))
            if(slopeInfo.normal != Vector3.up)
                return true;
            else
                return false;
        return false;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDist, defineGround);

        PlayerInput();
        DragControl();
        SpeedControl();

        if(Input.GetKeyDown(jumpKey) && isGrounded)
            Jump();

        moveSlopeDirection = Vector3.ProjectOnPlane(moveDirection, slopeInfo.normal);

    }

    void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    void PlayerInput()
    {
        hMovement = Input.GetAxisRaw("Horizontal");
        vMovement = Input.GetAxisRaw("Vertical");

        moveDirection = orientation.forward * vMovement + orientation.right * hMovement;
    }

    void SpeedControl()
    {
        if(Input.GetKey(sprintKey) && isGrounded)
        {
            moveSpeed = Mathf.Lerp(moveSpeed, sprintSpeed, accLerp * Time.deltaTime);
            //cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, sprintFov, accLerp * Time.deltaTime);
        }
        else
        {
            moveSpeed = Mathf.Lerp(moveSpeed, walkSpeed, accLerp * Time.deltaTime);
            //cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, walkFov, accLerp * Time.deltaTime);
        }

    }

    void DragControl()
    {
        if(isGrounded)
            rb.drag = groundDrag;
        else
            rb.drag = airDrag;
    }

    void FixedUpdate()
    {
        if(isGrounded && !OnSlope())
            rb.AddForce(moveDirection * moveSpeed * moveSpeedMulti, ForceMode.Acceleration);
        else if(isGrounded && OnSlope())
            rb.AddForce(moveSlopeDirection * moveSpeed * moveSpeedMulti, ForceMode.Acceleration);
        else if(!isGrounded)
            rb.AddForce(moveDirection * moveSpeed * moveSpeedMulti * airMulti, ForceMode.Acceleration);
    }
}
