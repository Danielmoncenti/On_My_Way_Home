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
                SceneManager.LoadScene("MainMenu"); 
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
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
