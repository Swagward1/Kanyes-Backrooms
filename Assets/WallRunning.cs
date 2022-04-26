using UnityEngine;

public class WallRunning : MonoBehaviour
{
    [SerializeField] PlayerMovement player;
    [SerializeField] Transform orientation;
    Rigidbody rb;

    [Header("Wall Runnning")]
    [SerializeField] float wallDistance;
    [SerializeField] float minJumpHeight;
    [SerializeField] float wallRunGravity;
    [SerializeField] float wallRunJumpForce;

    [Header("Camera")]
    [SerializeField] Camera cam;
    [SerializeField] float fov;
    [SerializeField] float wallRunFov;
    [SerializeField] float wallRunFovTime;
    [SerializeField] float camTilt;
    [SerializeField] float camTiltTime;

    public float tilt {get; private set; }

    bool wallLeft = false;
    bool wallRight = false;

    RaycastHit leftWallHit;
    RaycastHit rightWallHit;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    bool CanWallRun()
    {
        return !Physics.Raycast(transform.position, Vector3.down, minJumpHeight);
    }

    void CheckWall()
    {
        wallLeft = Physics.Raycast(transform.position, -orientation.right, out leftWallHit ,wallDistance);
        wallRight = Physics.Raycast(transform.position, orientation.right, out rightWallHit ,wallDistance);
    }

    void Update()
    {
        CheckWall();

        if(CanWallRun())
        {
            if(wallLeft)
                StartWallRun();
            else if(wallRight)
                StartWallRun();
            else
                StopWallRun();
        }
        else
            StopWallRun();
    }

    void StartWallRun()
    {
        rb.useGravity = false;
        rb.AddForce(Vector3.down * wallRunGravity, ForceMode.Force);

        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, wallRunFov, wallRunFovTime * Time.deltaTime);
        
        if(wallLeft)
            tilt = Mathf.Lerp(tilt, -camTilt, camTiltTime * Time.deltaTime);
        else if(wallRight)
            tilt = Mathf.Lerp(tilt, camTilt, camTiltTime * Time.deltaTime);

        if(Input.GetKeyDown(player.jumpKey))
            if(wallLeft)
            {
                Vector3 wallRunJumpDetection = transform.up + leftWallHit.normal;
                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                rb.AddForce(wallRunJumpDetection * wallRunJumpForce * 100, ForceMode.Force);
            }
            else if(wallRight)
            {
                Vector3 wallRunJumpDetection = transform.up + rightWallHit.normal;
                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                rb.AddForce(wallRunJumpDetection * wallRunJumpForce * 100, ForceMode.Force);
            }
    }

    void StopWallRun()
    {
        rb.useGravity = true;

        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, fov, wallRunFovTime * Time.deltaTime);
        tilt = Mathf.Lerp(tilt, 0, camTiltTime * Time.deltaTime);
    }
}
