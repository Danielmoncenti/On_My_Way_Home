using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomController : MonoBehaviour
{
    private Animator animator;
    private int bounce_animation;
    private bool isBouncing = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        bounce_animation = Animator.StringToHash("isBouncing");
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool(bounce_animation, isBouncing);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Spikey")
        {
            isBouncing = true;
        }
    }

    private void ResetAnimation() { isBouncing = false; }
}
