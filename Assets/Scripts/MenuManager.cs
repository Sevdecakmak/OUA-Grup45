using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject menuPanel;

    private bool isMenuActive = false;

    private void Start()
    {
        // Menü panelini başlangıçta kapat
        menuPanel.SetActive(false);
    }

    private void Update()
    {
        // Esc tuşuna basıldığında menüyü aç/kapat
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isMenuActive = !isMenuActive;
            menuPanel.SetActive(isMenuActive);
            Time.timeScale = isMenuActive ? 0 : 1; // Oyunu duraklat/başlat
        }
    }

    public void ResumeGame()
    {
        isMenuActive = false;
        menuPanel.SetActive(false);
        Time.timeScale = 1; // Oyunu başlat
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1; // Oyunu başlat
        SceneManager.LoadScene(0);
        menuPanel.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
