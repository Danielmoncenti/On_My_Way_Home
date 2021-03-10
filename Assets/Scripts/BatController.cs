using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatController : MonoBehaviour
{
    private float baseSpeed = 3.0f;
    //private float maxSpeed = 8.0f;

    private float currentSpeedV = 0.0f;
    private float currentSpeedH = 0.0f;

    private Rigidbody2D rigidBody;

    private float positionX;
    private float positionY;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        /*float rand = Random.Range(0.0f, 0.8f);
        if (rand < 0.2)
        {
            currentSpeedH = -baseSpeed;
        }
        else if (rand == 0.2 || rand < 0.4)
        {
            currentSpeedH = baseSpeed;
        }
        else if (rand == 0.4 || rand < 0.6)
        {
            currentSpeedV = baseSpeed;
        }
        else if (rand == 0.6 || rand < 0.8)
        {
            currentSpeedV = -baseSpeed;
        }*/

        currentSpeedV = -baseSpeed;

        positionX = transform.position.x;
        positionY = transform.position.y;

    }

    // Update is called once per frame
    void Update()
    {

        if (transform.position.y >= 30.0f)
        {
            currentSpeedV *= -1;
        }



        /*if (positionX > positionX + 100 || positionX < positionX - 100)
        {
            currentSpeedH *= -1;
        }*/



    }
    private void FixedUpdate()
    {
        float delta = Time.fixedDeltaTime * 1000; ;
        rigidBody.velocity = new Vector2(currentSpeedH, currentSpeedV) * delta;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Tilemap")
        {
            currentSpeedH *= -1;
            currentSpeedV *= -1;
        }
        if (collision.gameObject.tag == "Puas")
        {
            Destroy(gameObject);
        }

    }
}
