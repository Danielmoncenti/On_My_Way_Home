using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject arrows;
    public float menuoptions;
    KeyCode upButton = KeyCode.W;
    KeyCode downButton = KeyCode.S;
    KeyCode select = KeyCode.Space;
    // Start is called before the first frame update
    void Start()
    {
        menuoptions = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(upButton))
        {
            if (menuoptions == 0)
            {
                menuoptions = 2;
            }
            else
            {
                menuoptions -= 1;
            }
        }
        if (Input.GetKeyDown(downButton))
        {
            if (menuoptions == 2)
            {
                menuoptions = 0;
            }
            else
            {
                menuoptions += 1;
            }
        }
        if (Input.GetKeyDown(select))
        {
            switch (menuoptions)
            {
                case 0:
                    SceneManager.LoadScene("SampleScene");
                    break;
                case 1:
                    arrows.transform.position = new Vector2(0, -20);
                    break;
                case 2:
                    arrows.transform.position = new Vector2(0, -60);
                    break;
            }
        
        }
        switch (menuoptions)
        {
            case 0:
                arrows.transform.position = new Vector2(0, 20);
                break;
            case 1:
                arrows.transform.position = new Vector2(0, -20);
                break;
            case 2:
                arrows.transform.position = new Vector2(0, -60);
                break;
        }
    }
}

