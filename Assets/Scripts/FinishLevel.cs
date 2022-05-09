using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLevel : MonoBehaviour
{
    public Timer timer;
    public CamControl cam;
    public PlayerMovement player;
    public Gun gun;
    public GameObject finishUI;
    public GameObject finC;

    void Update()
    {
        //create temp reference to current scene
        Scene currentScene = SceneManager.GetActiveScene();

        //grab the scenes name
        string sceneName = currentScene.name;

        if (sceneName == "Level3")
        {
            if (gun.dreamKills == 1)
            {
                Instantiate(finC, new Vector3(7, 5, -4), Quaternion.identity);
                gun.dreamKills = 21;
            }
        }
    }

    public void End()
    {
        //Debug.Log("hello this works");
        finishUI.SetActive(true);

        Time.timeScale = .5f;
        //timer.StopCoroutine("Stopwatch");
        player.StopCoroutine("UpdateStamina");
        timer.StopCoroutine("Stopwatch");

        cam.enabled = false;
        player.enabled = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}