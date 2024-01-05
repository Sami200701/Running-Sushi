using System.Collections;
using System.Collections.Generic;
using PlasticPipe.PlasticProtocol.Messages;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject pauseMenuUI;
    public bool inOptions = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused && !inOptions)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void mainMenu()
    {
        Time.timeScale = 1f;
        isPaused = false;
        SceneManager.LoadScene("StartMenu");
    }

    public void quitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void enterOptions()
    {
        inOptions = true;
    }

    public void exitOptions()
    {
        inOptions = false;
    }
}
