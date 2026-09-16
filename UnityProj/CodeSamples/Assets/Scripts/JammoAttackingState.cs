using UnityEngine;
using static UnityEngine.ParticleSystem;

public class JammoAttackingState : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        MovementInput movementInput = animator.gameObject.GetComponent<MovementInput>();
        if (movementInput != null)
        {
            movementInput.EnableDirectionChange = false;
        }
        //Debug.Log("Attacking ENTER");
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        MovementInput movementInput = animator.gameObject.GetComponent<MovementInput>();
        if (movementInput != null)
        {
            movementInput.EnableDirectionChange = true;
        }
        //Debug.Log("Attacking EXIT");
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Debug.Log("Attacking UPDATE");
    }

    override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Debug.Log("Attacking MOVE");
    }

    override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Debug.Log("Attacking IK");
    }
}
