using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockController : MonoBehaviour
{
    Rigidbody2D _rb2d;
    GameObject Rock;
    float angle = 45.0f;
    float thurst = 125.0f;
    Vector3 speed;
    Vector3 direction;

    [SerializeField] GameObject Spikey;

    // Start is called before the first frame update
    void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        direction = new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), Mathf.Cos(angle * Mathf.Deg2Rad), 0);

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
