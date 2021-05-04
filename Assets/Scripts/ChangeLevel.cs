using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeLevel : MonoBehaviour
{
    public string nextlevel;

    Scene currentScene;
    string sceneName;

    // Start is called before the first frame update
    private void Start()
    {
        currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Spikey")
        {
            switch (sceneName)
            {
                case "Level1":
                    LevelsManager.Instance.canLevel1 = true;
                    break;
                case "Level2":
                    LevelsManager.Instance.canLevel1 = true;
                    break;
                case "Level3":
                    LevelsManager.Instance.canLevel1 = true;
                    break;
                case "Level4":
                    LevelsManager.Instance.canLevel1 = true;
                    break;
                case "Level5":
                    LevelsManager.Instance.canLevel1 = true;
                    break;
                case "Level6":
                    LevelsManager.Instance.canLevel1 = true;
                    break;
            }
            SceneManager.LoadScene(nextlevel);
        }
    }
}
