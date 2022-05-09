using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletKill : MonoBehaviour
{
    public float bulletTime;
    Health hp;
    GameObject player;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        hp = player.GetComponent<Health>();

        Destroy(gameObject, bulletTime);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if (Time.timeScale == 1)
                hp.health--;

            Destroy(gameObject);
        }

        if (col.gameObject.CompareTag("Ground"))
            Destroy(gameObject);
    }


}
