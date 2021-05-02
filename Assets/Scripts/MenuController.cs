using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    //public enum MENUOPTIONS { START, CONTINUE, OPTIONS, CREDITS, QUIT };
    private int menuoptions;
    KeyCode upButton = KeyCode.W;
    KeyCode downButton = KeyCode.S;
    KeyCode select = KeyCode.Space;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.position = new Vector2(0, 45);
        menuoptions = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(upButton))
        {
            SoundManager.PlaySound("ClickMenu");
            menuoptions--;
            if (menuoptions < 0) { menuoptions = 4; }

        }
        else if (Input.GetKeyDown(downButton))
        {
            SoundManager.PlaySound("ClickMenu");
            menuoptions++;
            if (menuoptions > 4) { menuoptions = 0; }

        }
        else if (Input.GetKeyDown(select))
        {
            switch (menuoptions)
            {
                case 0:
                    SoundManager.StopSound();
                    SoundManager.PlaySound("SongGame");
                    SceneManager.LoadScene("Tutorial");
                    break;
                case 1:
                    // si hay partida guardada ir a ese nivel
                    break;
                case 2:
                    SceneManager.LoadScene("Options");
                    break;
                case 3:
                    SceneManager.LoadScene("Credits");
                    break;
                case 4:
                    Application.Quit();
                    break;
            }
        
        }
        switch (menuoptions)
        {
            case 0:
                gameObject.transform.position = new Vector2(0, 45);
                break;
            case 1:
                gameObject.transform.position = new Vector2(0, 0);
                break;
            case 2:
                gameObject.transform.position = new Vector2(0, -45);
                break;
            case 3:
                gameObject.transform.position = new Vector2(0, -90);
                break;
            case 4:
                gameObject.transform.position = new Vector2(0, -135);
                break;
        }
    }
}

