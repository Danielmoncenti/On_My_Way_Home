using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckManagers : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManagerController gmc = FindObjectOfType<GameManagerController>();
        if (gmc != null)
        {
            Destroy(gmc.gameObject);
        }
    }

}
