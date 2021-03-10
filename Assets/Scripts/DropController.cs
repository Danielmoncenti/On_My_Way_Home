using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropController : MonoBehaviour
{
    //animacion
    //sonido

    // Start is called before the first frame update
    void Start()
    {
        //animacion
        //sonido
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Tilemap")
        {
            Destroy(gameObject);
           //animacion
           //sonido
        }


    }
}
