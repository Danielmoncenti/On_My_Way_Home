using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThormsController : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    private Animator animator;
    private int mad_animation;
    private bool isMad = false;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        mad_animation = Animator.StringToHash("isMad");
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool(mad_animation, isMad);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Spikey")
        {
            isMad = true;
        }
    }

    private void SetMadness() { isMad = false; }
}
