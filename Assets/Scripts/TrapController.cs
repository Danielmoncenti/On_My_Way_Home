using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapController : MonoBehaviour
{
    [SerializeField] GameObject Spikey;
    private Animator animator;
    private int itsAtrap_animation;
    private int isWaiting_animation;
    private bool isTrapped = false;
    private bool isWaiting = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        itsAtrap_animation = Animator.StringToHash("isTrapped");
        isWaiting_animation = Animator.StringToHash("isWaiting");
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool(itsAtrap_animation, isTrapped);
        animator.SetBool(isWaiting_animation, isWaiting);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Spikey")
        {
            isTrapped = true;
            SoundManager.PlaySound("Trap");
        }
    }

    private void FreezeAnimation() { isWaiting = true; }

    private void ResetAnimation() { isTrapped = false; }

    private void GoIddle() { isWaiting = false; }
}
