using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CD_DashController : MonoBehaviour
{
    private Animator animator;
    public bool isUsed = false;
    private bool isComing = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Cuando dashea
        //isUsed = true

        animator.SetBool("isUsed", isUsed);
        animator.SetBool("isComing", isComing);
    }

    private void Recharge() { isComing = true; }

    private void SettoIddle() { isUsed = false; isComing = false; }
}
