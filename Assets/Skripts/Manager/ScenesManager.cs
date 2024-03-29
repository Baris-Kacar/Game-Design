using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{

    public static ScenesManager Instance;

    private void Awake() {
        Instance = this;
    }
    public enum Scene {
        MainMenu,
        Level1
    }
    public void LoadScene(Scene scene) {
        SceneManager.LoadScene(scene.ToString());
    }
    public void LoadNewGame() {
        SceneManager.LoadScene(Scene.Level1.ToString());
    }
    public void LoadNextScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void LoadMainMenu() {
        SceneManager.LoadScene(Scene.MainMenu.ToString());
    }
   public void LoadMainMenuScene()
    {
        SceneManager.LoadScene(Scene.MainMenu.ToString());
    }
    public void Restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Quit() {
        Application.Quit();
    }
}
