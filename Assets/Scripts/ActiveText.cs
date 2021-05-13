using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActiveText : MonoBehaviour
{
    //Rigidbody2D rigidBody;
    //BoxCollider2D boxCollider;
    Animator animator;
    public Text text;
    public GameObject Spikey;
    public GameObject Clouds;
    bool nomore;
    bool Move;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        Move = false;
        nomore = false;
        
        text.enabled = false;
        Clouds.SetActive(false);
    }

    private void Update()
    {
        animator.SetBool("Move", Move);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Spikey" && !nomore)
        {
            text.enabled = true;
            Move = true;
            Clouds.SetActive(true);
            nomore = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Spikey")
        {
            text.enabled = false;
            Clouds.SetActive(false);
        }
    }
}
