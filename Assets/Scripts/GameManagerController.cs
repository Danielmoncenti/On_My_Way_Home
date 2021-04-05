using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;
using System.IO;

public class GameManagerController : MonoBehaviour
{
    public static GameManagerController Instance { get; private set; }

    private enum POINTS { COIN, BIGCOIN, LIFEUP };

    private GameObject Spikey;
    private Rigidbody2D Spikey_rigidBody;

    private GameObject BigCoin;
    private Rigidbody2D BigCoin_rigidBody;

    private GameObject Coin;
    private Rigidbody2D Coin_rigidBody;

    private GameObject LifeUp;
    private Rigidbody2D LifeUp_rigidBody;

    //Puntos y vida de Spikey
    public int life = 3;
    public int points = 0;

    //Booleanos para los puntos
    public bool bigCoin = false;
    public bool coin = false;
    public bool lifeUp = false;

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
        Spikey = GameObject.Find("Spikey");
        Spikey_rigidBody = Spikey.GetComponent<Rigidbody2D>();

        BigCoin = GameObject.Find("BigCoin");
        BigCoin_rigidBody = BigCoin.GetComponent<Rigidbody2D>();

        Coin = GameObject.Find("Coin");
        Coin_rigidBody = Coin.GetComponent<Rigidbody2D>();

        LifeUp = GameObject.Find("LifeUp");
        LifeUp_rigidBody = LifeUp.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
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
                lifeUp = false;
            }
       }
    }
}
