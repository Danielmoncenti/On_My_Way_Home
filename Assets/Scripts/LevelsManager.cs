using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsManager : MonoBehaviour
{
    public static LevelsManager Instance { get; private set; }

    public bool canLevel1;
    public bool canLevel2;
    public bool canLevel3;
    public bool canLevel4;
    public bool canLevel5;
    public bool canLevel6;

    private void Awake()
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


    // Start is called before the first frame update
    void Start()
    {
        canLevel1 = true;
        canLevel2 = false;
        canLevel3 = false;
        canLevel4 = false;
        canLevel5 = false;
        canLevel6 = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
