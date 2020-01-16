using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    public static int lives = 8;
    public static int score = 0;
    public GameObject pausePanel;

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex >= 2)
        {
            pausePanel.SetActive(false);
        }
    }

    private void Update()
    {
        if (lives > 8)
        {
            lives = 8;
        }
    }

    //functions for menu buttons - start
    public void StartGame()
    {
        lives = 8;
        score = 0;
        SceneManager.LoadScene(2);        
    }
    //functions for menu buttons - quit
    public void QuitGame()
    {
        Application.Quit();
    }
    //functions for menu buttons - instructions
    public void LoadInstructions()
    {
        SceneManager.LoadScene(1);
    }

    //function for instructions menu - go back to main menu
    public void BackToMenu()
    {
        if (SceneManager.GetActiveScene().buildIndex >= 2)
        {
            ResumeGame();
        }
        SceneManager.LoadScene(0);
    }
    
    //pause menu trigger
    public void PauseGame()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
    }
    //resume game
    public void ResumeGame()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }

    //use when beat the level
    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
