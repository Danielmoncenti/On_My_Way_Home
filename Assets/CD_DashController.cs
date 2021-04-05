using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CD_DashController : MonoBehaviour
{
    private Animator animator;
    private int BasicDashIddle_animation;
    private int BasicDashUsed_animation;
    private int BasicDashComing_animation;
    private int DobleDashIddle_animation;
    private int DobleDashUsed_animation;
    private int DobleDashComing_animation;
    private int ShadowDashIddle_animation;
    private int ShadowDashUsed_animation;
    private int ShadowDashComing_animation;
    private bool isUsed = false;
    private bool isComing = false;
    private bool basicDash = false;
    private bool dobleDash = false;
    private bool shadowDash = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        BasicDashIddle_animation = Animator.StringToHash("isComing");
        BasicDashIddle_animation = Animator.StringToHash("BasicDash");
        BasicDashUsed_animation = Animator.StringToHash("isUsed");
        BasicDashComing_animation = Animator.StringToHash("isComing");

        DobleDashIddle_animation = Animator.StringToHash("isComing");
        DobleDashIddle_animation = Animator.StringToHash("DobleDash");
        DobleDashUsed_animation = Animator.StringToHash("isUsed");
        DobleDashComing_animation = Animator.StringToHash("isComing");

        ShadowDashIddle_animation = Animator.StringToHash("isComing");
        ShadowDashIddle_animation = Animator.StringToHash("ShadowDash");
        ShadowDashUsed_animation = Animator.StringToHash("isUsed");
        ShadowDashComing_animation = Animator.StringToHash("isComing");
    }

    // Update is called once per frame
    void Update()
    {




        animator.SetBool(BasicDashIddle_animation, isComing);
        animator.SetBool(BasicDashIddle_animation, basicDash);
        animator.SetBool(BasicDashUsed_animation, isUsed);
        animator.SetBool(BasicDashComing_animation, isComing);

        animator.SetBool(DobleDashIddle_animation, isComing);
        animator.SetBool(DobleDashIddle_animation, dobleDash);
        animator.SetBool(DobleDashUsed_animation, isUsed);
        animator.SetBool(DobleDashComing_animation, isComing);

        animator.SetBool(ShadowDashIddle_animation, isComing);
        animator.SetBool(ShadowDashIddle_animation, shadowDash);
        animator.SetBool(ShadowDashUsed_animation, isUsed);
        animator.SetBool(ShadowDashComing_animation, isComing);

    }

    private void SetToIddle() { isComing = true; }

    private void Recharge() { isUsed = false; }
}
