using UnityEngine;
using UnityEngine.InputSystem;

public class CombatInput : MonoBehaviour
{
    Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
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
}
