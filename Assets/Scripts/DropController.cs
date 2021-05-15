using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropController : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    private Animator animator;
    private int hold_animation;
    private int splash_animation;
    private bool isHolding = false;
    private bool splash = false;
    private bool stop = false;

    public static AudioClip Drop;
    static AudioSource audiosrc;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        hold_animation = Animator.StringToHash("isHolding");
        splash_animation = Animator.StringToHash("Splash");

        Drop = Resources.Load<AudioClip>("Drop");
        audiosrc = GetComponent<AudioSource>();
        audiosrc.maxDistance = 300;
    }

    // Update is called once per frame
    void Update()
    {
        if (rigidBody.velocity.y < -450)
        {
            //rigidBody.drag = 0;
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, -450.0f);
        }

        if (stop)
        {
            rigidBody.gravityScale = 0;
            rigidBody.velocity = Vector2.zero;
        }

        animator.SetBool(hold_animation, isHolding);
        animator.SetBool(splash_animation, splash);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Tilemap")
        {
            audiosrc.PlayOneShot(Drop);
            stop = true;
            splash = true;
        }
    }

    /*private void CheckCollisions()
    {
       
    }*/


    private void HoldAnimation() { isHolding = true; }

    private void DestroyDrop() { Destroy(gameObject); }
}
