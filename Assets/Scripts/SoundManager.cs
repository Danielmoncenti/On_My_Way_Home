using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour {

    public static AudioClip Song, SongGame, PlayerJump, PlayerDash, PlayerRevert, PlayerRun, PlayerDamage, PlayerSpike, Rat, Bat, Drop, Trap, Crocodile, ClickMenu, Checkpoint, Button, Cristal, CrossBow, BoundTrap, Water;
    static AudioSource audiosrc;

    Scene currentScene;
    string sceneName;

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

        currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;

        if (sceneName == "MainMenu" || sceneName == "GameOver") {
            PlaySound("Song");
        } else {
            PlaySound("SongGame");
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }

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
            case "Drop":
                audiosrc.PlayOneShot(Drop);
                break;
            case "Trap":
                audiosrc.PlayOneShot(Trap);
                break;
            case "Roar":
                audiosrc.PlayOneShot(Crocodile);
                break;
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