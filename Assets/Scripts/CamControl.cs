using UnityEngine;
using UnityEngine.SceneManagement;

public class CamControl : MonoBehaviour
{
    public float sensitivityX = 250;
    public float sensitivityY = 250;
    public float defaultView = 90;
    public float zoomView = 30;

    [SerializeField] Transform cam;
    [SerializeField] Transform orientation;
    [SerializeField] PlayerMovement player;
    float mouseX;
    float mouseY;
    float multi = .01f;

    float xRotation;
    float yRotation;

    [SerializeField] Camera playerCamera;
    [SerializeField] GameObject clippingObjects;

    void Start()
    {
        playerCamera.fieldOfView = defaultView;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        //create temp reference to current scene
        Scene currentScene = SceneManager.GetActiveScene();

        //grab the scenes name
        string sceneName = currentScene.name;


        mouseX = Input.GetAxisRaw("Mouse X");
        mouseY = Input.GetAxisRaw("Mouse Y");

        yRotation += mouseX * sensitivityX * multi;
        xRotation -= mouseY * sensitivityY * multi;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        cam.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.transform.rotation = Quaternion.Euler(0, yRotation, 0);

        if(sceneName == "Level3")
            CheckForZoomL3();
        else
            CheckForZoom();
    }

    void CheckForZoom()
    {
        if (Time.timeScale == 1)
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                playerCamera.fieldOfView = zoomView;
                sensitivityX = 85f;
                sensitivityY = 85f;
                clippingObjects.SetActive(false);
            }

            if (Input.GetKeyUp(KeyCode.C))
            {
                playerCamera.fieldOfView = defaultView;
                sensitivityX = 250f;
                sensitivityY = 250f;
                clippingObjects.SetActive(true);
            }
        }
    }

    void CheckForZoomL3()
    {
        if (Time.timeScale == 1)
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                playerCamera.fieldOfView = zoomView;
                sensitivityX = 85f;
                sensitivityY = 85f;
                clippingObjects.SetActive(true);
            }

            if (Input.GetKeyUp(KeyCode.C))
            {
                playerCamera.fieldOfView = defaultView;
                sensitivityX = 250f;
                sensitivityY = 250f;
                clippingObjects.SetActive(false);
            }
        }
    }
}