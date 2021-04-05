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
    public Text Point_text;
    public Text Lifes_text;

    //Puntos y vida de Spikey
    public int totalLifes = 10;
    public int life = 3;
    public int points = 0;

    //Animacion de las vidas
    public bool Iddle = false;
    public bool OneUp = false;
    public bool OneDown = false;

    //Booleanos para los puntos
    public bool bigCoin = false;
    public bool coin = false;
    public bool lifeUp = false;
    public bool lifeDown = false;

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
       
    }

    //Start is called before the first frame update
    void Start()
    {
        Point_text = GameObject.FindGameObjectWithTag("Points").GetComponent<Text>();
        Lifes_text = GameObject.FindGameObjectWithTag("Lifes").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (points % 100 == points)
        {
            lifeUp = true;
        }

        if (coin)
        {
            points++;
            coin = false;
        }
        else if (bigCoin)
        {
            points += 20;
            bigCoin = false;
        }
        else if (lifeUp)
        {
            if (life == 6)
            {
                points += 20;
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
            if (life == 0)
            {
                totalLifes--;
                lifeDown = false;
            }
            else if (life <= 6)
            {
                life--;
                OneDown = true;
                lifeDown = false;
            }
        }

        if (totalLifes == 0)
        {

        }

        Point_text.text = points.ToString();
        Lifes_text.text = points.ToString();
    }
}
