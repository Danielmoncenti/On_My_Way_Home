using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TextManager : MonoBehaviour
{
    [TextArea(5,10)]
    public string story_text;
    public Text text_to_show;
    public Text text_space;
    public Text text_esc;

    float timer;
    int index;
    int text_length;

    string complete_text;
    string actual_text;

    bool isComplete;
    bool escaped;

    TransitionsController transition;

    private void Awake()
    {
        transition = GameObject.FindGameObjectWithTag("Transitor").GetComponent<TransitionsController>();
    }
    // Start is called before the first frame update
    void Start()
    {
        text_esc.enabled = false;
        text_space.enabled = false;
        text_to_show.text = "";
        text_length = story_text.Length;
        timer = 0;
        index = 0;
        complete_text = "";
        actual_text = "";
        isComplete = false;
        escaped = false;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (index < text_length)
        {
            if (timer >= 0.05)
            {
                actual_text = actual_text + story_text[index];
                text_to_show.text = actual_text;
                index++;
                timer = 0;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isComplete)
            {
                transition.a_out = true;
            }
            else
            {
                for (int i = 0; i < text_length; i++)
                {
                    complete_text = complete_text + story_text[i];
                }

                text_to_show.text = complete_text;
                index = text_length;
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape) && escaped)
        {
            SceneManager.LoadScene("MainMenu");
            //CAMBIAR MUSICA
        }

        if (Input.GetKeyDown(KeyCode.Escape) && !escaped)
        {
            text_esc.enabled = true;
            escaped = true;
        }

        if (index == text_length)
        {
            text_space.enabled = true;
            isComplete = true;
        }

    }

}
