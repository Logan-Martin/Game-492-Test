using UnityEngine;
using UnityEngine.InputSystem;

public class CombatInput : MonoBehaviour
{
    MovementInput movementInput;
    Animator animator;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        movementInput = GetComponent<MovementInput>();
    }

    // Update is called once per frame
    void Update()
    {
        InputAction attackAction = InputSystem.actions.FindAction("Attack");
        if(attackAction.WasPressedThisFrame())
        {
            if(animator != null)
            {
                animator.SetTrigger("attack");
            }
        }
    }
        

    void SetForcedForwardSpeed(float speed)
    {
        if(movementInput != null)
        {
            movementInput.ForcedForwardSpeed = speed;
        }
    }
}
