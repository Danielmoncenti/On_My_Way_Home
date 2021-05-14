using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour {

    public static SoundManager Instance { get; private set; }

    public GameObject Hijo;
    public static AudioClip Song, SongGame, PlayerJump, PlayerDash, PlayerRevert, PlayerRun, PlayerDamage, PlayerSpike, Rat, Bat, Drop, Trap, Crocodile, ClickMenu, Checkpoint, Button, Cristal, CrossBow, BoundTrap, Water, Coin;
    static AudioSource audiosrc;
    static AudioSource audiosrc2;

    Scene currentScene;
    string sceneName;

    private float musicVolume = 0.1f;
    private float sfxVolume = 0.1f;

    //Comprobar para activar sonidos
    /*private int radius = 500;
    Ballista _Ballista;
    private bool b_ballista = false;
    BatController _Bat;
    private bool b_bat = false;
    BoundTrapController _BoundTrap;
    private bool b_boundtrap = false;
    BushController _Bush;
    private bool b_bush = false;
    ButtonController _Button;
    private bool b_button = false;
    CrocodileController _Crocodile;
    private bool b_crocodile = false;
    DropController _Drop;
    private bool b_drop = false;
    FallingPlataform _FallingPlataform;
    private bool b_fallinplataform = false;
    //GameObject Pajaro;
    MoloController _Mole;
    private bool b_mole = false;
    MushroomController _Mushroom;
    private bool b_mushroom = false;
    RatController _Rat;
    private bool b_rat = false;
    RockController _Rock;
    private bool b_rock = false;
    RotateTrapController _RotateTrap;
    private bool b_rotatetrap = false;
    Spawner _Spawner;
    private bool b_spawner = false;
    ThormsController _Thorms;
    private bool b_thorms = false;
    TrapController _Trap; 
    private bool b_trap = false;

    Vector2 distance;*/

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

    // Start is called before the first frame update
    void Start()
    {
        Song = Resources.Load<AudioClip> ("Song");
        SongGame = Resources.Load<AudioClip> ("SongGame");
        PlayerJump = Resources.Load<AudioClip> ("Jump");
        PlayerDash = Resources.Load<AudioClip>("Dash");
        PlayerRevert = Resources.Load<AudioClip>("DashRevert");
        PlayerDamage = Resources.Load<AudioClip>("Damage");
        Rat = Resources.Load<AudioClip>("RatChase");
        Bat = Resources.Load<AudioClip>("BatChase");
        Drop = Resources.Load<AudioClip>("Drop");
        Trap = Resources.Load<AudioClip>("Trap");
        ClickMenu = Resources.Load<AudioClip>("ClickMenu");
        Checkpoint = Resources.Load<AudioClip>("Checkpoint");
        Coin = Resources.Load<AudioClip>("Coin");

        BoundTrap = Resources.Load<AudioClip>("BoundTrap"); // AL FINAL NO SE USARA?
        CrossBow = Resources.Load<AudioClip>("Crossbow"); // AL FINAL NO SE USARA?

        //Crocodile = Resources.Load<AudioClip>("Crocodile"); HAY QUE
        
        // NO USADOS / IMPLEMENTADOS
        //PlayerSpike = Resources.Load<AudioClip>("Spikes");
        Cristal = Resources.Load<AudioClip>("Cristal");
        Button = Resources.Load<AudioClip>("Button");
        PlayerRun = Resources.Load<AudioClip> ("Step");
        Water = Resources.Load<AudioClip>("Water");

        audiosrc = GetComponent<AudioSource>();
        audiosrc2 = Hijo.GetComponent<AudioSource>();

        /*_Ballista = GetComponent<Ballista>();
        _Bat = GetComponent<BatController>();
        _BoundTrap = GetComponent<BoundTrapController>();
        _Bush = GetComponent<BushController>();
        _Button = GetComponent<ButtonController>();
        _Crocodile = GetComponent<CrocodileController>();
        _Drop = GetComponent<DropController>();
        _FallingPlataform = GetComponent<FallingPlataform>();
        //Pajaro = GameObject.FindGameObjectsWithTag("").GetComponent<ButtonController>();
        _Mole = GetComponent<MoloController>();
        _Mushroom = GetComponent<MushroomController>();
        _Rat = GetComponent<RatController>();
        _Rock = GetComponent<RockController>();
        _RotateTrap = GetComponent<RotateTrapController>();
        _Spawner = GetComponent<Spawner>();
        _Thorms = GetComponent<ThormsController>();
        _Trap = GetComponent<TrapController>();*/        

    }
    // Update is called once per frame
    void Update()
    {
        //CheckCollisions();
        if (!audiosrc.isPlaying) {
            currentScene = SceneManager.GetActiveScene();
            sceneName = currentScene.name;
            if ((sceneName == "MainMenu" || sceneName == "GameOver" || sceneName == "Win" || sceneName == "Start"))
            {
                PlaySound("Song");
            }
            else
            {
                PlaySound("SongGame");
            }
        }

        audiosrc.volume = musicVolume;

        audiosrc2.volume = sfxVolume;
    }

    public void SetVolumeMusic(float vol)
    {
        musicVolume = vol;
    }
    public void SetVolumeSFX(float vol)
    {
        sfxVolume = vol;
    }

    /*private void CheckCollisions()
    {
       
    }*/
    public static void StopSound () { audiosrc.Stop(); }
    public static void PlaySound (string clip)
    {
        switch (clip)
        {
            case "Song":
                audiosrc.PlayOneShot(Song);
                break;
            case "SongGame":
                audiosrc.PlayOneShot(SongGame);
                break;
            case "Jump":
                audiosrc2.PlayOneShot(PlayerJump);
                break;
            case "Dash":
                audiosrc2.PlayOneShot(PlayerDash);
                break;
            case "DashRevert":
                audiosrc2.PlayOneShot(PlayerRevert);
                break;
            case "Run":
                audiosrc2.PlayOneShot(PlayerRun);
                break;
            case "Damage":
                audiosrc2.PlayOneShot(PlayerDamage);
                break;
            case "Spikes":
                audiosrc2.PlayOneShot(PlayerSpike);
                break;
            case "RatChase":
                audiosrc2.PlayOneShot(Rat);
                break;
            case "BatChase":
                audiosrc2.PlayOneShot(Bat);
                break;
            //case "Drop":
                //audiosrc2.PlayOneShot(Drop);
               // break;
            case "Trap":
                audiosrc2.PlayOneShot(Trap);
                break;
            //case "Crocodile":
                //audiosrc2.PlayOneShot(Crocodile);
                //break;
            case "ClickMenu":
                audiosrc2.PlayOneShot(ClickMenu);
                break;
            case "Checkpoint":
                audiosrc2.PlayOneShot(Checkpoint);
                break;
            case "Button":
                audiosrc2.PlayOneShot(Button);
                break;
            case "Cristal":
                audiosrc2.PlayOneShot(Cristal);
                break;
            case "Crossbow":
                audiosrc2.PlayOneShot(CrossBow);
                break;
            case "BoundTrap":
                audiosrc2.PlayOneShot(BoundTrap);
                break;
            case "Water":
                audiosrc2.PlayOneShot(Water);
                break;
        }
    }
}