using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockController : MonoBehaviour
{
    private Transform Rock;
    private GameObject Spikey;

    private float angle = 45.0f;
    private float thurst = 0.55f;
    private Vector3 speed;
    private Vector3 distance;
    private Vector3 targetposition;
    private Vector3 direction;


    private Rigidbody2D _rb2d;

    // Start is called before the first frame update
    void Start()
    {
        Spikey = GameObject.FindGameObjectWithTag("Spikey");

        Rock = GetComponent<Transform>();
        _rb2d = GetComponent<Rigidbody2D>();

        distance = Rock.transform.position - Spikey.transform.position;
        if (distance.x > 0) {
            direction = new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad) * -1, Mathf.Cos(angle * Mathf.Deg2Rad), 0);

            direction.Normalize();

            thurst = thurst * distance.x;

            speed = direction * thurst;

            _rb2d.AddForce(speed, ForceMode2D.Impulse);
        }else {
            direction = new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad) * -1, Mathf.Cos(angle * Mathf.Deg2Rad), 0);

            direction.Normalize();

            thurst = thurst * distance.x;

            speed = direction * thurst;
            speed.y = speed.y * -1; 
            _rb2d.AddForce(speed, ForceMode2D.Impulse);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void setVelocity()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Tilemap")
        {
            Destroy(gameObject);
        }
        else  if (collision.gameObject.tag == "Spikey")
        {
            Destroy(gameObject);
        }
    }
}
