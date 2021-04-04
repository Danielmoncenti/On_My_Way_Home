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


    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        hold_animation = Animator.StringToHash("isHolding");
        splash_animation = Animator.StringToHash("Splash");

    }

    // Update is called once per frame
    void Update()
    {
        if (rigidBody.velocity.y > 100)
        {
            rigidBody.drag = 0;
            rigidBody.velocity = new Vector2(0.0f, 100.0f);
        }

        if (stop)
        {
            rigidBody.velocity = Vector3.zero;
            rigidBody.gravityScale = 0;
        }

        animator.SetBool(hold_animation, isHolding);
        animator.SetBool(splash_animation, splash);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Tilemap")
        {
            stop = true;
            splash = true;
            SoundManager.Play(SoundManager.Sound.DROP);
        }
    }

    private void HoldAnimation() { isHolding = true; }

    private void DestroyDrop() { Destroy(gameObject); }
}
