using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerController : MonoBehaviour
{
    [SerializeField] GameObject GameCamera;
    [SerializeField] GameObject Spikey;
    private SpikeyController spikeyController;

    private BoxCollider2D boxCollider;

    // Start is called before the first frame update
    void Start()
    {
        spikeyController = Spikey.GetComponent<SpikeyController>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
