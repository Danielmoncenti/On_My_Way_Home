using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionsController : MonoBehaviour
{
    Animator animator;
    public string nextscene;
    bool a_normal;
    public bool a_out;

    Scene currentScene;
    string sceneName;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;
        a_normal = false;
        a_out = false;
    }

    // Update is called once per frame
    void Update()
    {

        animator.SetBool("normal", a_normal);
        animator.SetBool("out", a_out);
    }

    public void SetNormal() {
        a_normal = true;
    }

    public void SetOut() { a_out = true; }

    void SetScene() { SceneManager.LoadScene(nextscene); }
}
