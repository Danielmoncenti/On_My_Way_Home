using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    public void GoStart() { 
        SceneManager.LoadScene("Level1");
        SoundManager.PlaySound("ClickMenu");
    }

    public void GoMenu() { 
        SceneManager.LoadScene("MainMenu");
        SoundManager.PlaySound("ClickMenu");
    }
}
