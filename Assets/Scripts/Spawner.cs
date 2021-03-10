using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    private float timer = 0;

    public GameObject Gouts;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float delta = Time.deltaTime * 1000;

        timer += delta;
        if (timer > 2000)
        {
            GameObject Gota = Instantiate(Gouts, transform.position - (transform.up * 4), transform.rotation);
            timer = 0;
            
        }

    }
}
