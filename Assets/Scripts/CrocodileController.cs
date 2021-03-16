using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrocodileController : MonoBehaviour
{
    private float baseSpeed = 3.0f;
    public float currentSpeedH = 0.0f;
    public float currentSpeedV = 0.0f;
    public bool canAttack = false;

    private int crocodileRadius = 300;
    [SerializeField] GameObject Spikey;

    private Rigidbody2D rigidBody;
    Vector3 crocodileScale;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        crocodileScale = transform.localScale;
        float rand = Random.Range(0.0f, 1.0f);
        if (rand < 0.5)
        {
            currentSpeedH = -baseSpeed;
            crocodileScale.x = 2f;
        }
        else
        {
            currentSpeedH = baseSpeed;
            crocodileScale.x = -2f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = crocodileScale;

        if (checkSpikeyPosition())
        {
            currentSpeedH = 0.0f;
            canAttack = true;
        }
        else if (!checkSpikeyPosition())
        {
            currentSpeedH = -baseSpeed;
            canAttack = false;
        }
    }

    private void FixedUpdate()
    {
        float delta = Time.fixedDeltaTime * 1000;

        if (canAttack)
        {
            if (delta >= 1000)
            {
                currentSpeedV += 5;
                if (delta >= 2000)
                {
                    currentSpeedV -= 5;
                    delta = 0;
                }
            }
            else if (!canAttack)
            {
                currentSpeedV = 0;
            }
        }

        rigidBody.velocity = new Vector2(currentSpeedH, 0.0f) * delta;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Tilemap")
        {
            currentSpeedH *= -1;
            crocodileScale.x *= -1;
        }
        if (collision.gameObject.tag == "Puas")
        {
            Destroy(gameObject);
        }
    }
    private bool checkSpikeyPosition()
    {
        return Vector2.Distance(this.transform.position, Spikey.transform.position) <= this.crocodileRadius;
    }
}
