using UnityEngine;

/// <summary>
/// Made with via Brackeys on Youtube
/// https://www.youtube.com/watch?v=THnivyG0Mvo
/// </summary>

public class EnemyHealth : MonoBehaviour
{
    public float health = 10;
    Rigidbody rb;
    //[SerializeField] Transform camFPS;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void TakeDamage(float dmgAmount)
    {
        health -= dmgAmount;
        if (health <= 0f)
            Destroy(gameObject);
        //rb.AddForce(camFPS.transform.forward * 1000, ForceMode.Acceleration);
    }
}
