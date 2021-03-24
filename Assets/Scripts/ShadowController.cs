using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowController : MonoBehaviour
{
    private Rigidbody2D rb2d;
    public GameObject erizo;
    private Vector2 startPosition;
    private float timer;
    public float TotalTime;
    public bool startReturn;
   
    SpikeyController erizocontroller;

    // Start is called before the first frame update
    void Start()
    {
        startReturn = false;
        rb2d = GetComponent<Rigidbody2D>();
        startPosition = rb2d.transform.position;
       
    }

    // Update is called once per frame
    void Update()
    {
        if (startReturn == true)
        {
            gameObject.layer = 0;
            float t = timer / TotalTime;
            rb2d.transform.position = (Vector2.Lerp(startPosition, erizo.transform.position, t));
            timer += Time.deltaTime;
            if (t > 1)
            {
                erizocontroller = erizo.GetComponent < SpikeyController>();
                erizocontroller.shadowExists = false;
                Destroy(gameObject);
                
            }
        }
    }
}

