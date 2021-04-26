﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoloController : MonoBehaviour
{
    private Animator animator;
    private Transform Spikey;
    [SerializeField] GameObject Rock;
    private GameObject Mole;
    //[SerializeField] GameObject Rock;
    private float turretRadius = 400.0f;
    private float timer = 0.0f;
    private float bulletSpeed = 250.0f;
    private int mad_animaton;
    private int attack_animation;
    private int wait_animation;
    private int out_animation;
    private bool isMad = false;
    private bool isAttacking = false;
    private bool isWaiting = false;
    private bool isOut = false;

    private Vector3 speed;
    private Vector3 direction;
    private float thurst = 25.0f;
    private float angle = 45.0f;
    public float gravity = 9.8f;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        mad_animaton = Animator.StringToHash("isMad");
        attack_animation  = Animator.StringToHash("isAttacking");
        out_animation = Animator.StringToHash("getHitted");
        wait_animation = Animator.StringToHash("isWaiting");

        Mole = GetComponent<GameObject>();
        Spikey = GameObject.Find("Spikey").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (checkShipPosition())
        {
            isMad = true;
        }
        else
        {
            isMad = false;
            isAttacking = false;
        }

        if (isWaiting)
        {
            timer += Time.deltaTime;
            if (timer > 2)
            {
                timer = 0;
                isWaiting = false;
            }
        }

        animator.SetBool(mad_animaton, isMad);
        animator.SetBool(attack_animation, isAttacking);
        animator.SetBool(wait_animation, isWaiting);
        animator.SetBool(out_animation, isOut);
    }

    private bool checkShipPosition()
    {
        return Vector2.Distance(this.transform.position, Spikey.transform.position) <= this.turretRadius;
    }

    private void shootToPosition()
    {
        GameObject inst = Instantiate(Rock, this.transform.position + transform.up * -5, Rock.transform.rotation);
        //RockController bc = inst.GetComponent<RockController>();

        //bc.setVelocity();
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, turretRadius);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Puas")
        {
            isOut = true;
            gameObject.layer = 21;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Shadow")
        {
            isOut = true;
            gameObject.layer = 21;
        }
    }

    private void SetAttackAnimation() { isAttacking = true; }

    private void HoldAnimation() { isWaiting = true; }

    private void ShootToPosition() { shootToPosition(); }

    private void IsDead() { Destroy(gameObject); }

}
