using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            LoadCredits();
        }
    }

    public void LoadCredits()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
