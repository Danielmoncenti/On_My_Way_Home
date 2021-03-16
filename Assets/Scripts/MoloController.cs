using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoloController : MonoBehaviour
{
  
    private float life = 3;
    private Animator animations;
    private Rigidbody2D rb2d;
    private int left;
    private int right;
    private int center;
    public GameObject erizo;
    public GameObject rock;
    private float turretRadius;
    private float timer;
    private float bulletSpeed;

    // Start is called before the first frame update
    void Start()
    {
        bulletSpeed = 250;
        turretRadius = 350;
        timer = 0;
        rb2d = GetComponent<Rigidbody2D>();
   
    }

    // Update is called once per frame
    void Update()
    {
        
        if (checkShipPosition())
        {

            timer += Time.deltaTime;
            if (timer >= 2)
            {
                timer = 0;
                shootToPosition(erizo.transform.position);
            }

        }
    }
    private bool checkShipPosition()
    {

        return Vector2.Distance(this.transform.position, erizo.transform.position) <= this.turretRadius;


    }
    private void shootToPosition(Vector2 target)
    {
        Vector2 v = target - (Vector2)this.transform.position;

        GameObject inst = Instantiate(rock, this.transform.position + transform.up * -5, rock.transform.rotation);
        RockController bc = inst.GetComponent<RockController>();
        bc.setVelocity(v.normalized * this.bulletSpeed);
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, turretRadius);
    }

}
