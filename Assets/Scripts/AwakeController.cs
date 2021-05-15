using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class AwakeController : MonoBehaviour
{
    public GameObject SoundManager;
    private int trueawake = 0;
    float timer;
    private void Awake()
    {
        if (trueawake == 0)
        {
            Instantiate(SoundManager);
            trueawake++;
        }
    }

    private void Update()
    {
        float delta = Time.deltaTime;
        timer += delta;
        if (timer > 1.8f) SceneManager.LoadScene("Intro");
    }
}

