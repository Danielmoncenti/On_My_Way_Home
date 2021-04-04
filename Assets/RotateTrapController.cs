using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTrapController : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        float delta = Time.fixedDeltaTime * 1000;

        this.transform.Rotate(0, 0, 2);
    }
}
