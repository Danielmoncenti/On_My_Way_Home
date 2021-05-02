using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeyController : MonoBehaviour
{
    public enum Direction { NONE, UP, DOWN, RIGHT, LEFT };
    public Direction SpikeyDirection = Direction.NONE;
    public Direction FacingDirection = Direction.RIGHT;
    public Direction VerticalDashDirection = Direction.NONE;

    public bool basicDashActivated;
    //public bool shadowDashActivated;
    //public bool doubleDashActivated;
    private bool sprinting;
    public float dashCD = 0;
    private float baseSpeed = 320.0f;
    public float sprintSpeed = 450.0f;
    private float currentSpeedV;
    public float dashspeed;
    public bool shadowExists;
    public bool dashing;
    public bool stucked;
    public float stuckedtimer = 0;
    public LayerMask LayerDashLimit;
    private float currentSpeedH = 0.0f;
    [SerializeField] float thrust = 35.0f;
    public int life = 5;
    private bool canWalk = true;
    public bool Jumping = true;
    public bool canClimb = true;
    public bool canAttack = true;
    public float cdattack = 0;
   
    public bool climbing = false;
    public bool takingdamage = false;
    public bool invulnerability = false;
    public float invulnerabilitytimer = 0.0f;
    public float damagetimer = 0.0f;
    private Vector2 lastCheckpoint;

    //Inputs del teclado
    KeyCode upButton = KeyCode.W;
    KeyCode downButton = KeyCode.S;
    KeyCode rightButton = KeyCode.D;
    KeyCode leftButton = KeyCode.A;
    KeyCode spaceButton = KeyCode.Space;
    KeyCode attackButton = KeyCode.L;
    KeyCode dashButton = KeyCode.K;
    //KeyCode shadowButton = KeyCode.Q;
    KeyCode sprintButton = KeyCode.J;
    KeyCode respawnButton = KeyCode.Z;


    //Get Components
    private BoxCollider2D bc2d;
    private Rigidbody2D rigidBody;
    private GameObject shadowOndash;
    private GameObject limitdash;

    //Get Components de fuera del script
    public GameObject pua;
    public GameObject dashtrigger;
    public GameObject shadow;
    public GameObject movingPlataform;
    [SerializeField] CD_AttackController cd_attack;
    [SerializeField] GameObject obj_cd_attack;
    [SerializeField] CD_DashController cd_dash;
    [SerializeField] GameObject obj_cd_dash;
    //private ShadowController shadowcontroller;

    //Para que los enemigos hagan la animacion de muerte cuando mueren con un dash
    public bool killedByDash = false;

    //Animaciones
    private Animator animator;
    bool isWalking = false;
    bool isRunning = false;
    bool isJumping = false;
    bool isClimbing = false;
    bool isHurt = false;
    bool isDashing = false;
    bool isAttacking = false;

    Vector3 Spikeyscale;

    // Start is called before the first frame update
    void Start()
    {
        basicDashActivated = true;
        //shadowDashActivated = true;
        //doubleDashActivated = false;

        rigidBody = GetComponent<Rigidbody2D>();
        bc2d = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        lastCheckpoint = rigidBody.transform.position;
        //audio = GetComponent<AudioSource>();        
        shadowExists = false;
        dashing = false;
        stucked = false;
        obj_cd_attack.SetActive(false);
        obj_cd_dash.SetActive(false);
        Spikeyscale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        float delta = Time.deltaTime * 1000;
        damagetimer += Time.deltaTime;
        invulnerabilitytimer += Time.deltaTime;

        if (rigidBody.velocity.y > 1000)
        {
         rigidBody.velocity = new Vector2(currentSpeedH, 1000);
        }
        if (rigidBody.velocity.y < -600)
        {
            rigidBody.velocity = new Vector2(currentSpeedH, -600);
        }
       
        if (dashCD > 0)
        {
            dashCD -= delta;
        }
        else if (dashCD <= 0)
        {
            dashCD = 0;
        }

        isWalking = false;
        isRunning = false;
        isJumping = false;
        isClimbing = false;
        isHurt = false;
        isDashing = false;
        isAttacking = false;

        if (Input.GetKeyDown(respawnButton))
        {
            this.transform.position = lastCheckpoint;
        }
     
        if (takingdamage)
        {
            //no se cumple la animacion porq se siguen aceptando los inputs
            isWalking = false;
            isRunning = false;
            isJumping = false;
            isClimbing = false;
            isHurt = true;
            isDashing = false;
            isAttacking = false;
            if (damagetimer >= 0.5)
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

        
     
        if (takingdamage == false)
        {
            if (stucked == false)
            {
                if (dashing == false)
                {
                    SpikeyDirection = Direction.NONE;
                    VerticalDashDirection = Direction.NONE;
                    killedByDash = false;
                    if (climbing)
                    {
                        isClimbing = true;
                        if (Input.GetKey(upButton))
                        {
                            VerticalDashDirection = Direction.UP;
                            SpikeyDirection = Direction.UP;

                        }
                        else if (Input.GetKey(downButton))
                        {
                            SpikeyDirection = Direction.DOWN;

                        }
                    }
          
                    if (Input.GetKey(upButton))
                    {
                        VerticalDashDirection = Direction.UP;


                    }

                    if (Input.GetKey(rightButton) && climbing == false)
                    {
                        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
                        currentSpeedH = rigidBody.velocity.x;
                        //if (currentSpeedH > sprintSpeed)
                        //{
                        //    canWalk = false;
                        //}
                        //else
                        //{
                        //    canWalk = true;
                        //}

                  
                            SpikeyDirection = Direction.RIGHT;
                            FacingDirection = Direction.RIGHT;
                        
                        isWalking = true;
                        isAttacking = false;
                    }
                    else if (Input.GetKey(leftButton) && climbing == false)
                    {
                        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
                        currentSpeedH = rigidBody.velocity.x;
                        //if (currentSpeedH < sprintSpeed * -1)
                        //{
                        //    canWalk = false;
                        //}
                        //else
                        //{
                        //    canWalk = true;
                        //}

                        if (canWalk)
                        {
                            SpikeyDirection = Direction.LEFT;
                            FacingDirection = Direction.LEFT;
                        }
                        isWalking = true;
                        isAttacking = false;
                    }

                    if (Input.GetKeyDown(spaceButton) && Jumping == false)
                    {
                        jump();
                    }
                    //if (Input.GetKeyDown(spaceButton) && Jumping == true && climbing==true)
                    //{
                    //    climbjump();
                    //}

                    if (Jumping)
                    {
                        isWalking = false;
                        isRunning = false;
                        isJumping = true;
                    }

                    if (!canAttack)
                    {
                        cdattack += delta;
                        if (cdattack > 5000)
                        {
                            canAttack = true;
                            cdattack = 0;
                        }
                    }
                    
                    if (Input.GetKey(attackButton) && canAttack)
                    {
                        isAttacking = true;
                    }

                    //if (Input.GetKeyDown(shadowButton))
                    //{
                    //    if (shadowExists == true && dashing == false)
                    //    {
                    //        shadowcontroller.startReturn = true;
                    //        dashCD = 10000;
                    //        SoundManager.PlaySound("DashRevert");
                    //    }
                    //}
                }
                else
                {
                    isDashing = true;
                    killedByDash = true;
                }

                if (Input.GetKeyDown(dashButton))
                {
                    /*if (doubleDashActivated)
                    //{
                    //    if (dashCD <= 5000)
                    //    {
                    //        isDashing = true;
                    //        doubleDash();
                    //    }
                    }*/
                    if (dashCD <= 0)
                    {
                        if (basicDashActivated)
                        {
                            isDashing = true;
                            basicDash();
                            obj_cd_dash.SetActive(true);
                            cd_dash.isUsed = true;
                        }
                        //else if (shadowDashActivated)
                        //{
                        //    if (shadowExists == false)
                        //    {
                        //        isDashing = true;
                        //        shadowdash();

                        //    }
                        //    else if (shadowExists == true && dashing == false)
                        //    {
                        //        goToshadow();
                        //    }
                        //}
                    }
                }

                if (!cd_attack.isUsed) obj_cd_attack.SetActive(false);
                if (!cd_dash.isUsed) obj_cd_dash.SetActive(false);
            }
            else
            {
                isHurt = true;
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
            if (rigidBody.velocity == new Vector2(0, 0))
            {
                isRunning = false;
            }
            else if (isDashing || Jumping || isClimbing)
            {
                isRunning = false;
            }
            else
            {
                isWalking = false;
                isRunning = true;
                sprinting = true;
            }

        }
        if (Input.GetKeyUp(sprintButton) || Jumping ==true)
        {
            sprinting = false;
        }

        transform.localScale = Spikeyscale;

        animator.SetBool("isWalking", isWalking);
        animator.SetBool("isRunning", isRunning);
        animator.SetBool("isJumping", isJumping);
        animator.SetBool("isDashing", isDashing);
        animator.SetBool("isHurt", isHurt);
        animator.SetBool("isClimbing", isClimbing);
        animator.SetBool("isAttacking", isAttacking);
    }

    private void Respawn()
    {
        gameObject.layer = 8;
        rigidBody.transform.position = lastCheckpoint;
        rigidBody.velocity = new Vector2(0, 0);

        life = 5;
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
                if (hit.collider.gameObject.tag == "Tilemap" || hit.collider.gameObject.tag == "Wall" || hit.collider.gameObject.tag == "Crocodile" || hit.collider.gameObject.tag == "Water") { return true; }
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

        if (climbing == false)
        {
            Jumping = true;
            float delta = Time.fixedDeltaTime * 1000;
            rigidBody.AddForce(transform.up * thrust * delta, ForceMode2D.Impulse);
            //rigidBody.velocity = rigidBody.velocity + Vector2.up * thrust;
            
        }
        else 
        {
            float delta = Time.fixedDeltaTime * 1000;
            climbing = false;
            if (FacingDirection == Direction.LEFT)
            {
                Jumping = true;
                FacingDirection = Direction.RIGHT;
                rigidBody.AddForce((new Vector2(30 * delta, 5 * delta)), ForceMode2D.Impulse);

               

            }
            else if (FacingDirection == Direction.RIGHT)
            {
                Jumping = true;
                FacingDirection = Direction.LEFT;
                rigidBody.AddForce((new Vector2(30 * delta * -1, 5 * delta)), ForceMode2D.Impulse);

                

            }
        }
        SoundManager.PlaySound("Jump");
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
        
        obj_cd_attack.SetActive(true);
        cd_attack.isUsed = true;
        canAttack = false;
        
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

    //private void shadowdash()
    //{
    //    if (dashing == false)
    //    {
    //        if (VerticalDashDirection == Direction.NONE)
    //        {
    //            dashing = true;
    //            gameObject.layer = 12;
    //            shadowOndash = Instantiate(shadow, transform.position, transform.rotation);
    //            shadowcontroller = shadowOndash.GetComponent<ShadowController>();
    //            shadowcontroller.erizo = this.gameObject;
    //            if (FacingDirection == Direction.RIGHT)
    //            {
    //                float centery = (bc2d.bounds.min.y + bc2d.bounds.max.y) / 2;
    //                Vector2 positioncenter = new Vector2(bc2d.bounds.max.x, centery);
    //                Vector2 positionup = new Vector2(bc2d.bounds.max.x, bc2d.bounds.max.y);
    //                Vector2 positiondown = new Vector2(bc2d.bounds.max.x, bc2d.bounds.min.y);

    //                RaycastHit2D hitscenter = Physics2D.Raycast(positioncenter, Vector2.right, 300, LayerDashLimit);
    //                RaycastHit2D hitsup = Physics2D.Raycast(positionup, Vector2.right, 300, LayerDashLimit);
    //                RaycastHit2D hitsdown = Physics2D.Raycast(positiondown, Vector2.right, 300, LayerDashLimit);
    //                RaycastHit2D[] array = new RaycastHit2D[] { hitsup, hitscenter, hitsdown };

    //                Vector2 LimitDashPosition = transform.position + transform.right * 300;
    //                for (int i = 0; i < 3; i++)
    //                {
    //                    if (array[i].collider != null && Vector2.Distance(array[i].point, this.transform.position) < Vector2.Distance(LimitDashPosition, this.transform.position))
    //                    {
    //                        LimitDashPosition = array[i].point;
    //                    }
    //                }
    //                limitdash = Instantiate(dashtrigger, LimitDashPosition, transform.rotation);

    //                rigidBody.velocity = new Vector2(dashspeed, 0);
    //                shadowExists = true;
    //                rigidBody.gravityScale = 0;
    //                dashCD = 10000;
    //            }
    //            else if (FacingDirection == Direction.LEFT)
    //            {
    //                float centery = (bc2d.bounds.min.y + bc2d.bounds.max.y) / 2;
    //                Vector2 positioncenter = new Vector2(bc2d.bounds.min.x, centery);
    //                Vector2 positionup = new Vector2(bc2d.bounds.min.x, bc2d.bounds.max.y);
    //                Vector2 positiondown = new Vector2(bc2d.bounds.min.x, bc2d.bounds.min.y);

    //                RaycastHit2D hitscenter = Physics2D.Raycast(positioncenter, Vector2.right * -1, 300, LayerDashLimit);
    //                RaycastHit2D hitsup = Physics2D.Raycast(positionup, Vector2.right * -1, 300, LayerDashLimit);
    //                RaycastHit2D hitsdown = Physics2D.Raycast(positiondown, Vector2.right * -1, 300, LayerDashLimit);
    //                RaycastHit2D[] array = new RaycastHit2D[] { hitsup, hitscenter, hitsdown };
    //                Vector2 LimitDashPosition = transform.position + transform.right * 300 * -1;
    //                for (int i = 0; i < 3; i++)
    //                {
    //                    if (array[i].collider != null && Vector2.Distance(array[i].point, this.transform.position) < Vector2.Distance(LimitDashPosition, this.transform.position))
    //                    {
    //                        LimitDashPosition = array[i].point;
    //                    }
    //                }





    //                limitdash = Instantiate(dashtrigger, LimitDashPosition, transform.rotation);
    //                rigidBody.velocity = new Vector2(dashspeed * -1, 0);
    //                shadowExists = true;
    //                rigidBody.gravityScale = 0;
    //                dashCD = 10000;

    //            }
    //        }
    //        else if (VerticalDashDirection == Direction.UP)
    //        {
    //            dashing = true;
    //            gameObject.layer = 12;
    //            shadowOndash = Instantiate(shadow, transform.position, transform.rotation);
    //            shadowcontroller = shadowOndash.GetComponent<ShadowController>();
    //            shadowcontroller.erizo = this.gameObject;

    //            float center = (bc2d.bounds.min.y + bc2d.bounds.max.y) / 2;
    //            Vector2 right = new Vector2(bc2d.bounds.max.x, bc2d.bounds.max.y);
    //            Vector2 poscenter = new Vector2(bc2d.bounds.center.x, bc2d.bounds.max.y);
    //            Vector2 left = new Vector2(bc2d.bounds.min.x, bc2d.bounds.max.y);

    //            RaycastHit2D hitscenter = Physics2D.Raycast(right, Vector2.up, 300, LayerDashLimit);
    //            RaycastHit2D hitsup = Physics2D.Raycast(poscenter, Vector2.up, 300, LayerDashLimit);
    //            RaycastHit2D hitsdown = Physics2D.Raycast(left, Vector2.up, 300, LayerDashLimit);
    //            RaycastHit2D[] array = new RaycastHit2D[] { hitsup, hitscenter, hitsdown };

    //            Vector2 LimitDashPosition = transform.position + transform.up * 300;
    //            for (int i = 0; i < 3; i++)
    //            {
    //                if (array[i].collider != null && Vector2.Distance(array[i].point, this.transform.position) < Vector2.Distance(LimitDashPosition, this.transform.position))
    //                {
    //                    LimitDashPosition = array[i].point;
    //                }
    //            }
    //            limitdash = Instantiate(dashtrigger, LimitDashPosition, transform.rotation);

    //            rigidBody.velocity = new Vector2(0, dashspeed);
    //            shadowExists = true;
    //            rigidBody.gravityScale = 0;
    //            dashCD = 10000;
    //        }

            
    //    }

    //    SoundManager.PlaySound("DashRevert");
    //}


    //private void goToshadow()
    //{
    //    if (shadowExists == true)
    //    {
            
    //        rigidBody.transform.position = shadowOndash.transform.position;
    //        rigidBody.velocity = new Vector2(0, 0);
    //        Destroy(shadowOndash);
    //        shadowExists = false;
    //    }
    //    SoundManager.PlaySound("DashRevert");
    //}


    private void basicDash()
    {

        if (dashing == false)
        {
            dashCD = 3000;
            dashing = true;
            if (VerticalDashDirection == Direction.NONE)
            {
                if (FacingDirection == Direction.RIGHT)
                {
                    float centery = (bc2d.bounds.min.y + bc2d.bounds.max.y) / 2;
                    Vector2 positioncenter = new Vector2(bc2d.bounds.max.x, centery);
                    Vector2 positionup = new Vector2(bc2d.bounds.max.x, bc2d.bounds.max.y);
                    Vector2 positiondown = new Vector2(bc2d.bounds.max.x, bc2d.bounds.min.y);

                    RaycastHit2D hitscenter = Physics2D.Raycast(positioncenter, Vector2.right, 300, LayerDashLimit);
                    RaycastHit2D hitsup = Physics2D.Raycast(positionup, Vector2.right, 300, LayerDashLimit);
                    RaycastHit2D hitsdown = Physics2D.Raycast(positiondown, Vector2.right, 300, LayerDashLimit);
                    RaycastHit2D[] array = new RaycastHit2D[] { hitsup, hitscenter, hitsdown };

                    Vector2 LimitDashPosition = transform.position + transform.right * 300;
                    for (int i = 0; i < 3; i++)
                    {
                        if (array[i].collider != null && Vector2.Distance(array[i].point, this.transform.position) < Vector2.Distance(LimitDashPosition, this.transform.position))
                        {
                            LimitDashPosition = array[i].point;
                        }
                    }
                    limitdash = Instantiate(dashtrigger, LimitDashPosition, transform.rotation);

                    rigidBody.velocity = new Vector2(dashspeed, 0);

                    rigidBody.gravityScale = 0;
                }
                else if (FacingDirection == Direction.LEFT)
                {
                    float centery = (bc2d.bounds.min.y + bc2d.bounds.max.y) / 2;
                    Vector2 positioncenter = new Vector2(bc2d.bounds.min.x, centery);
                    Vector2 positionup = new Vector2(bc2d.bounds.min.x, bc2d.bounds.max.y);
                    Vector2 positiondown = new Vector2(bc2d.bounds.min.x, bc2d.bounds.min.y);

                    RaycastHit2D hitscenter = Physics2D.Raycast(positioncenter, Vector2.right * -1, 300, LayerDashLimit);
                    RaycastHit2D hitsup = Physics2D.Raycast(positionup, Vector2.right * -1, 300, LayerDashLimit);
                    RaycastHit2D hitsdown = Physics2D.Raycast(positiondown, Vector2.right * -1, 300, LayerDashLimit);
                    RaycastHit2D[] array = new RaycastHit2D[] { hitsup, hitscenter, hitsdown };
                    Vector2 LimitDashPosition = transform.position + transform.right * 300 * -1;
                    for (int i = 0; i < 3; i++)
                    {
                        if (array[i].collider != null && Vector2.Distance(array[i].point, this.transform.position) < Vector2.Distance(LimitDashPosition, this.transform.position))
                        {
                            LimitDashPosition = array[i].point;
                        }
                    }

                    limitdash = Instantiate(dashtrigger, LimitDashPosition, transform.rotation);
                    rigidBody.velocity = new Vector2(dashspeed * -1, 0);

                    rigidBody.gravityScale = 0;
                }
            }
            else if (VerticalDashDirection == Direction.UP)
            {
                dashCD = 3000;
                dashing = true;
               
                float center = (bc2d.bounds.min.y + bc2d.bounds.max.y) / 2;
                Vector2 right = new Vector2(bc2d.bounds.max.x, bc2d.bounds.max.y);
                Vector2 poscenter = new Vector2(bc2d.bounds.center.x, bc2d.bounds.max.y);
                Vector2 left = new Vector2(bc2d.bounds.min.x, bc2d.bounds.max.y);

                RaycastHit2D hitscenter = Physics2D.Raycast(right, Vector2.up, 300, LayerDashLimit);
                RaycastHit2D hitsup = Physics2D.Raycast(poscenter, Vector2.up, 300, LayerDashLimit);
                RaycastHit2D hitsdown = Physics2D.Raycast(left, Vector2.up, 300, LayerDashLimit);
                RaycastHit2D[] array = new RaycastHit2D[] { hitsup, hitscenter, hitsdown };

                Vector2 LimitDashPosition = transform.position + transform.up * 300;
                for (int i = 0; i < 3; i++)
                {
                    if (array[i].collider != null && Vector2.Distance(array[i].point, this.transform.position) < Vector2.Distance(LimitDashPosition, this.transform.position))
                    {
                        LimitDashPosition = array[i].point;
                    }
                }
                limitdash = Instantiate(dashtrigger, LimitDashPosition, transform.rotation);

                rigidBody.velocity = new Vector2(0, dashspeed);
                
                rigidBody.gravityScale = 0;
            }
        }
        SoundManager.PlaySound("Dash");
    }
    private void cancelDash()
    {
        dashing = false;
        gameObject.layer = 8;
        Destroy(limitdash);
        rigidBody.velocity = Vector2.zero;
        rigidBody.gravityScale = 100;
    }

    //private void doubleDash()
    //{

    //    if (dashing == false)
    //    {

    //        dashCD += 5000;
    //        dashing = true;
    //        if (VerticalDashDirection == Direction.NONE)
    //        {
    //            if (FacingDirection == Direction.RIGHT)
    //            {
    //                float centery = (bc2d.bounds.min.y + bc2d.bounds.max.y) / 2;
    //                Vector2 positioncenter = new Vector2(bc2d.bounds.max.x, centery);
    //                Vector2 positionup = new Vector2(bc2d.bounds.max.x, bc2d.bounds.max.y);
    //                Vector2 positiondown = new Vector2(bc2d.bounds.max.x, bc2d.bounds.min.y);

    //                RaycastHit2D hitscenter = Physics2D.Raycast(positioncenter, Vector2.right, 300, LayerDashLimit);
    //                RaycastHit2D hitsup = Physics2D.Raycast(positionup, Vector2.right, 300, LayerDashLimit);
    //                RaycastHit2D hitsdown = Physics2D.Raycast(positiondown, Vector2.right, 300, LayerDashLimit);
    //                RaycastHit2D[] array = new RaycastHit2D[] { hitsup, hitscenter, hitsdown };

    //                Vector2 LimitDashPosition = transform.position + transform.right * 300;
    //                for (int i = 0; i < 3; i++)
    //                {
    //                    if (array[i].collider != null && Vector2.Distance(array[i].point, this.transform.position) < Vector2.Distance(LimitDashPosition, this.transform.position))
    //                    {
    //                        LimitDashPosition = array[i].point;
    //                    }
    //                }
    //                limitdash = Instantiate(dashtrigger, LimitDashPosition, transform.rotation);

    //                rigidBody.velocity = new Vector2(dashspeed, 0);

    //                rigidBody.gravityScale = 0;
    //            }
    //            else if (FacingDirection == Direction.LEFT)
    //            {
    //                float centery = (bc2d.bounds.min.y + bc2d.bounds.max.y) / 2;
    //                Vector2 positioncenter = new Vector2(bc2d.bounds.min.x, centery);
    //                Vector2 positionup = new Vector2(bc2d.bounds.min.x, bc2d.bounds.max.y);
    //                Vector2 positiondown = new Vector2(bc2d.bounds.min.x, bc2d.bounds.min.y);

    //                RaycastHit2D hitscenter = Physics2D.Raycast(positioncenter, Vector2.right * -1, 300, LayerDashLimit);
    //                RaycastHit2D hitsup = Physics2D.Raycast(positionup, Vector2.right * -1, 300, LayerDashLimit);
    //                RaycastHit2D hitsdown = Physics2D.Raycast(positiondown, Vector2.right * -1, 300, LayerDashLimit);
    //                RaycastHit2D[] array = new RaycastHit2D[] { hitsup, hitscenter, hitsdown };
    //                Vector2 LimitDashPosition = transform.position + transform.right * 300 * -1;
    //                for (int i = 0; i < 3; i++)
    //                {
    //                    if (array[i].collider != null && Vector2.Distance(array[i].point, this.transform.position) < Vector2.Distance(LimitDashPosition, this.transform.position))
    //                    {
    //                        LimitDashPosition = array[i].point;
    //                    }
    //                }

    //                limitdash = Instantiate(dashtrigger, LimitDashPosition, transform.rotation);
    //                rigidBody.velocity = new Vector2(dashspeed * -1, 0);

    //                rigidBody.gravityScale = 0;
    //            }
    //        }
    //        else if (VerticalDashDirection == Direction.UP)
    //        {
    //            float center = (bc2d.bounds.min.y + bc2d.bounds.max.y) / 2;
    //            Vector2 right = new Vector2(bc2d.bounds.max.x, bc2d.bounds.max.y);
    //            Vector2 poscenter = new Vector2(bc2d.bounds.center.x, bc2d.bounds.max.y);
    //            Vector2 left = new Vector2(bc2d.bounds.min.x, bc2d.bounds.max.y);

    //            RaycastHit2D hitscenter = Physics2D.Raycast(right, Vector2.up, 300, LayerDashLimit);
    //            RaycastHit2D hitsup = Physics2D.Raycast(poscenter, Vector2.up, 300, LayerDashLimit);
    //            RaycastHit2D hitsdown = Physics2D.Raycast(left, Vector2.up, 300, LayerDashLimit);
    //            RaycastHit2D[] array = new RaycastHit2D[] { hitsup, hitscenter, hitsdown };

    //            Vector2 LimitDashPosition = transform.position + transform.up * 300;
    //            for (int i = 0; i < 3; i++)
    //            {
    //                if (array[i].collider != null && Vector2.Distance(array[i].point, this.transform.position) < Vector2.Distance(LimitDashPosition, this.transform.position))
    //                {
    //                    LimitDashPosition = array[i].point;
    //                }
    //            }
    //            limitdash = Instantiate(dashtrigger, LimitDashPosition, transform.rotation);

    //            rigidBody.velocity = new Vector2(0, dashspeed);

    //            rigidBody.gravityScale = 0;
    //        }
    //    }

    //    SoundManager.PlaySound("Dash");
    //}

    private void DamageTaken(Vector2 damageDirection)
    {
        if (takingdamage == false && invulnerability== false)
        {
            cancelDash();
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
        else if(life <= 0)
        {
            Respawn();
        }
        SoundManager.PlaySound("Daño");
    }

    private void FixedUpdate()
    {
  
            float delta = Time.fixedDeltaTime * 1000;
        if (!dashing && !takingdamage && !stucked) {
            currentSpeedV = rigidBody.velocity.y;
            currentSpeedH = rigidBody.velocity.x;
            if (Jumping && rigidBody.velocity.x > baseSpeed)
            {
                rigidBody.velocity = new Vector2(baseSpeed, currentSpeedV);
            }
            else if (Jumping && rigidBody.velocity.x < baseSpeed * -1)
            {
                rigidBody.velocity = new Vector2(baseSpeed * -1, currentSpeedV);
            }

            switch (FacingDirection)
            {
                default:
                    Spikeyscale.x = 2f;
                    break;
                case Direction.RIGHT:
                    Spikeyscale.x = 2f;
                    break;
                case Direction.LEFT:
                    Spikeyscale.x = -2f;
                    break;

            }

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
                        //rigidBody.AddForce(transform.right * baseSpeed * delta);
                    if (Jumping)
                    {
                            rigidBody.AddForce(transform.right * 50 * delta);
                    }

                  else  if (!sprinting)
                  {
                        rigidBody.velocity = new Vector2(baseSpeed, currentSpeedV);
                  }
                    else
                    {
                        rigidBody.velocity = new Vector2(sprintSpeed, currentSpeedV);
                    }
                    break;
                case Direction.LEFT:
                        //rigidBody.AddForce((transform.right * baseSpeed * delta) * -1);

                   if (Jumping)
                   {
                            rigidBody.AddForce(transform.right * 50*-1 * delta);
                   }
                   else if (!sprinting)
                   {
                        rigidBody.velocity = new Vector2(baseSpeed * -1, currentSpeedV);
                   }
                   else
                   {
                        rigidBody.velocity = new Vector2(sprintSpeed * -1, currentSpeedV);
                   }
                    break;
                case Direction.NONE:
                        //if (climbing)
                        //{
                        //   rigidBody.velocity = new Vector2(0, 0);
                        //}
                   if (movingPlataform != null)
                   {
                            rigidBody.velocity = new Vector2(movingPlataform.GetComponent<Rigidbody2D>().velocity.x, currentSpeedV);
                   }

                   else if(Jumping)
                   {
                        rigidBody.velocity = new Vector2(currentSpeedH, currentSpeedV);
                   }
                   else 
                   {
                           rigidBody.velocity = new Vector2(0, 0);
                   }
                   break;
            }
        }

    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Water")
        {
            //sprintSpeed = 80;
        }
        else if(collision.gameObject.tag == "Tilemap" || collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Crocodile"|| collision.gameObject.tag == "Water")
        {
            if (collision.gameObject.tag == "Wall")
            {
                bool col = false;

                float centery = (bc2d.bounds.min.y + bc2d.bounds.max.y) / 2;
                Vector2 positionright = new Vector2(bc2d.bounds.max.x, centery);
                Vector2 positionleft = new Vector2(bc2d.bounds.min.x, centery);
                if (FacingDirection == Direction.LEFT)
                {
                    RaycastHit2D[] hits2 = Physics2D.RaycastAll(positionleft, -Vector2.right, 2);
                    if (checkRaycastWithScenarioClimb(hits2)) { col = true; }
                }
                else if (FacingDirection == Direction.RIGHT)
                {
                    RaycastHit2D[] hits2 = Physics2D.RaycastAll(positionright, Vector2.right, 2);
                    if (checkRaycastWithScenarioClimb(hits2)) { col = true; }
                }
                if (col && canClimb == true) { climbing = true; Jumping = false; }

            }
            if (Jumping == true)
            {
              
                bool col1 = false;
                bool col2 = false;
                bool col3 = false;
                float centerx = (bc2d.bounds.min.x + bc2d.bounds.max.x) / 2;
                Vector2 centerPosition = new Vector2(centerx, bc2d.bounds.min.y);
                Vector2 leftPosition = new Vector2(bc2d.bounds.min.x, bc2d.bounds.min.y);
                Vector2 rightPosition = new Vector2(bc2d.bounds.max.x, bc2d.bounds.min.y);

                RaycastHit2D[] hits = Physics2D.RaycastAll(centerPosition, -Vector2.up, 2);
                if (checkRaycastWithScenarioJump(hits)) { col1 = true; }
                hits = Physics2D.RaycastAll(leftPosition, -Vector2.up, 2);
                if (checkRaycastWithScenarioJump(hits   )) { col2 = true; }
                hits = Physics2D.RaycastAll(rightPosition, -Vector2.up, 2);
                if (checkRaycastWithScenarioJump(hits)) { col3 = true; }

                if (col1 || col2 || col3) { Jumping = false; }
                else { isJumping = true; }

            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Crocodile")
        {
            movingPlataform = collision.gameObject;
        }

        if (collision.gameObject.tag == "Water")
        {
            Jumping = false;
            //sprintSpeed = 80;
        }
        else if (collision.gameObject.tag == "ToxicWater")
        {
            Vector2 damagedirection = this.transform.position - collision.transform.position;
            DamageTaken(damagedirection);
            SoundManager.PlaySound("Damage");
            Respawn();
        }
        else if(collision.gameObject.tag == "Enemies" && dashing==false)
        {
            Vector2 damagedirection = this.transform.position-collision.transform.position;
            DamageTaken(damagedirection);
            GameManagerController.Instance.lifeDown = true;
            SoundManager.PlaySound("Damage");
        }
        else if(collision.gameObject.tag == "Enemies" && dashing==true)
        {

            cancelDash();
            if (basicDashActivated)
            {
                dashCD = 0;
                cd_dash.isUsed = false;
            }
            SoundManager.PlaySound("Damage");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BoundTrap")
        {
            rigidBody.velocity = Vector2.zero;
            rigidBody.AddForce(new Vector2(-1000, 500), ForceMode2D.Impulse);
        }
        else if(collision.gameObject.tag == "BoundTrapReverse")
        {
            rigidBody.velocity = Vector2.zero;
            rigidBody.AddForce(new Vector2(700, 500), ForceMode2D.Impulse);
        }
        else if(collision.gameObject.tag == "Dash")
        {
            cancelDash();
        }
        else if(collision.gameObject.tag == "JawTrap")
        {
            cancelDash();
            rigidBody.velocity = Vector2.zero;
            stucked = true;
        }
        else if(collision.gameObject.tag == "Enemies")
        {
            Vector2 damagedirection = this.transform.position - collision.transform.position;
            DamageTaken(damagedirection);
            GameManagerController.Instance.lifeDown = true;

        }
        else if (collision.gameObject.tag == "Arrow")
        {
            Vector2 damagedirection = this.transform.position - collision.transform.position;
            DamageTaken(damagedirection);
            GameManagerController.Instance.lifeDown = true;

        }
        else if (collision.gameObject.tag == "CheckPoint")
        {
            lastCheckpoint = collision.transform.position;
            SoundManager.PlaySound("Checkpoint");
        }
        else if (collision.gameObject.tag == "Coin")
        {
            Destroy(collision.gameObject);
            GameManagerController.Instance.coin = true;
            SoundManager.PlaySound("Coin");
        }
        else if (collision.gameObject.tag == "BigCoin")
        {
            GameManagerController.Instance.bigCoin = true;
            Destroy(collision.gameObject);
            SoundManager.PlaySound("Coin");
        }
        else if (collision.gameObject.tag == "LifeUp")
        {
            GameManagerController.Instance.lifeUp = true;
            Destroy(collision.gameObject);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Tilemap")
        {
            Jumping = true;
        }
        else if(collision.gameObject.tag == "Water")
        {
            Jumping = true;
            //sprintSpeed = 130;
        }
        else if(collision.gameObject.tag == "Crocodile")
        {
            Jumping = true;
            movingPlataform = null;
        }
        else if(collision.gameObject.tag == "Wall")
        {
            Jumping = true;
           
            if (climbing == true)
            {

                climbing = false;
                rigidBody.gravityScale = 100;

            }
        }
    }
}