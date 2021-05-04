using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool gameispaused = false;
    public bool ispaused;
    public GameObject pauseCanvas;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameispaused) {
                Resume();
            }
            else {
                pause();
            }
        }

         void pause() {
            Time.timeScale = 0;
            pauseCanvas.SetActive(true);
            gameispaused = true;
            //SoundManager.StopSound();
         }
        
        if (!gameispaused) pauseCanvas.SetActive(false);
        ispaused = gameispaused;
  
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Resume()
    {
        Time.timeScale = 1;
        pauseCanvas.SetActive(false);
        gameispaused = false;
        //SoundManager.PlaySound("GameSong");
    }

    public void GoMainMenu()
    {
        Destroy(GameManagerController.Instance.gameObject);
        SceneManager.LoadScene("MainMenu");
    }

}
