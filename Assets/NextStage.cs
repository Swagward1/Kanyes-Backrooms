using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextStage : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject prevStage;
    [SerializeField] GameObject nextStage;

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            player.transform.position = new Vector3(transform.position.x + 10, transform.position.y + 10, transform.position.z);
            prevStage.SetActive(false);
            nextStage.SetActive(true);
            //Debug.Log("mother fucker can you just work for once");
        }
    }
}
