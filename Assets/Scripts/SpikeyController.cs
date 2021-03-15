using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeyController : MonoBehaviour
{
    public enum Direction { NONE, UP, DOWN, RIGHT, LEFT};
    public Direction SpikeyDirection = Direction.NONE;
    public Direction Dashdirection = Direction.RIGHT;
    private float baseSpeed = 90.0f;
    public float maxSpeed = 130.0f;
    public float dashspeed;
    public bool shadowExists;
    public bool isdashing;
    public bool stucked;
    public float stuckedtimer = 0;

    private float currentSpeedV = 0.0f;
    private float currentSpeedH = 0.0f;
    public float thrust = 25.0f;

    private bool canWalk = true;
    public bool canJump = false;
    private bool canClimb = false;

    KeyCode upButton = KeyCode.W;
    KeyCode downButton = KeyCode.S;
    KeyCode rightButton = KeyCode.D;
    KeyCode leftButton = KeyCode.A;
    KeyCode spaceButton = KeyCode.Space;
    KeyCode attackButton = KeyCode.L;
    KeyCode dashButton = KeyCode.K;
    KeyCode shadowButton = KeyCode.Q;
    KeyCode sprintButton = KeyCode.J;

    private Rigidbody2D rigidBody;
    private GameObject shadowOndash;
    private GameObject limitdash;
    //private AudioSource audio;

    public GameObject pua;
    public GameObject dashtrigger;
    public GameObject shadow;
    private ShadowController shadowcontroller;

    Vector3 Spikeyscale;


    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        //audio = GetComponent<AudioSource>();        
          shadowExists=false;
          isdashing = false;
        stucked = false;
        Spikeyscale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        
        SpikeyDirection = Direction.NONE;
        if (stucked == false)
        {
            if (isdashing == false)
            {
                if (Input.GetKeyDown(upButton) && Climb())
                {
                    SpikeyDirection = Direction.UP;

                }
                else if (Input.GetKeyDown(downButton) && Climb())
                {

                    SpikeyDirection = Direction.DOWN;

                }

                if (Input.GetKey(rightButton))
                {
                    //transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
                    currentSpeedH = rigidBody.velocity.x;
                    if (currentSpeedH > maxSpeed)
                    {
                        canWalk = false;
                    }
                    else
                    {
                        canWalk = true;
                    }

                    if (canWalk)
                    {
                        SpikeyDirection = Direction.RIGHT;
                        Dashdirection = Direction.RIGHT;
                    }
                }
                else if (Input.GetKey(leftButton))
                {
                    //transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
                    currentSpeedH = rigidBody.velocity.x;
                    if (currentSpeedH < maxSpeed * -1)
                    {
                        canWalk = false;
                    }
                    else
                    {
                        canWalk = true;
                    }

                    if (canWalk)
                    {
                        SpikeyDirection = Direction.LEFT;
                        Dashdirection = Direction.LEFT;

                    }
                }

                if (Input.GetKeyDown(spaceButton) && canJump)
                {
                    jump();
                }

                if (Input.GetKeyDown(attackButton))
                {
                    attack();
                }
             
                if (Input.GetKeyDown(shadowButton))
                {
                    if (shadowExists == true && isdashing == false)
                    {
                        shadowcontroller.startReturn = true;
                        
                    }
                }
            }

            if (Input.GetKeyDown(dashButton))
            {
                if (shadowExists == false)
                {
                    dash();

                }
                else if (shadowExists == true && isdashing == false)
                {
                    goToshadow();

                }
            }

        }
        
        else
        {
            stuckedtimer += Time.deltaTime * 1000;
            if (stuckedtimer >= 2000)
            {
                stucked = false;
                stuckedtimer = 0;
            }
        }
        if (Input.GetKey(sprintButton))
        {
            maxSpeed = 300.0f;
        }
        if (Input.GetKeyUp(sprintButton))
        {
            maxSpeed = 130.0f;
        }

        transform.localScale = Spikeyscale;
    }
    private void cancelDash()
    {
        isdashing = false;
        Destroy(limitdash);
        rigidBody.velocity = Vector2.zero;
        rigidBody.gravityScale = 100;
    }
  
  
    private bool Walk()
    {
        currentSpeedH = rigidBody.velocity.x;

        if (currentSpeedH > baseSpeed)
        {
            canWalk = false;
        }
        else
        {
            canWalk = true;
        }
        return canWalk;
    }

    private bool Climb()
    {
        currentSpeedV = rigidBody.velocity.y;

        if (currentSpeedV > baseSpeed)
        {
            canClimb = false;
        }
        else
        {
            canClimb = true;
        }
        return canClimb;
    }

    private void jump()
    {
        // rigidBody.transform.position.y = new Vector3(transform.position.x, transform.position.y + 5, 0);
        float delta = Time.fixedDeltaTime * 1000;
        rigidBody.AddForce(transform.up * thrust * delta, ForceMode2D.Impulse);
        canJump = false;

    }
    private void attack()
    {
        GameObject Pua1 = Instantiate(pua, transform.position + transform.up * 2, Quaternion.Euler(0, 0, 0));
        GameObject Pua2 = Instantiate(pua, transform.position + transform.up * 2, Quaternion.Euler(0, 0, 45));
        GameObject Pua3 = Instantiate(pua, transform.position + transform.up * 2, Quaternion.Euler(0, 0, 90));
        GameObject Pua4 = Instantiate(pua, transform.position + transform.up * 2, Quaternion.Euler(0, 0, 135));
        GameObject Pua5 = Instantiate(pua, transform.position + transform.up * 2, Quaternion.Euler(0, 0, 180));
        GameObject Pua6 = Instantiate(pua, transform.position + transform.up * 2, Quaternion.Euler(0, 0, 225));
        GameObject Pua7 = Instantiate(pua, transform.position + transform.up * 2, Quaternion.Euler(0, 0, 270));
        GameObject Pua8 = Instantiate(pua, transform.position + transform.up * 2, Quaternion.Euler(0, 0, 315));
        GameObject Pua9 = Instantiate(pua, transform.position + transform.up * 2, Quaternion.Euler(0, 0, 360));

        Destroy(Pua1, 3);
        Destroy(Pua2, 3);
        Destroy(Pua3, 3);
        Destroy(Pua4, 3);
        Destroy(Pua5, 3);
        Destroy(Pua6, 3);
        Destroy(Pua7, 3);
        Destroy(Pua8, 3);
        Destroy(Pua9, 3);
    }

    private void dash()
    {
        if (isdashing == false)
        {
            isdashing = true;
             shadowOndash= Instantiate(shadow, transform.position, transform.rotation);
            shadowcontroller = shadowOndash.GetComponent<ShadowController>();
            shadowcontroller.erizo = this.gameObject;
            if (Dashdirection == Direction.RIGHT)
            {
                limitdash = Instantiate(dashtrigger, transform.position + transform.right * 200, transform.rotation);
                rigidBody.velocity = new Vector2(dashspeed, 0);
                shadowExists = true;
                rigidBody.gravityScale = 0;
            }
            else if (Dashdirection == Direction.LEFT)
            {
                limitdash = Instantiate(dashtrigger, transform.position + transform.right * 200*-1, transform.rotation);
                rigidBody.velocity = new Vector2(dashspeed*-1, 0);
                shadowExists = true;
                rigidBody.gravityScale = 0;
            }
        }
    


    }
    
    private void goToshadow()
    {
        if (shadowExists == true)
        {
            rigidBody.transform.position = shadowOndash.transform.position;
            Destroy(shadowOndash);
            shadowExists = false;
        }
    }

    private void FixedUpdate()
    {
        float delta = Time.fixedDeltaTime * 1000;

     
        
        
            switch (SpikeyDirection)
            {
                default: break;
                case Direction.UP:
                    rigidBody.AddForce(transform.up * baseSpeed * delta);
                    break;
                case Direction.DOWN:
                    rigidBody.AddForce((transform.up * baseSpeed * delta) * -1);
                    break;
                case Direction.RIGHT:
                    rigidBody.AddForce(transform.right * baseSpeed * delta);
                    Spikeyscale.x = 1;
                    break;
                case Direction.LEFT:
                    rigidBody.AddForce((transform.right * baseSpeed * delta) * -1);
                    Spikeyscale.x = -1;
                    break;
            }
        
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Tilemap")
        {
            canJump = true;
        }

        if (collision.gameObject.tag == "Nombre del gameobject qe permite escalar")
        {
            canClimb = true;
        }

        if (collision.gameObject.tag == "Wall" && isdashing == true)
        {
            cancelDash();
        }
        if (collision.gameObject.tag == "Rat")
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Mole")
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Thorns")
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Bat")
        {
            Destroy(gameObject);
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Dash" )
        {
            cancelDash();
        }

        if (collision.gameObject.tag == "JawTrap")
        {
            cancelDash();
            rigidBody.velocity = Vector2.zero;
            stucked = true;
            
        }
        if (collision.gameObject.tag == "Rock")
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Thorns")
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Cristal")
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Gout")
        {
            Destroy(gameObject);
        }
    }
}
