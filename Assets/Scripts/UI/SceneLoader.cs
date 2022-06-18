using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadIntro()
    {
        SceneManager.LoadScene("Intro");
    }

    
    public void LoadTutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }
    
    
    public void LoadMainLevel()
    {
        SceneManager.LoadScene("MainLevel");
    }


    public void QuitGame()
    {
        Application.Quit();
    }
}
