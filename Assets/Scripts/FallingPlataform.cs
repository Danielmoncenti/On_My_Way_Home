using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlataform : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private Animator animator;
    private int fallingID;
    public bool falling = false;
    public float falltimer = 0;
    public float startshake = 1;
    public float fall = 3;
    private Vector2 initialpos;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        fallingID = Animator.StringToHash("Falling");
        initialpos = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (falling)
        {
            falltimer += Time.deltaTime;
            if (falltimer >= 1)
            {
                animator.SetBool(fallingID, true);
            }
            if (falltimer >= 3)
            {
                animator.SetBool(fallingID, false);
                rb2d.gravityScale = 100;
            }
            if (falltimer >= 6)
            {
                falling = false;
                rb2d.gravityScale = 0;
                falltimer = 0;
                rb2d.velocity = Vector2.zero;
                rb2d.transform.position = initialpos;
                

            }
        }

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Spikey")
        {
            falling = true;
        }
    }
    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag == "Spikey" && falltimer<3)
    //    {
    //        falling = false;
    //        falltimer = 0;
    //        animator.SetBool(fallingID, false);
    //    }
    //}
    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag == "Spikey")
    //    {

    //    }
    //}
}
