using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundTrapController : MonoBehaviour
{
    private Animator animator;
    private int isOn_animation;
    private bool isOn = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        isOn_animation = Animator.StringToHash("isOn");

    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool(isOn_animation, isOn);
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Spikey")
        {
            isOn = true;
        }
    }


    private void SetBool() { isOn = false; }
}
