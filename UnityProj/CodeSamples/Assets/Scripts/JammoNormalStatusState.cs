using UnityEngine;
using static UnityEngine.ParticleSystem;

public class JammoNormalStatusState : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("attack");

        MovementInput movementInput = animator.gameObject.GetComponent<MovementInput>();
        if (movementInput != null)
        {
            movementInput.EnableVelocity = true;
        }
        //Debug.Log("NormalStatus ENTER");
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        MovementInput movementInput = animator.gameObject.GetComponent<MovementInput>();
        if (movementInput != null)
        {
            movementInput.EnableVelocity = false;
        }
        //Debug.Log("NormalStatus EXIT");
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Debug.Log("NormalStatus UPDATE");
    }

    override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Debug.Log("NormalStatus MOVE");
    }

    override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Debug.Log("NormalStatus IK");
    }
}
