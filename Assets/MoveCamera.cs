using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField] Transform cam;
    
    void Update()
    {
        transform.position = cam.transform.position;
    }
}
