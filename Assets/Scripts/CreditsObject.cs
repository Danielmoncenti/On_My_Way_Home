using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsObject : MonoBehaviour
{
    Rigidbody2D rigidBody;
    [SerializeField] float speed;
    [SerializeField] bool quiet = false;
    float esc;

    TransitionsController transition;

    private void Awake()
    {
        transition = GameObject.FindGameObjectWithTag("Transitor").GetComponent<TransitionsController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.velocity = -transform.up * speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (quiet)
        {
            rigidBody.velocity = Vector3.zero;
            esc += Time.deltaTime;
            if (esc > 2) {
                /*if (SectionManager.SInstance != null && GameManager.GInstance != null)
                {
                    Destroy(SectionManager.SInstance.gameObject);
                    Destroy(GameManager.GInstance.gameObject);
                }
                else if (SoundManager.SoInstance != null)
                {
                    Destroy(SoundManager.SoInstance.gameObject);
                }*/
                transition.a_out = true;
                transition.nextscene = "MainMenu";
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            transition.a_out = true;
            transition.nextscene = "MainMenu";
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Water")
        {
            quiet = true;
        }
    }
}
