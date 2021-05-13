using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManaerInstancer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("empiezo el game instancer");
        GameManagerController gmc = FindObjectOfType<GameManagerController>();
        if (gmc == null)
        {
            Instantiate(Resources.Load("GameManager"));
        }
    }

}
