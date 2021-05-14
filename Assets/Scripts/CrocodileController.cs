using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrocodileController : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rigidBody;
    private int bite_animation;
    public bool isBiting = false;

    private float timer = 0.0f;
    private float baseSpeed = 10.0f;
    private float currentSpeedH = 0.0f;
    private float currentSpeedV = 0.0f;
    private bool canAttack = true;
    
    Vector3 crocodileScale;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        bite_animation = Animator.StringToHash("isBiting");
        crocodileScale = transform.localScale;
        currentSpeedH = -baseSpeed;
        crocodileScale.x = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if (canAttack)
        {
            timer += Time.deltaTime;
            if (timer > 3)
            {
                isBiting = true;
                timer = 0.0f;
                SoundManager.PlaySound("Roar");
            }
        }
        
        animator.SetBool(bite_animation, isBiting);
        
        transform.localScale = crocodileScale;
    }

    private void FixedUpdate()
    {
        float delta = Time.fixedDeltaTime * 1000;
        rigidBody.velocity = new Vector2(currentSpeedH, currentSpeedV) * delta;
    }

    private void SetToIddle() { isBiting = false; }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Tilemap")
        {
            currentSpeedH *= -1;
            crocodileScale.x *= -1;
        }
        else if (collision.gameObject.tag == "Spikey")
        {
            canAttack = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Spikey")
        {
            canAttack = true;
        }
    }

   
}
