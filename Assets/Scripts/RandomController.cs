using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomController : MonoBehaviour
{

    private Rigidbody2D rb2d;
   
    public float enemyRadius;
    public LayerMask layers;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();


    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D rayhit = Physics2D.Raycast(this.transform.position, -this.transform.up, Mathf.Infinity, layers);
        if (rayhit.rigidbody!=null && rayhit.rigidbody.CompareTag("Spikey"))
        {
            rb2d.velocity = new Vector2(0, -200);
            Debug.Log("a");
        }
    }

    

}
