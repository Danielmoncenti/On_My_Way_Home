using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //Camera
    private Camera _camera;

    //What we are following
    private Transform Spikey;
 
    //Zeros out the velocity
    private Vector3 velocity = Vector3.zero;

    //Time to follow target
    private float smoothTime = .15f;

    //enable and set the maxium Y value
    private bool YMaxEnabled = true;
    private float YMaxValue = 100;

    //enable and set the min Y value
    private bool YMinEnabled = true;
    private float YMinValue = -10;

    //enable and set the maxium X value
    private bool XMaxEnabled = false;
    private float XMaxValue = 0;

    //enable and set the min X value
    private bool XMinEnabled = true;
    private float XMinValue = 0;

    //pixels
    private float pixelToUnits = 40.0f;


    // Start is called before the first frame update
    void Start()
    {
        _camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        Spikey = GameObject.Find("Spikey").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        /*float Spikey_x = Spikey.transform.position.x;
        float Spikey_y = Spikey.transform.position.y;

        float rounded_x = RoundToNearestPixel(Spikey_x);
        float rounded_y = RoundToNearestPixel(Spikey_y);

        Vector3 new_pos = new Vector3(rounded_x, rounded_y, -10.0f); //-10 por la posicion de la camara
        //transform.position = new_pos;

        transform.position = Vector3.SmoothDamp(transform.position, new_pos, ref velocity, smoothTime);*/
    }

    private void FixedUpdate()
    {
        //target position
        Vector3 targetPos = Spikey.position;

        //Vertical
        if (YMinEnabled && YMaxEnabled)
        {
            targetPos.y = Mathf.Clamp((int)Spikey.position.y, (int)YMinValue, (int)YMaxValue);
        }
        else if (YMinEnabled)
        {
            targetPos.y = Mathf.Clamp((int)Spikey.position.y, (int)YMinValue, (int)Spikey.position.y);

        }
        else if (YMaxEnabled)
        {
            targetPos.y = Mathf.Clamp((int)Spikey.position.y, (int)Spikey.position.y, (int)YMaxValue);
        }

        //Horizontal
        if (XMinEnabled && XMaxEnabled)
        {
            targetPos.x = Mathf.Clamp((int)Spikey.position.x, (int)XMinValue, (int)XMaxValue);
        }
        else if (XMinEnabled)
        {
            targetPos.x = Mathf.Clamp((int)Spikey.position.x, (int)XMinValue, (int)Spikey.position.x);

        }
        else if (XMaxEnabled)
        {
            targetPos.x = Mathf.Clamp((int)Spikey.position.x, (int)Spikey.position.x, (int)XMaxValue);
        }



        //align the camera and the targets z position
        targetPos.z = _camera.transform.position.z;

        //using smooth daap we will gradually change the camera transform position to the target position based on the cameras transform velocity and our smooth time
        _camera.transform.position = Vector3.SmoothDamp(_camera.transform.position, targetPos, ref velocity, smoothTime);

    }

    /*public float RoundToNearestPixel(float unityUnits)
    {
        float valueInPixels = unityUnits * pixelToUnits;
        valueInPixels = Mathf.Round(valueInPixels);
        float roundedUnityUnits = valueInPixels * (1 / pixelToUnits);
        return roundedUnityUnits;
    }*/
}
