using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    #region game stuff
    public GameObject playerUI;
    public GameObject pauseUI;
    public GameObject settingsUI;
    public GameObject winUI;
    public GameObject deathUI;

    public bool optionsIsDisplayed;
    #endregion

    #region menu stuff
    public GameObject mainMenu;
    public GameObject levelSelect;
    #endregion

    //lets you input whatever scene you want
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    //reset scene just played
    public void ReplayScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OpenOptions()
    {
        playerUI.SetActive(false);
        pauseUI.SetActive(false);
        settingsUI.SetActive(true);

        optionsIsDisplayed = true;
    }

    public void CloseOptions()
    {
        playerUI.SetActive(true);
        pauseUI.SetActive(true);
        settingsUI.SetActive(false);

        optionsIsDisplayed = false;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void OpenLS()
    {
        mainMenu.SetActive(false);
        levelSelect.SetActive(true);
    }

    public void ExitLS()
    {
        mainMenu.SetActive(true);
        levelSelect.SetActive(false);
    }
}