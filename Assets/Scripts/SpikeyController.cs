using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeyController : MonoBehaviour
{
    public enum Direction { NONE, UP, DOWN, RIGHT, LEFT };
    public Direction SpikeyDirection = Direction.NONE;
    public Direction Dashdirection = Direction.RIGHT;
    private float baseSpeed = 90.0f;
    public float maxSpeed = 130.0f;
    public float dashspeed;
    public bool shadowExists;
    public bool isdashing;
    public bool stucked;
    public float stuckedtimer = 0;
    public LayerMask LayerDashLimit;
    private float currentSpeedV = 0.0f;
    private float currentSpeedH = 0.0f;
    public float thrust = 25.0f;
    public int life = 5;
    private bool canWalk = true;
    public bool Jumping = true;
    public bool canClimb = false;
    public bool climbing = false;
    public bool takingdamage = false;
    public bool invulnerability = false;
    public float invulnerabilitytimer = 0.0f;
    public float damagetimer = 0.0f;
    private Vector2 lastCheckpoint;
    KeyCode upButton = KeyCode.W;
    KeyCode downButton = KeyCode.S;
    KeyCode rightButton = KeyCode.D;
    KeyCode leftButton = KeyCode.A;
    KeyCode spaceButton = KeyCode.Space;
    KeyCode attackButton = KeyCode.L;
    KeyCode dashButton = KeyCode.K;
    KeyCode shadowButton = KeyCode.Q;
    KeyCode sprintButton = KeyCode.J;
    KeyCode climbButton = KeyCode.H;

    private BoxCollider2D bc2d;
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
        bc2d = GetComponent<BoxCollider2D>();
        lastCheckpoint = rigidBody.transform.position;
        //audio = GetComponent<AudioSource>();        
        shadowExists = false;
        isdashing = false;
        stucked = false;
        Spikeyscale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        damagetimer += Time.deltaTime;
        invulnerabilitytimer += Time.deltaTime;
        if (takingdamage)
        {
            if (damagetimer >= 1)
            {
                takingdamage = false;
                
                damagetimer = 0;
            }
        }
        if (invulnerability)
        {
            if (invulnerabilitytimer >= 3)
            {
                gameObject.layer = 8;
                invulnerability = false;
            }
        }
     
        SpikeyDirection = Direction.NONE;
        if (takingdamage == false)
        {
            if (stucked == false)
            {


                if (isdashing == false)
                {
                    if (Input.GetKey(upButton) && climbing == true)
                    {
                        SpikeyDirection = Direction.UP;

                    }
                    else if (Input.GetKey(downButton) && climbing == true)
                    {

                        SpikeyDirection = Direction.DOWN;

                    }

                    if (Input.GetKey(rightButton) && climbing == false)
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
                    else if (Input.GetKey(leftButton) && climbing == false)
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

                    if (Input.GetKeyDown(spaceButton) && Jumping == false)
                    {
                        jump();
                    }
                    //if (Input.GetKeyDown(spaceButton) && Jumping == true && climbing==true)
                    //{
                    //    climbjump();
                    //}


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
                if (Input.GetKey(climbButton))
                {

                    if (canClimb == true && climbing == false)
                    {

                        climbing = true;

                        rigidBody.gravityScale = 0;
                    }

                }
                if (Input.GetKeyUp(climbButton))
                {

                    climbing = false;
                    rigidBody.gravityScale = 100;
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
        gameObject.layer = 8;
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

    private bool checkRaycastWithScenarioJump(RaycastHit2D[] hits)
    {
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.tag == "Tilemap" || hit.collider.gameObject.tag == "Wall" || hit.collider.gameObject.tag == "Crocodile"|| hit.collider.gameObject.tag == "Water") { return true; }
            }
        }
        return false;
    }
    private bool checkRaycastWithScenarioClimb(RaycastHit2D[] hits)
    {
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.tag == "Wall") { return true; }
            }
        }
        return false;
    }






    private void jump()
    {
         //rigidBody.transform.position.y = new Vector3(transform.position.x, transform.position.y + 5, 0);
       
        
            float delta = Time.fixedDeltaTime * 1000;
            rigidBody.AddForce(transform.up * thrust * delta, ForceMode2D.Impulse);
            Jumping = true;
        
    }
    //private void climbjump()
    //{
    //    //rigidBody.transform.position.y = new Vector3(transform.position.x, transform.position.y + 5, 0);

    //    climbing = false;
    //    float delta = Time.fixedDeltaTime * 1000;
    //    rigidBody.AddForce(new Vector2(400,400), ForceMode2D.Impulse);
    //    Jumping = true;
    //    rigidBody.gravityScale = 100;

    //}
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
            gameObject.layer = 12;
            shadowOndash = Instantiate(shadow, transform.position, transform.rotation);
            shadowcontroller = shadowOndash.GetComponent<ShadowController>();
            shadowcontroller.erizo = this.gameObject;
            if (Dashdirection == Direction.RIGHT)
            {
                float centery = (bc2d.bounds.min.y + bc2d.bounds.max.y) / 2;
                Vector2 positioncenter = new Vector2(bc2d.bounds.max.x, centery);
                Vector2 positionup = new Vector2(bc2d.bounds.max.x, bc2d.bounds.max.y);
                Vector2 positiondown = new Vector2(bc2d.bounds.max.x, bc2d.bounds.min.y);

                RaycastHit2D hitscenter = Physics2D.Raycast(positioncenter, Vector2.right, 200, LayerDashLimit);
                RaycastHit2D hitsup = Physics2D.Raycast(positionup, Vector2.right, 200, LayerDashLimit);
                RaycastHit2D hitsdown = Physics2D.Raycast(positiondown, Vector2.right, 200, LayerDashLimit);
                RaycastHit2D[] array = new RaycastHit2D[] {  hitsup, hitscenter, hitsdown };

                Vector2 LimitDashPosition = transform.position + transform.right * 200;
                for (int i = 0; i < 3; i++)
                {
                    if (array[i].collider != null && Vector2.Distance(array[i].point, this.transform.position) < Vector2.Distance(LimitDashPosition, this.transform.position))
                    {
                        LimitDashPosition = array[i].point;
                    }
                }
                limitdash = Instantiate(dashtrigger, LimitDashPosition, transform.rotation);

                rigidBody.velocity = new Vector2(dashspeed, 0);
                shadowExists = true;
                rigidBody.gravityScale = 0;
            }
            else if (Dashdirection == Direction.LEFT)
            {
                float centery = (bc2d.bounds.min.y + bc2d.bounds.max.y) / 2;
                Vector2 positioncenter = new Vector2(bc2d.bounds.min.x, centery);
                Vector2 positionup = new Vector2(bc2d.bounds.min.x, bc2d.bounds.max.y);
                Vector2 positiondown = new Vector2(bc2d.bounds.min.x, bc2d.bounds.min.y );

                RaycastHit2D hitscenter = Physics2D.Raycast(positioncenter, Vector2.right * -1, 200, LayerDashLimit);
                RaycastHit2D hitsup = Physics2D.Raycast(positionup, Vector2.right * -1, 200, LayerDashLimit);
                RaycastHit2D hitsdown = Physics2D.Raycast(positiondown, Vector2.right * -1, 200, LayerDashLimit);
                RaycastHit2D[] array = new RaycastHit2D[] {  hitsup, hitscenter, hitsdown };
                Vector2 LimitDashPosition = transform.position + transform.right * 200 * -1;
                for (int i = 0; i < 3; i++)
                {
                    if (array[i].collider != null && Vector2.Distance(array[i].point, this.transform.position) < Vector2.Distance(LimitDashPosition, this.transform.position))
                    {
                        LimitDashPosition = array[i].point;
                    }
                }

                


                limitdash = Instantiate(dashtrigger, LimitDashPosition, transform.rotation);
                rigidBody.velocity = new Vector2(dashspeed * -1, 0);
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
            rigidBody.velocity = new Vector2(0, 0);
            Destroy(shadowOndash);
            shadowExists = false;
        }
    }

    private void DamageTaken(Vector2 damageDirection)
    {
        if (takingdamage == false && invulnerability== false)
        {
            gameObject.layer = 14;
            damagetimer = 0.0f;
            invulnerabilitytimer = 0.0f;
            takingdamage = true;
            invulnerability = true;
            life--;
            
            if (damageDirection.x > 0)
            {
                rigidBody.velocity = new Vector2(0, 0);
                rigidBody.AddForce(new Vector2(300, 300),ForceMode2D.Impulse);
            }
            else
            {
                rigidBody.velocity = new Vector2(0, 0);
                rigidBody.AddForce(new Vector2(-300, 300),ForceMode2D.Impulse);
            }
        }
        if (life <= 0)
        {
            gameObject.layer = 8;
            rigidBody.transform.position = lastCheckpoint;
            life = 5;
        }

    }

    private void FixedUpdate()
    {
        float delta = Time.fixedDeltaTime * 1000;




        switch (SpikeyDirection)
        {
            default: break;
            case Direction.UP:
                rigidBody.velocity = new Vector2(0, 90);
                break;
            case Direction.DOWN:
                rigidBody.velocity = new Vector2(0, -90);
                break;
            case Direction.RIGHT:
                rigidBody.AddForce(transform.right * baseSpeed * delta);
                Spikeyscale.x = 1;
                break;
            case Direction.LEFT:
                rigidBody.AddForce((transform.right * baseSpeed * delta) * -1);
                Spikeyscale.x = -1;
                break;
            case Direction.NONE:
                if (climbing == true)
                {
                    rigidBody.velocity = new Vector2(0, 0);
                }
                break;
        }

    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Water")
        {
            maxSpeed = 80;
        }
        if (collision.gameObject.tag == "Tilemap" || collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Crocodile"|| collision.gameObject.tag == "Water")
        {
            if (Jumping == true)
            {
                bool col1 = false;
                bool col2 = false;
                bool col3 = false;
                float centerx = (bc2d.bounds.min.x + bc2d.bounds.max.x) / 2;
                Vector2 centerPosition = new Vector2(centerx, bc2d.bounds.min.y);
                Vector2 leftPosition = new Vector2(bc2d.bounds.min.x + 3, bc2d.bounds.min.y);
                Vector2 rightPosition = new Vector2(bc2d.bounds.max.x - 3, bc2d.bounds.min.y);

                RaycastHit2D[] hits = Physics2D.RaycastAll(centerPosition, -Vector2.up, 2);
                if (checkRaycastWithScenarioJump(hits)) { col1 = true; }
                hits = Physics2D.RaycastAll(leftPosition, -Vector2.up, 2);
                if (checkRaycastWithScenarioJump(hits)) { col2 = true; }
                hits = Physics2D.RaycastAll(rightPosition, -Vector2.up, 2);
                if (checkRaycastWithScenarioJump(hits)) { col3 = true; }

                if (col1 || col2 || col3) { Jumping = false; }

            }
        }
        if (collision.gameObject.tag == "Wall")
        {
            if (canClimb == false)
            {
                bool col = false;

                float centery = (bc2d.bounds.min.y + bc2d.bounds.max.y) / 2;
                Vector2 positionright = new Vector2(bc2d.bounds.max.x, centery);
                Vector2 positionleft = new Vector2(bc2d.bounds.min.x, centery);
                if (Dashdirection == Direction.LEFT)
                {
                    RaycastHit2D[] hits = Physics2D.RaycastAll(positionleft, -Vector2.right, 2);
                    if (checkRaycastWithScenarioClimb(hits)) { col = true; }
                }
                else if (Dashdirection == Direction.RIGHT)
                {
                    RaycastHit2D[] hits = Physics2D.RaycastAll(positionright, Vector2.right, 2);
                    if (checkRaycastWithScenarioClimb(hits)) { col = true; }
                }
                if (col) { canClimb = true; }
            }
        }


    }
    private void OnCollisionEnter2D(Collision2D collision)
    {


        if (collision.gameObject.tag == "Water")
        {
            maxSpeed = 80;
        }
        if (collision.gameObject.tag == "Enemies")
        {
            Vector2 damagedirection = this.transform.position-collision.transform.position;
            DamageTaken(damagedirection);
        }
      

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Dash")
        {
            cancelDash();
        }

        if (collision.gameObject.tag == "JawTrap")
        {
            cancelDash();
            rigidBody.velocity = Vector2.zero;
            stucked = true;

        }
        if (collision.gameObject.tag == "Enemies")
        {
            Vector2 damagedirection = this.transform.position - collision.transform.position;
            DamageTaken(damagedirection);
        }
        if (collision.gameObject.tag == "CheckPoint")
        {
            lastCheckpoint = collision.transform.position;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Tilemap")
        {
            Jumping = true;
        }
        if(collision.gameObject.tag == "Water")
        {
            Jumping = true;
            maxSpeed = 130;
        }
        if (collision.gameObject.tag == "Crocodile")
        {
            Jumping = true;
        }
        if (collision.gameObject.tag == "Wall")
        {
            Jumping = true;
            canClimb = false;
            if (climbing == true)
            {

                climbing = false;
                rigidBody.gravityScale = 100;

            }
        }
    }
}