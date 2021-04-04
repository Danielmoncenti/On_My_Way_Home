﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerController : MonoBehaviour
{
    public static GameManagerController Instance { get; private set; }

    private Camera GameCamera;
    [SerializeField] GameObject Spikey;

    private SpikeyController spikeyController;
    //Camera camera;
    private Vector3 cameraPos;
    private float SpikeyY;


    //public SoundAudioClip[] SoundAudioClipArray;
    //[System.Serializable]
    //public class SoundAudioClip
    //{
    //    public SoundManager.Sound sound;
    //    public AudioClip audioClip;
    //}

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
        spikeyController = Spikey.GetComponent<SpikeyController>();
        GameCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        SpikeyY = Spikey.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (SpikeyY == SpikeyY + 50)
        {

        }
        cameraPos = new Vector3(Spikey.transform.position.x, Spikey.transform.position.y + 60, -10.0f);
        GameCamera.transform.position = cameraPos;
    }
}
