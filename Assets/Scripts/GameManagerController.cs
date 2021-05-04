﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;
using System.IO;

public class GameManagerController : MonoBehaviour
{
    public static GameManagerController Instance { get; private set; }

    private enum LIFES { NONE, ONE, TWO, THREE, FOUR, FIVE, SIX };
    private enum POINTS { COIN, BIGCOIN, LIFEUP };

    //Texto de los puntos y vidas
    private Text Point_text;
    private Text Lifes_text;

    //Puntos y vida de Spikey
    public int totalLifes = 3;
    public int life = 3;
    public int points = 0;

    //Animacion de las vidas
    //public bool Iddle = false;
    public bool OneUp = false;
    public bool OneDown = false;
    public bool TryAgain = false;

    //Booleanos para los puntos
    public bool bigCoin = false;
    public bool coin = false;
    public bool lifeUp = false;
    public bool lifeDown = false;

    //Variable para sumar una vida cada 100 monedas
    public int internPoints = 0;
  
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Debug.Log("Warning: multiple " + this + " in scene!!");
        }

        Point_text = GameObject.FindGameObjectWithTag("Points").GetComponent<Text>();
        Lifes_text = GameObject.FindGameObjectWithTag("Lifes").GetComponent<Text>();
    }

    //Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Point_text = GameObject.FindGameObjectWithTag("Points").GetComponent<Text>();
        Lifes_text = GameObject.FindGameObjectWithTag("Lifes").GetComponent<Text>();

        if (coin)
        {
            points++;
            internPoints++;
            coin = false;
        }
        else if (bigCoin)
        {
            points += 20;
            internPoints += 20;
            bigCoin = false;
        }

        if (internPoints >= 100)
        {
            internPoints = 0;
            lifeUp = true;
        }


        if (lifeUp)
        {
            if (life == 6)
            {
                totalLifes++;
                life = 3;
                OneUp = true;
                TryAgain = true;
                lifeUp = false;
            }
            else if (life < 6)
            {
                life++;
                OneUp = true;
                lifeUp = false;
            }
        }
        else if (lifeDown)
        {
            if (life == 1)
            {
                totalLifes--;
                life = 3;
                OneDown = true;
                TryAgain = true;
                lifeDown = false;
            }
            else
            {
                life--;
                OneDown = true;
                lifeDown = false;

            }

        }

        CheckTotalLifes();

        Lifes_text.text = string.Format("{0:00}", totalLifes);
        Point_text.text = string.Format("{0:000}", points);
    }

    private void CheckTotalLifes()
    {
        if (totalLifes < 0)
        {
            SceneManager.LoadScene("GameOver");
            Destroy(this);
        }
    }
}
