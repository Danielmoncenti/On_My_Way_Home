using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatController : MonoBehaviour
{
    private float baseSpeed = 3.0f;
    private float maxSpeed = 8.0f;

    private float currentSpeedH = 0.0f;

    private int ratRadius = 300;
    private Transform Spikey;

    private Rigidbody2D rigidBody;
    private BoxCollider2D boxCollider2D;
    private Animator animator;

    private int mad_animation;
    private int bite_animation;
    private int falling_animation;

    private bool isMad = false;
    private bool isBitting = false;
    private bool isFalling = false;
    Vector3 ratScale;

    // Start is called before the first frame update
    void Start()
    { 
        rigidBody = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        mad_animation = Animator.StringToHash("isMad");
        bite_animation = Animator.StringToHash("isBitting");
        falling_animation = Animator.StringToHash("isFalling");
        Spikey = GameObject.Find("Spikey").GetComponent<Transform>();
        ratScale = transform.localScale;
        currentSpeedH = -baseSpeed;
        ratScale.x = -2.5f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = ratScale;

        if (checkSpikeyPosition())
        {
            StartRayCast();
        }
        else if (!checkSpikeyPosition())
        {
            if (currentSpeedH == maxSpeed) { currentSpeedH = baseSpeed; }
            else if (currentSpeedH == -maxSpeed){ currentSpeedH = -baseSpeed; }
            isMad = false;
        }

        if (isFalling)
        {
            currentSpeedH = 0.0f;
            gameObject.layer = 21;
        }

        animator.SetBool(mad_animation, isMad);
        animator.SetBool(bite_animation, isBitting);
        animator.SetBool(falling_animation, isFalling);
    }

    private void FixedUpdate()
    {
        float delta = Time.fixedDeltaTime * 1000; ;
        rigidBody.velocity = new Vector2(currentSpeedH, 0.0f) * delta;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            currentSpeedH *= -1;
            ratScale.x *= -1;
        }
        if (collision.gameObject.tag == "Puas")
        {
            isFalling = true;
        }
        if (collision.gameObject.tag == "Spikey")
        {
            isBitting = true;
        }
    }

    private void SetBitAnimation()
    {
        isBitting = false;
    }

    private void RatisDead ()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Shadow")
        {
            isFalling = true;
        }
    }

    private bool checkRaycastWithScenario(RaycastHit2D hits)
    {
       if (hits.collider != null)
       {
           if(hits.collider.gameObject.tag == "Spikey") { return true; }
       }  
        return false;
    }

    private void StartRayCast()
    {
        bool rightCollision = false;
        bool leftCollision = false;
        Vector2 rightPosition = new Vector2(boxCollider2D.bounds.max.x, boxCollider2D.bounds.max.y);
        Vector2 leftPosition = new Vector2(boxCollider2D.bounds.min.x, boxCollider2D.bounds.max.y);

        if (currentSpeedH == baseSpeed)
        {
            RaycastHit2D hits = Physics2D.Raycast(rightPosition, Vector2.right, 200);
            if (checkRaycastWithScenario(hits)) { rightCollision = true; }
        }
        else if (currentSpeedH == -baseSpeed)
        {
            RaycastHit2D hits = Physics2D.Raycast(leftPosition, -Vector2.right, 200);
            if (checkRaycastWithScenario(hits)) { leftCollision = true; }
        }
        
        if (rightCollision) { 
            currentSpeedH = maxSpeed;
            isMad = true;
            SoundManager.PlaySound("RatChase");
        }
        else if (leftCollision) { 
            currentSpeedH = -maxSpeed; 
            isMad = true;
            SoundManager.PlaySound("RatChase");
        }

    }
    private bool checkSpikeyPosition()
    {
        return Vector2.Distance(this.transform.position, Spikey.transform.position) <= this.ratRadius;
    }
}
