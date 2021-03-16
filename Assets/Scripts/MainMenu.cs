using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    
    void Start()
    {
        //QuitButton.gameObject.SetActive(false);    
    }
    public void playGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Settings()
    {
        
    }
}
