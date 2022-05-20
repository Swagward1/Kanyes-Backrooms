using UnityEngine;

public class ObjectSpin : MonoBehaviour
{

    [SerializeField] private float delay;
    [SerializeField] private float otherDelay;
    void Update()
    {
        float z = Mathf.PingPong(Time.time / delay, 1f);    
        Vector3 axis = new Vector3(1, 1, z);
        transform.Rotate(axis, otherDelay);
    }
}
