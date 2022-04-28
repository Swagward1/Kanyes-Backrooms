using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextStage : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject prevStage;
    [SerializeField] GameObject nextStage;
    [SerializeField] GameObject playerSpawn;

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            player.transform.position = playerSpawn.transform.position;
            prevStage.SetActive(false);
            nextStage.SetActive(true);
        }
    }
}
