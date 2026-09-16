using UnityEngine;
using UnityEngine.InputSystem;

public class CombatInput : MonoBehaviour
{
    Animator animator;
    public GameObject hitBox_LeftHand;
    public GameObject hitBox_RightHand;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();

        if(hitBox_LeftHand != null)
        {
            hitBox_LeftHand.SetActive(false);
        }

        if (hitBox_RightHand != null)
        {
            hitBox_RightHand.SetActive(false);
        }
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

    void EnableHitBox_LeftHand()
    {
        if (hitBox_LeftHand != null)
        {
            hitBox_LeftHand.SetActive(true);
        }
    }

    void DisableHitBox_LeftHand()
    {
        if (hitBox_LeftHand != null)
        {
            hitBox_LeftHand.SetActive(false);
        }
    }

    void EnableHitBox_RightHand()
    {
        if (hitBox_RightHand != null)
        {
            hitBox_RightHand.SetActive(true);
        }
    }

    void DisableHitBox_RightHand()
    {
        if (hitBox_RightHand != null)
        {
            hitBox_RightHand.SetActive(false);
        }
    }
}
