using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatController : MonoBehaviour
{
    float baseSpeed = 5.0f;
    float maxSpeed = 10.0f;

    public float currentSpeedH = 0.0f;

    int ratRadius = 500;
    Transform Spikey;
    [SerializeField] SpikeyController obj_Spikey;

    Rigidbody2D rigidBody;
    BoxCollider2D boxCollider2D;
    Animator animator;

    bool isMad = false;
    bool isBitting = false;
    public bool isFalling = false;
    Vector3 ratScale;

    // Start is called before the first frame update
    void Start()
    { 
        rigidBody = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        Spikey = GameObject.Find("Spikey").GetComponent<Transform>();
        ratScale = transform.localScale;
        currentSpeedH = -baseSpeed;
        ratScale.x = -3.5f;
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

        animator.SetBool("isMad", isMad);
        animator.SetBool("isBitting", isBitting);
        animator.SetBool("isFalling", isFalling);
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
            if (obj_Spikey.killedByDash)
            {
                isFalling = true;
            }
            else
            {
                isBitting = true;
            }
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
        Vector2 rightPosition = new Vector2(boxCollider2D.bounds.max.x, boxCollider2D.bounds.center.y);
        Vector2 leftPosition = new Vector2(boxCollider2D.bounds.min.x, boxCollider2D.bounds.center.y);

        if (currentSpeedH == baseSpeed)
        {
            RaycastHit2D[] hits = Physics2D.RaycastAll(rightPosition, Vector2.right, 400);
            if (checkRaycastWithScenario(hits)) { rightCollision = true; }
        }
        else if (currentSpeedH == -baseSpeed)
        {
            RaycastHit2D[] hits = Physics2D.RaycastAll(leftPosition, -Vector2.right, 400);
            if (checkRaycastWithScenario(hits)) { 
                leftCollision = true; 
            }
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
