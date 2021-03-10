using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeySpawner : MonoBehaviour
{
    public GameObject erizo;
    KeyCode respawn = KeyCode.X;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(respawn))
        {
            GameObject newerizo = Instantiate(erizo, transform.position, transform.rotation);
        }
    }
}
