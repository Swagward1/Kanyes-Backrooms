using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] WallRunning wallRun;
    [SerializeField] Transform cam;
    [SerializeField] Transform orientation;

    [Header("Camera Motion")]
    [SerializeField] float sensX;
    [SerializeField] float sensY;
    float mouseX;
    float mouseY;

    float xRot;
    float yRot;

    float multi = .01f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        CameraInput();

        cam.transform.localRotation = Quaternion.Euler(xRot, yRot, wallRun.tilt);
        orientation.transform.rotation = Quaternion.Euler(0, yRot, 0);
    }

    void CameraInput()
    {
        mouseX = Input.GetAxisRaw("Mouse X");
        mouseY = Input.GetAxisRaw("Mouse Y");

        yRot += mouseX * sensX * multi;
        xRot -= mouseY * sensY * multi;

        xRot = Mathf.Clamp(xRot, -90f, 90f);
    }
}
