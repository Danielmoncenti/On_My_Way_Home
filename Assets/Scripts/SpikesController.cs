using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikesController : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    private float thurst = 200.0f;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.velocity = transform.up * thurst;

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Tilemap")
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Rat")
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Bat")
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Crocodile")
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Lobster")
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Mole")
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Gout")
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "ClimbedWall")
        {
            Destroy(gameObject);
        }
    }
}