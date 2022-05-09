using UnityEngine;

public class SlightForceAddition : MonoBehaviour
{
    public float force = 4;
    float multi = 20;
    Rigidbody rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        float r = Random.Range(-15, 15);
        rb.AddForce(transform.forward * force * multi, ForceMode.Acceleration);
        rb.AddTorque(new Vector3(r, r, r) * 10);

    }
}
