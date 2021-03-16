using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatController : MonoBehaviour
{
    private float baseSpeed = 3.0f;
    private float maxSpeed = 8.0f;
    public float currentSpeedV = 0.0f;
    private float maxPositionY = 16.0f;
    private int batRadius = 300;
    private bool actNormal = true;

    private Rigidbody2D rigidBody;
    private BoxCollider2D boxCollider2D;

    [SerializeField] GameObject Spikey;

    
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
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
           currentSpeedV = -baseSpeed;
            actNormal = true;
       }
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
        if (collision.gameObject.tag == "Tilemap")
        {
            currentSpeedV *= -1;
        }
        if (collision.gameObject.tag == "Puas")
        {
            Destroy(gameObject);
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
        Vector2 rightPosition = new Vector2(boxCollider2D.bounds.max.x, boxCollider2D.bounds.min.y);
        Vector2 leftPosition = new Vector2(boxCollider2D.bounds.min.x, boxCollider2D.bounds.min.y);

        RaycastHit2D[] hits = Physics2D.RaycastAll(rightPosition, -Vector2.up, 150);
        if (checkRaycastWithScenario(hits)) { rightCollision = true; }
        
        hits = Physics2D.RaycastAll(leftPosition, -Vector2.up, 150);
        if (checkRaycastWithScenario(hits)) { leftCollision = true; }
      
        if (rightCollision || leftCollision) { currentSpeedV = -maxSpeed; actNormal = false; }

    }
    private bool checkSpikeyPosition()
    {
        return Vector2.Distance(this.transform.position, Spikey.transform.position) <= this.batRadius;
    }
}
