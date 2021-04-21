using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointController : MonoBehaviour
{
    private Animator animator;
    private int nice_animator;
    private int checked_animator;
    private bool isNice = false;
    private bool isChecked = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        nice_animator = Animator.StringToHash("isNice");
        checked_animator = Animator.StringToHash("isChecked");
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool(nice_animator, isNice);
        animator.SetBool(checked_animator, isChecked);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Spikey")
        {
            isNice = true;
        }
    }

    private void SetCheckAnimator() { isNice = false; isChecked = true;   }

}
