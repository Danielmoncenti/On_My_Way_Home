using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelsController : MonoBehaviour
{
    public GameObject manager;

    bool canLevel1;
    bool canLevel2;
    bool canLevel3;
    bool canLevel4;
    bool canLevel5;
    bool canLevel6;

    string tagg;

    // Start is called before the first frame update
    void Start()
    {
        canLevel1 = false;
        canLevel2 = false;
        canLevel3 = false;
        canLevel4 = false;
        canLevel5 = false;
        canLevel6 = false;

        tagg = this.tag;
    }

    // Update is called once per frame
    void Update()
    {
        canLevel1 = LevelsManager.Instance.canLevel1;
        canLevel2 = LevelsManager.Instance.canLevel2;
        canLevel3 = LevelsManager.Instance.canLevel3;
        canLevel4 = LevelsManager.Instance.canLevel4;
        canLevel5 = LevelsManager.Instance.canLevel5;
        canLevel6 = LevelsManager.Instance.canLevel6;

        switch (tagg)
        {
            case "Level1":
                if (!canLevel1) GetComponent<Button>().interactable = false; 
                break;
            case "Level2":
                if (!canLevel2) GetComponent<Button>().interactable = false;
                break;
            case "Level3":
                if (!canLevel3) GetComponent<Button>().interactable = false;
                break;
            case "Level4":
                if (!canLevel4) GetComponent<Button>().interactable = false;
                break;
            case "Level5":
                if (!canLevel5) GetComponent<Button>().interactable = false;
                break;
            case "Level6":
                if (!canLevel6) GetComponent<Button>().interactable = false;
                break;
        }
    }

    public void Tutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void Level1 ()
    {
        SceneManager.LoadScene("Level1");
    }

    public void Level2()
    {
        SceneManager.LoadScene("Level2");
    }

    public void Level3()
    {
        SceneManager.LoadScene("Level3");
    }

    public void Level4()
    {
        SceneManager.LoadScene("Level4");
    }

    public void Level5()
    {
       
        SceneManager.LoadScene("Level5");
    }

    public void Level6()
    {
        SceneManager.LoadScene("Level6");
    }

}
