using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraReact : MonoBehaviour
{
    //Camera
    [SerializeField] CameraFollow _camera;

    //enable and set the maxium Y value
    public bool YMaxEnabled = false;
    public float YMaxValue = 0;

    //enable and set the min Y value
    public bool YMinEnabled = false;
    public float YMinValue = 0;

    //enable and set the maxium X value
    public bool XMaxEnabled = false;
    public float XMaxValue = 0;

    //enable and set the min X value
    public bool XMinEnabled = false;
    public float XMinValue = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Control")
        {
            Debug.Log("HOLA");
            _camera.YMaxEnabled = YMaxEnabled;
            _camera.YMaxValue = YMaxValue;

            _camera.YMinEnabled = YMinEnabled;
            _camera.YMinValue = YMinValue;

            _camera.XMaxEnabled = XMaxEnabled;
            _camera.XMaxValue = XMaxValue;

            _camera.XMinEnabled = XMinEnabled;
            _camera.XMinValue = XMinValue;

        }
    }
}
