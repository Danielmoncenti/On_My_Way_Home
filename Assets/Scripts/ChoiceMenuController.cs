using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceMenuController : MonoBehaviour
{
    private Animator animator;
    private int On;
    private int Off;
    private bool isSelected = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        On = Animator.StringToHash("isSelected");
        Off = Animator.StringToHash("isSelected");
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool(On, isSelected);
        animator.SetBool(Off, isSelected);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Control")
        {
            isSelected = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Control")
        {
            isSelected = false;
        }
    }

}
