using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerController : MonoBehaviour
{
    [SerializeField] GameObject gameCamera;
    [SerializeField] GameObject Spikey;
    private SpikeyController spikeyController;

    // Start is called before the first frame update
    void Start()
    {
        spikeyController = Spikey.GetComponent<SpikeyController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
