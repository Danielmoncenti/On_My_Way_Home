using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool gameispaused = false;
    public bool ispaused;
    public GameObject pauseCanvas;
    public Button Menu;

    //bool canMenu = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SceneManager.LoadScene("Level1");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SceneManager.LoadScene("Level2");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SceneManager.LoadScene("Level3");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SceneManager.LoadScene("Level4");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            SceneManager.LoadScene("Level5");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            SceneManager.LoadScene("Level6");
        }


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

        //if (!canMenu) Menu.interactable = false;

        if (!gameispaused) pauseCanvas.SetActive(false);
        ispaused = gameispaused;
    }

    public void Quit()
    {
        SoundManager.PlaySound("ClickMenu");
        Application.Quit();
    }

    public void Resume()
    {
        Time.timeScale = 1;
        pauseCanvas.SetActive(false);
        gameispaused = false;
        SoundManager.PlaySound("ClickMenu");
        //SoundManager.PlaySound("GameSong");
    }

    public void GoMainMenu()
    {
        Time.timeScale = 1;
        pauseCanvas.SetActive(false);
        gameispaused = false;
        //Destroy(GameManagerController.Instance.gameObject);
        SoundManager.StopSound();
        SoundManager.PlaySound("ClickMenu");
        SoundManager.PlaySound("Song");
        SceneManager.LoadScene("MainMenu");
    }
}
