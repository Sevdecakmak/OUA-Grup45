using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //public GameObject fadeOut;
    //public GameObject loadText;

    //public GameObject loadButton;
    public int loadInt;

    void Start()
    {
        loadInt = PlayerPrefs.GetInt("AutoSave");
        
    }

    public void NewGame()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(loadInt);
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }


    public void NewGameScene1()
    {
        SceneManager.LoadScene(0);
    }
}