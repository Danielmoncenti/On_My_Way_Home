using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool gameispaused = false;
    public GameObject pauseMenu;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameispaused)
            {
                resume();
            }
            else
            {
                pause();
            }
        }

         void pause() 
         { 
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
            gameispaused = true;
         }
  
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        gameispaused = false;
    }
}
