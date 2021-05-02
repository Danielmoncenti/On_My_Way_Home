using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoloController : MonoBehaviour
{
    Animator animator;
    Transform Spikey;
    [SerializeField] GameObject Rock;
    [SerializeField] SpikeyController obj_Spikey;
    GameObject Mole;
    //[SerializeField] GameObject Rock;
    float turretRadius = 400.0f;
    float timer = 0.0f;
    //float bulletSpeed = 250.0f;
    int mad_animaton;
    int attack_animation;
    int wait_animation;
    int out_animation;
    bool isMad = false;
    bool isAttacking = false;
    bool isWaiting = false;
    bool isOut = false;

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
        else if (collision.gameObject.tag == "Spikey" && obj_Spikey.killedByDash)
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
