using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartsController : MonoBehaviour
{
    //Animacion de las vidas
    private Animator animator;
    private int One_to_N_animation;
    private int One_Iddle_animation;
    private int One_to_Two_animation;
    private int Two_to_One_animation;
    private int Two_Iddle_animation;
    private int Two_to_Three_animation;
    private int Three_to_Two_animation;
    private int Three_Iddle_animation;
    private int Three_to_Four_animation;
    private int Four_to_Three_animation;
    private int Four_Iddle_animation;
    private int Four_to_Five_animation;
    private int Five_to_Four_animation;
    private int Five_Iddle_animation;
    private int Five_to_Six_animation;
    private int Six_to_Five_animation;
    private int Six_Iddle_animation;
    public bool Iddle = true;
    public bool OneUp = false;
    public bool OneDown = false;
    public bool TryAgain = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        One_to_N_animation = Animator.StringToHash("OneDown");
        One_Iddle_animation = Animator.StringToHash("Iddle");
        One_to_Two_animation = Animator.StringToHash("OneUp");
        Two_to_One_animation = Animator.StringToHash("OneDown");
        Two_Iddle_animation = Animator.StringToHash("Iddle");
        Two_to_Three_animation = Animator.StringToHash("OneUp");
        Three_to_Two_animation = Animator.StringToHash("OneDown");
        Three_Iddle_animation = Animator.StringToHash("Iddle");
        Three_Iddle_animation = Animator.StringToHash("TryAgain");
        Three_to_Four_animation = Animator.StringToHash("OneUp");
        Four_to_Three_animation = Animator.StringToHash("OneDown");
        Four_Iddle_animation = Animator.StringToHash("Iddle");
        Four_to_Five_animation = Animator.StringToHash("OneUp");
        Five_to_Four_animation = Animator.StringToHash("OneDown");
        Five_Iddle_animation = Animator.StringToHash("Iddle");
        Five_to_Six_animation = Animator.StringToHash("OneUp");
        Six_to_Five_animation = Animator.StringToHash("OneDown");
        Six_Iddle_animation = Animator.StringToHash("Iddle");
    }

    // Update is called once per frame
    void Update()
    {
        OneUp = GameManagerController.Instance.OneUp;
        OneDown = GameManagerController.Instance.OneDown;
        TryAgain = GameManagerController.Instance.TryAgain;

        animator.SetBool(One_to_N_animation, OneDown);
        animator.SetBool(One_Iddle_animation, Iddle);
        animator.SetBool(One_to_Two_animation, OneUp);
        animator.SetBool(Two_to_One_animation, OneDown);
        animator.SetBool(Two_Iddle_animation, Iddle);
        animator.SetBool(Two_to_Three_animation, OneUp);
        animator.SetBool(Three_to_Two_animation, OneDown);
        animator.SetBool(Three_Iddle_animation, TryAgain);
        animator.SetBool(Three_Iddle_animation, Iddle);
        animator.SetBool(Three_to_Four_animation, OneUp);
        animator.SetBool(Four_to_Three_animation, OneDown);
        animator.SetBool(Four_Iddle_animation, Iddle);
        animator.SetBool(Four_to_Five_animation, OneUp);
        animator.SetBool(Five_to_Four_animation, OneDown);
        animator.SetBool(Five_Iddle_animation, Iddle);
        animator.SetBool(Five_to_Six_animation, OneUp);
        animator.SetBool(Six_to_Five_animation, OneDown);
        animator.SetBool(Six_Iddle_animation, Iddle);
    }

    public void SetToIddle() 
    {
        GameManagerController.Instance.OneDown = false;
        GameManagerController.Instance.OneUp = false; 
        Iddle = true; 
    }

    private void SetTryAgain() { GameManagerController.Instance.TryAgain = false; }

    private void LifeDown() { GameManagerController.Instance.OneDown = false; }

    private void LifeUp() { GameManagerController.Instance.OneUp = false; }
}
