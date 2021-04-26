using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockController : MonoBehaviour
{

    
    private GameObject Rock;
    private float angle = 45.0f;
    private float thurst = 215.0f;
    private Vector3 speed;
    private Vector3 direction;


    private Rigidbody2D _rb2d;
    //public Rigidbody2D rb2d
    //{
    //    get
    //    {
    //        _rb2d = _rb2d ?? GetComponent<Rigidbody2D>();
    //        return _rb2d;
    //    }
    //}

    // Start is called before the first frame update
    void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        direction = new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad) * -1, Mathf.Cos(angle * Mathf.Deg2Rad), 0);

        direction.Normalize();

        speed = direction * thurst;

        _rb2d.AddForce(speed, ForceMode2D.Impulse);
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
