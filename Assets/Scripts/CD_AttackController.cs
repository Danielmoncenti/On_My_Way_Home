using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CD_AttackController : MonoBehaviour
{
    Animator animator;
    public bool isUsed = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Cuando ataque 
        //isUsed = true;

        animator.SetBool("isUsed", isUsed);
    }

    private void SettoIddle() { isUsed = false; }

}
