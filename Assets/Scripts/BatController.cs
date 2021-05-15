using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatController : MonoBehaviour
{
    private float baseSpeed = 3.0f;
    private float maxSpeed = 8.0f;
    private float currentSpeedV = 0.0f;
    private float maxPositionY;
    private int batRadius = 300;
    private bool actNormal = true;

    private Rigidbody2D rigidBody;
    private BoxCollider2D boxCollider2D;
    private Animator animator;

    private int mad_animation;
    private int falling_animation;
    private int dead_animation;

    private bool isMad = false;
    public bool isFalling = false;
    private bool isDead = false;

    private Transform Spikey;
    [SerializeField] SpikeyController obj_Spikey;

    
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        mad_animation = Animator.StringToHash("isMad");
        falling_animation = Animator.StringToHash("isFalling");
        dead_animation = Animator.StringToHash("isDead");
        Spikey = GameObject.Find("Spikey").GetComponent<Transform>();

        maxPositionY = this.transform.position.y + 10;

        float rand = Random.Range(0.0f, 0.8f);
        if (rand < 0.8)
        {
            currentSpeedV = baseSpeed;
        }
        else
        {
            currentSpeedV = -baseSpeed;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (checkSpikeyPosition())
        {
            StartRayCast();
        }
        else if (!checkSpikeyPosition() && !actNormal)
        {
            if (currentSpeedV == -maxSpeed) currentSpeedV = -baseSpeed;
            else { currentSpeedV = baseSpeed; }
            actNormal = true;
            isMad = false;
        }

        if (isFalling || isDead)
        {
            currentSpeedV = -baseSpeed;
            gameObject.layer = 21;
        }

        animator.SetBool(mad_animation, isMad);
        animator.SetBool(falling_animation, isFalling);
        animator.SetBool(dead_animation, isDead);
    }
   
    private void FixedUpdate()
    {
        if (this.transform.position.y > maxPositionY)
        {
            currentSpeedV *= -1;
        }
        float delta = Time.fixedDeltaTime * 1000; ;
        rigidBody.velocity = new Vector2(0.0f, currentSpeedV) * delta;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Tilemap" || collision.gameObject.tag == "Wall")
        {
            if (isFalling)
            {
                isDead = true;
                currentSpeedV = 0.0f;
                Destroy(gameObject, 2);
            }
            currentSpeedV *= -1;
        }
        else if (collision.gameObject.tag == "Water")
        {
            if (isFalling)
            {
                isDead = true;
                currentSpeedV = 0.0f;
                Destroy(gameObject, 2);
            }
            else
            {
                currentSpeedV *= -1;
            }
        }
        else if (collision.gameObject.tag == "Puas")
        {
            isFalling = true;
            currentSpeedV = -maxSpeed;
        }
        else if (collision.gameObject.tag == "Spikey")
        {
            if (obj_Spikey.killedByDash)
            {
                isFalling = true;
            }
            else
            {
                currentSpeedV *= -1;
            }
        }
    }

    private bool checkRaycastWithScenario(RaycastHit2D[] hits)
    {
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.tag == "Spikey") { return true; }
            }
        }
        return false;
    }

    private void StartRayCast()
    {
        bool rightCollision = false;
        bool leftCollision = false;
        //molaba hacerlo en diagonal
        Vector2 rightPosition = new Vector2(boxCollider2D.bounds.max.x, boxCollider2D.bounds.min.y);
        Vector2 leftPosition = new Vector2(boxCollider2D.bounds.min.x, boxCollider2D.bounds.min.y);

        RaycastHit2D[] hits = Physics2D.RaycastAll(rightPosition, -Vector2.up, 150);
        if (checkRaycastWithScenario(hits)) { rightCollision = true; }
        
        hits = Physics2D.RaycastAll(leftPosition, -Vector2.up, 150);
        if (checkRaycastWithScenario(hits)) { leftCollision = true; }
      
        if (rightCollision || leftCollision) { 
            if (currentSpeedV == -baseSpeed) currentSpeedV = -maxSpeed;
            else if (currentSpeedV == baseSpeed) { currentSpeedV = -maxSpeed; }
            actNormal = false;
            isMad = true;
            SoundManager.PlaySound("BatChase");
        }
    }
    private bool checkSpikeyPosition()
    {
        return Vector2.Distance(this.transform.position, Spikey.transform.position) <= this.batRadius;
    }
}
