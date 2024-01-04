using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public GameObject gameOverUI;
    public GameObject instructionsPanel;

    private bool gamePaused = false;

    
    // Start is called before the first frame update
    void Start()
    {
        
        // Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (gameOverUI.activeInHierarchy)
        {
           // Cursor.visible = true;
            //Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            //Cursor.visible = false;
            //Cursor.lockState = CursorLockMode.Locked;
        }
        if (gamePaused == false)
        {
            PauseGame();
        }
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D) ||
            Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (gamePaused)
            {
                ResumeGame();
            }
        }
    }

    public void gameOver()
    {
        gameOverUI.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        instructionsPanel.SetActive(false);
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        gamePaused = true;
        instructionsPanel.SetActive(true);
    }
}
