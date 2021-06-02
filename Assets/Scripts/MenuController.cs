using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] TransitionsController transition;

    /*private void Awake()
    {
        transition = GameObject.FindGameObjectWithTag("Transitor").GetComponent<TransitionsController>();
    }*/

    public void GoMenu()
    {
        SoundManager.PlaySound("ClickMenu");
        transition.a_out = true;
        transition.nextscene = "MainMenu";
    }

    public void GoStart()
    {
        SoundManager.StopSound();
        SoundManager.PlaySound("SongGame");
        SoundManager.PlaySound("ClickMenu");
        transition.a_out = true;
        transition.nextscene = "Tutorial";
    }
    public void GoLevels()
    {
        SoundManager.PlaySound("ClickMenu");
        transition.a_out = true;
        transition.nextscene = "Levels";
    }
    public void GoOptions()
    {
        SoundManager.PlaySound("ClickMenu");
        transition.a_out = true;
        transition.nextscene = "Options";
    }
    public void GoCredits()
    {
        SoundManager.PlaySound("ClickMenu");
        transition.a_out = true;
        transition.nextscene = "Credits";
    }
    public void Quit()
    {
        SoundManager.PlaySound("ClickMenu");
        Application.Quit();
    }
}

