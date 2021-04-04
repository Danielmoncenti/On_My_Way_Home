using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //What we are following
    [SerializeField] Transform Spikey;
 
    //Zeros out the velocity
    private Vector3 velocity = Vector3.zero;

    //Time to follow target
    public float smoothTime = .15f;

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

    //pixels
    public float pixelToUnits = 40.0f;


    // Start is called before the first frame update
    void Start()
    {
        
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
            targetPos.y = Mathf.Clamp(Spikey.position.y, YMinValue, YMaxValue);
        }
        else if (YMinEnabled)
        {
            targetPos.y = Mathf.Clamp(Spikey.position.y, YMinValue, Spikey.position.y);

        }
        else if (YMaxEnabled)
        {
            targetPos.y = Mathf.Clamp(Spikey.position.y, Spikey.position.y, YMaxValue);
        }

        //Horizontal
        if (XMinEnabled && XMaxEnabled)
        {
            targetPos.x = Mathf.Clamp(Spikey.position.x, XMinValue, XMaxValue);
        }
        else if (XMinEnabled)
        {
            targetPos.x = Mathf.Clamp(Spikey.position.x, XMinValue, Spikey.position.x);

        }
        else if (XMaxEnabled)
        {
            targetPos.x = Mathf.Clamp(Spikey.position.x, Spikey.position.x, XMaxValue);
        }



        //align the camera and the targets z position
        targetPos.z = transform.position.z;

        //using smooth daap we will gradually change the camera transform position to the target position based on the cameras transform velocity and our smooth time
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime);

    }

    public float RoundToNearestPixel(float unityUnits)
    {
        float valueInPixels = unityUnits * pixelToUnits;
        valueInPixels = Mathf.Round(valueInPixels);
        float roundedUnityUnits = valueInPixels * (1 / pixelToUnits);
        return roundedUnityUnits;
    }
}
