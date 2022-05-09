using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTimer : MonoBehaviour
{
    public Timer timer;
    public PlayerMovement player;
    public GameObject destroyObj;

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            timer.StartCoroutine("Stopwatch");
            player.StartCoroutine("UpdateStamina");
            Destroy(destroyObj);
        }
    }
}
