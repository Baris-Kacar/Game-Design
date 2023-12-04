using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame() {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        ScenesManager.Instance.LoadNextScene();
    }
    public void Options() {

    }
    public void Back() {

    }
    public void QuitGame() {
        Debug.Log("Quit!");
        ScenesManager.Instance.Quit();
    }
    public void RestartGame() {
        ScenesManager.Instance.LoadMainMenu();
    }
}
