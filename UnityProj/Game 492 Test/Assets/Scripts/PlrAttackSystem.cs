using System;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.InputSystem;

public enum AttackType
{
    None,
    Base,
    Attack2,
    Attack3 // NOTE: During Attack3, type is Attack3. When Attack3 is over, Type set back to None
}
public enum CustomAnimationPhaseType
{
    None,
    StartingPhase,
    ActionPhase,
    LeavingPhase,
}

public class PlrAttackSystem : MonoBehaviour
{
    Animator animator; //assuming i need this to change anims
                       // CharacterSkinController does ```animator.SetTrigger(str trigger);```

    float tSinceLastPerfAttack = 0f; // will store to compare old and next Time.time
    float tWindowToAllowNxtAttack = 1f;
    AttackType lastAttackType = AttackType.None; // None, Base, 2nd, 3rd

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void DoNormalAttack()
    {
        print("Normal Attack!");
        lastAttackType = AttackType.Base;
        tSinceLastPerfAttack = Time.time;

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
    }
    private void Do3rdAttack()
    {
        lastAttackType = AttackType.Attack3;
        print("3rd Attack!");
        tSinceLastPerfAttack = Time.time;

        // ---- //
        lastAttackType = AttackType.None;
    }

    public void InputFunc_Attack(InputAction.CallbackContext context) // event needs to be connected to EventSystem GameObj. -> PlayerInput
    {
        if (!context.performed) { return; } // so below then if it's perf code below will run

        // Check for Normal or Another Type of Attack Needed
        print(Time.time - tSinceLastPerfAttack);
        if (!((Time.time - tSinceLastPerfAttack) < tWindowToAllowNxtAttack) || (lastAttackType == AttackType.None) ) {
            // if NOT [within time window to allow next attack] OR [lastAttack was set to None meaning a chain alr happened or time passed]
            // -so do normal attack work
            DoNormalAttack();
            return;
        }
        // 0000 //

        // Here on means non-normal attack:
        if (lastAttackType == AttackType.None) // None -> Base
        {
            DoNormalAttack();
        }
        else if (lastAttackType == AttackType.Base) // Base -> 2nd
        {
            Do2ndAttack();
        }
        else if (lastAttackType == AttackType.Attack2) // 2nd -> 3rd
        {
            Do3rdAttack(); 
        }
        else if (lastAttackType == AttackType.Attack3)
        {
            // in middle of perf attack3
            return;
        }
        else {
            print("Wrong string given!");
            // wrong str given
        }


        //context.phase = InputActionPhase.Performed; // Started, Waiting, Performed, Canceled, Disabled
        //context.duration = 0;
        //context.performed // shorthand for phase?
        //context.startTime;
        //context.time;
        //context.ReadValue();
        //context.started // shorthand for phase?
    }

}
