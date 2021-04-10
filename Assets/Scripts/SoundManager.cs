using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour {

    public static SoundManager Instance { get; private set; }

    public static AudioClip Song, SongGame, PlayerJump, PlayerDash, PlayerRevert, PlayerRun, PlayerDamage, PlayerSpike, Rat, Bat, Drop, Trap, Crocodile, ClickMenu, Checkpoint, Button, Cristal, CrossBow, BoundTrap, Water;
    static AudioSource audiosrc;

    Scene currentScene;
    string sceneName;

    private float introSongTimer;
    private float gameSongTimer;
    private float introSong;
    private float gameSong;

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
        PlayerRun = Resources.Load<AudioClip> ("Step");
        PlayerDamage = Resources.Load<AudioClip>("Daño");
        //PlayerSpike = Resources.Load<AudioClip>("Spikes");
        Rat = Resources.Load<AudioClip>("Rat");
        Bat = Resources.Load<AudioClip>("Bat");
        Drop = Resources.Load<AudioClip>("Drop");
        Trap = Resources.Load<AudioClip>("Trap");
        Crocodile = Resources.Load<AudioClip>("Roar");
        ClickMenu = Resources.Load<AudioClip>("ClickMenu");
        Checkpoint = Resources.Load<AudioClip>("Checkpoint");
        Button = Resources.Load<AudioClip>("Button");
        Cristal = Resources.Load<AudioClip>("Cristal");
        CrossBow = Resources.Load<AudioClip>("Crossbow");
        BoundTrap = Resources.Load<AudioClip>("BoundTrap");
        Water = Resources.Load<AudioClip>("Water");

        audiosrc = GetComponent<AudioSource>();

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

        introSong = Song.length;
        gameSong = SongGame.length;
    }
    // Update is called once per frame
    void Update()
    {
        //CheckCollisions();
        if (!audiosrc.isPlaying) {
            currentScene = SceneManager.GetActiveScene();
            sceneName = currentScene.name;
            if ((sceneName == "MainMenu" || sceneName == "GameOver" || sceneName == "Win"))
            {
                PlaySound("Song");
            }
            else
            {
                PlaySound("SongGame");
            }
        }

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
                audiosrc.PlayOneShot(PlayerJump);
                break;
            case "Dash":
                audiosrc.PlayOneShot(PlayerDash);
                break;
            case "DashRevert":
                audiosrc.PlayOneShot(PlayerRevert);
                break;
            case "Run":
                audiosrc.PlayOneShot(PlayerRun);
                break;
            case "Daño":
                audiosrc.PlayOneShot(PlayerDamage);
                break;
            case "Spikes":
                audiosrc.PlayOneShot(PlayerSpike);
                break;
            case "Rat":
                audiosrc.PlayOneShot(Rat);
                break;
            case "Bat":
                audiosrc.PlayOneShot(Bat);
                break;
            //case "Drop":
                //audiosrc.PlayOneShot(Drop);
               // break;
            case "Trap":
                audiosrc.PlayOneShot(Trap);
                break;
            //case "Roar":
                //audiosrc.PlayOneShot(Crocodile);
                //break;
            case "ClickMenu":
                audiosrc.PlayOneShot(ClickMenu);
                break;
            case "Checkpoint":
                audiosrc.PlayOneShot(Checkpoint);
                break;
            case "Button":
                audiosrc.PlayOneShot(Button);
                break;
            case "Cristal":
                audiosrc.PlayOneShot(Cristal);
                break;
            case "Crossbow":
                audiosrc.PlayOneShot(CrossBow);
                break;
            case "BoundTrap":
                audiosrc.PlayOneShot(BoundTrap);
                break;
            case "Water":
                audiosrc.PlayOneShot(Water);
                break;
        }
    }
}