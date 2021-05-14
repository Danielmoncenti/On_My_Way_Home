using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    //SoundManager.PlaySound("ClickMenu");
    public void GoMenu()
    {
        SoundManager.PlaySound("ClickMenu");
        SceneManager.LoadScene("MainMenu");
    }
    public void GoStart()
    {
        SoundManager.StopSound();
        SoundManager.PlaySound("SongGame");
        SoundManager.PlaySound("ClickMenu");
        SceneManager.LoadScene("Tutorial");
    }
    public void GoLevels()
    {
        SoundManager.PlaySound("ClickMenu");
        SceneManager.LoadScene("Levels");
    }
    public void GoOptions()
    {
        SoundManager.PlaySound("ClickMenu");
        SceneManager.LoadScene("Options");
    }
    public void GoCredits()
    {
        SoundManager.PlaySound("ClickMenu");
        SceneManager.LoadScene("Credits");
    }
    public void Quit()
    {
        SoundManager.PlaySound("ClickMenu");
        Application.Quit();
    }
}

