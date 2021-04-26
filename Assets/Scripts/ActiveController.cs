using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveController : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    Ballista Ballista;
    BatController Bat;
    BoundTrapController BoundTrap;
    BushController Bush;
    ButtonController Button;
    CrocodileController Crocodile;
    DropController Drop;
    FallingPlataform FallingPlataform;
    //GameObject Pajaro;
    MoloController Mole;
    MushroomController Mushroom;
    RatController Rat;
    RockController Rock;
    RotateTrapController RotateTrap;
    Spawner Spawner;
    SpikesController Spikes;
    ThormsController Thorms;
    TrapController Trap;
    

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        Ballista = GetComponent<Ballista>();
        Bat = GetComponent<BatController>();
        BoundTrap = GetComponent<BoundTrapController>();
        Bush = GetComponent<BushController>();
        Button = GetComponent<ButtonController>();
        Crocodile = GetComponent<CrocodileController>();
        Drop = GetComponent<DropController>();
        FallingPlataform = GetComponent<FallingPlataform>();
        //Pajaro = GameObject.FindGameObjectsWithTag("").GetComponent<ButtonController>();
        Mole = GetComponent<MoloController>();
        Mushroom = GetComponent<MushroomController>();
        Rat = GetComponent<RatController>();
        Rock = GetComponent<RockController>();
        RotateTrap = GetComponent<RotateTrapController>();
        Spawner = GetComponent<Spawner>();
        Spikes = GetComponent<SpikesController>();
        Thorms = GetComponent<ThormsController>();
        Trap = GetComponent<TrapController>();

        spriteRenderer.enabled = false;
        //Ballista.enabled = false;
        Bat.enabled = false;
        BoundTrap.enabled = false;
        Bush.enabled = false;
        Button.enabled = false;
        Crocodile.enabled = false;
        Drop.enabled = false;
        FallingPlataform.enabled = false;
        //Pajaro.enabled = false;
        Mole.enabled = false;
        Mushroom.enabled = false;
        Rat.enabled = false;
        Rock.enabled = false;
        RotateTrap.enabled = false;
        Spawner.enabled = false;
        Spikes.enabled = false;
        Thorms.enabled = false;
        Trap.enabled = false;


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemies")
        {
            spriteRenderer.enabled = true;
            collision.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Active")
        {
            gameObject.SetActive(false);
        }
    }
}
