using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScreen : MonoBehaviour
{
    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        CustomSceneManager.LoadPrevScene();
        Time.timeScale = 1;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
