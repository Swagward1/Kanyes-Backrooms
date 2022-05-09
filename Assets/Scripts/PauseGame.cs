using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour
{
    [SerializeField] CamControl cam;
    [SerializeField] PlayerMovement player;
    [SerializeField] Timer time;
    [SerializeField] LevelController canvas;

    void Update()
    {
        //create temp reference to current scene
        Scene currentScene = SceneManager.GetActiveScene();

        //grab the scenes name
        string sceneName = currentScene.name;

        if(sceneName != "Level3")
            PauseGameState();
        else if(sceneName == "Level3")
            Level3Pause();
    }

    void PauseGameState()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && Time.timeScale == 1)
        {
            canvas.playerUI.SetActive(false);
            canvas.pauseUI.SetActive(true);
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            cam.sensitivityX = 0;
            cam.sensitivityY = 0;
        }

            else if (Input.GetKeyDown(KeyCode.Escape) && Time.timeScale == 0 && !canvas.optionsIsDisplayed)
            {
                canvas.playerUI.SetActive(true);
                canvas.pauseUI.SetActive(false);
                Time.timeScale = 1;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                cam.sensitivityX = 250;
                cam.sensitivityY = 250;
                
                if(player.timerTrig == null)
                    time.StartCoroutine("Stopwatch");
            }

            else if(Input.GetKeyDown(KeyCode.Escape) && Time.timeScale == 0 && canvas.optionsIsDisplayed)
            {
                canvas.settingsUI.SetActive(false);
                canvas.pauseUI.SetActive(false);
                canvas.playerUI.SetActive(true);
                Time.timeScale = 1;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                cam.sensitivityX = 250;
                cam.sensitivityY = 250;

                if(player.timerTrig == null)
                    time.StartCoroutine("Stopwatch");
            }
            
        if (Input.GetKeyDown(KeyCode.Tab) && Time.timeScale == 1)
            canvas.ReplayScene();
    }

    void Level3Pause()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && Time.timeScale == 1)
        {
            //pause
            canvas.playerUI.SetActive(true);
            canvas.pauseUI.SetActive(true);
            Time.timeScale = .6f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            cam.sensitivityX = 83;
            cam.sensitivityY = 83;
        }

            else if (Input.GetKeyDown(KeyCode.Escape) && Time.timeScale == .6f && !canvas.optionsIsDisplayed)
            {
                canvas.playerUI.SetActive(true);
                canvas.pauseUI.SetActive(false);
                Time.timeScale = 1;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                cam.sensitivityX = 250;
                cam.sensitivityY = 250;
                
                if(player.timerTrig == null)
                    time.StartCoroutine("Stopwatch");
            }

            else if(Input.GetKeyDown(KeyCode.Escape) && Time.timeScale == .3f && canvas.optionsIsDisplayed)
            {
                canvas.settingsUI.SetActive(false);
                canvas.pauseUI.SetActive(false);
                canvas.playerUI.SetActive(true);
                Time.timeScale = 1;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                cam.sensitivityX = 250;
                cam.sensitivityY = 250;

                if(player.timerTrig == null)
                    time.StartCoroutine("Stopwatch");
            }
            
        if (Input.GetKeyDown(KeyCode.Tab) && Time.timeScale == 1)
            canvas.ReplayScene();
    }
}