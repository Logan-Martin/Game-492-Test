using System;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.InputSystem;


public class EnemyMoveEventCatcher : MonoBehaviour
{
    public Animator animator; //assuming i need this to change anims
                              // CharacterSkinController does ```animator.SetTrigger(str trigger);```
    //public MovementInput movementInputScript_Ref;
    public GameObject punchHitbox_R;
    public GameObject punchHitbox_L;
    public GameObject hurrKickHitbox;

    public bool isThisCharCurrAttacking = false;

    float tSinceLastPerfAttack = 0f; // will store to compare old and next Time.time
    float tWindowToAllowNxtAttack = 1f;
    AttackType lastAttackType = AttackType.None; // None, Base, 2nd, 3rd
    CustomAnimationPhaseType lastAnimPhase = CustomAnimationPhaseType.None;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Attack Animation Events: // For Animation Phase State
    public void AttackAnim_Starting()
    {
        lastAnimPhase = CustomAnimationPhaseType.StartingPhase;
        //movementInputScript_Ref.TogglePlayerMovement(false);
    }
    public void AttackAnim_Action(string str)
    {
        lastAnimPhase = CustomAnimationPhaseType.ActionPhase;

        isThisCharCurrAttacking = true;

        print(str);
        if (str == "" || str == null)
        {
            print("Action anim event fired but no str data was given!");
        }
        else if (str == "BothHands")
        {
            punchHitbox_L.SetActive(true);
            punchHitbox_R.SetActive(true);
        }
        else if (str == "LeftHand")
        {
            punchHitbox_L.SetActive(true);
        }
        else if (str == "RightHand")
        {
            punchHitbox_R.SetActive(true);
        }
        else if (str == "TornadoKick")
        {
            hurrKickHitbox.SetActive(true);
        }
        else
        {
            print("Action anim event fired but not in cases to catch. String typed wrong?");
        }
        // can attack / do dmg to another now
    }
    public void AttackAnim_Leaving()
    {
        lastAnimPhase = CustomAnimationPhaseType.LeavingPhase;
        //movementInputScript_Ref.TogglePlayerMovement(true);
        isThisCharCurrAttacking = false;
        // ---- //
        punchHitbox_L.SetActive(false);
        punchHitbox_R.SetActive(false);
        hurrKickHitbox.SetActive(false);
        // ---- //
        //lastAttackType = AttackType.None; //? no cause logic on combo w/ timing
    }
    // 00000 //


    private void DoNormalAttack()
    {
        print("Normal Attack!");
        lastAttackType = AttackType.Base;
        tSinceLastPerfAttack = Time.time;
        animator.SetTrigger("PlayAttackAnim");

        // play anim
        // play audio?
        // enable VFX

        // clean up
    }
    private void Do2ndAttack()
    {
        print("2nd Attack!");
        lastAttackType = AttackType.Attack2;
        tSinceLastPerfAttack = Time.time;
        animator.SetTrigger("PlayAttackAnim2");
    }
    private void Do3rdAttack()
    {
        lastAttackType = AttackType.Attack3;
        print("3rd Attack!");
        tSinceLastPerfAttack = Time.time;
        animator.SetTrigger("PlayAttackAnim3");
        // ---- //
    }
    private void ToggleAttackHitbox_R()
    {
        punchHitbox_R.SetActive(!punchHitbox_R.activeSelf);
    }
    private void ToggleAttackHitbox_L()
    {
        punchHitbox_L.SetActive(!punchHitbox_L.activeSelf);
    }
}
