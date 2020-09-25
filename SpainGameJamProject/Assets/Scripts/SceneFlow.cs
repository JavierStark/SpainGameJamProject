using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneFlow : MonoBehaviour
{
    public void ChangeScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
        ResetTimeScale();
    }
    public void ChangeScene(int sceneIndex) {
        SceneManager.LoadScene(sceneIndex);
        ResetTimeScale();
    }

    public void NextScene() {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene+1);
        ResetTimeScale();
    }

    public void ReloadScene() {
        ResetTimeScale();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame() {
        Application.Quit();
    }

    public void ResetTimeScale() {
        Time.timeScale = 1;
    }
}
