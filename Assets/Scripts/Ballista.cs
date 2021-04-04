using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ballista : MonoBehaviour
{

    private Rigidbody2D rb2d;
    private BoxCollider2D bc2d;
    public GameObject erizo;
    public GameObject arrow;
    public float turretRadius;
    private Vector2 arrowSpeed;
    private bool canShoot;

    // Start is called before the first frame update
    void Start()
    {
    
        canShoot = true;
        arrowSpeed = new Vector2(500,0);
        turretRadius = 250;
        rb2d = GetComponent<Rigidbody2D>();
        bc2d = GetComponent<BoxCollider2D>();

    }



    // Update is called once per frame
    void Update()
    {
        if (checkSpikeyPosition()==true)
        {
            if (canShoot==true)
            {
             toShoot();
            }
        }
    }
    private bool checkSpikeyPosition()
    {

        return Vector2.Distance(rb2d.transform.position, erizo.transform.position) <=turretRadius;


    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, turretRadius);
    }
    private void toShoot()
    {
        float centery = (bc2d.bounds.min.y + bc2d.bounds.max.y) / 2;
        Vector2 positioncenter = new Vector2(bc2d.bounds.min.x - 5, centery);
        RaycastHit2D[] hitscenter = Physics2D.RaycastAll(positioncenter, Vector2.right * -1, 500);
        foreach(RaycastHit2D hit in hitscenter)
        {
            if (hit.collider != null && hit.collider.gameObject.tag == "Spikey")
            {
                GameObject inst = Instantiate(arrow, rb2d.transform.position + transform.right * -5+transform.up *5, arrow.transform.rotation);
                ArrowController bc = inst.GetComponent<ArrowController>();
                bc.setVelocity(arrowSpeed*-1);
                SoundManager.PlaySound("BoundTrap");

                canShoot = false;
            }
        }
  
    }
}
