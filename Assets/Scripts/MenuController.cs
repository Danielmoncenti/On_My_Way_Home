using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    //SoundManager.PlaySound("ClickMenu");

    public void GoStart()
    {
        SoundManager.StopSound();
        SoundManager.PlaySound("SongGame");
        SceneManager.LoadScene("Tutorial");
    }
    public void GoOptions()
    {
        SceneManager.LoadScene("Options");
    }
    public void GoCredits()
    {
        SceneManager.LoadScene("Credits");
    }
    public void Quit()
    {
        Application.Quit();
    }
}

