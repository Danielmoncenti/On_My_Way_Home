using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CD_AttackController : MonoBehaviour
{
    private Animator animator;
    private int used_animation;
    private int coming_animation;
    private bool isUsed = false;
    private bool isComing = false;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        used_animation = Animator.StringToHash("isUsed");
        coming_animation = Animator.StringToHash("isComing");
    }

    // Update is called once per frame
    void Update()
    {
        //cuando ataque 
        //isUsed = true;


        animator.SetBool(used_animation, isUsed);
        animator.SetBool(coming_animation, isComing);
    }


    private void SetToIddle() { isComing = true; }


    private void Recharge() { isUsed = false; }
}
