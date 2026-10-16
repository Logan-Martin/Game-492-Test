using System.Collections;
using UnityEngine;

public class ApplyRootTransformTowardPoint : MonoBehaviour
{
    float maxDistToPush = 10f * 10f; // actually 10f, but using sqrMagnitude
    public Animator animatorRef;
    public MovementInput movementInput_ScriptRef;

    public void ApplyRootTransOnCharToPoint(Vector3 posToGoTo)
    {
        // bayoneta when attacking
        // faces enemy targ, plays a sliding animation based on dist to travel w/ max slide 
        // when in this phase, plr can cancel w/ jump, but takes away joystick movement while sliding

        float temp = (posToGoTo - this.transform.localPosition).sqrMagnitude;
        if (temp < maxDistToPush) // allow
        {
            //movementInput_ScriptRef.TogglePlayerMovement(false); // alr done by attack anim stuff but still prob have to override that
            movementInput_ScriptRef.blockRotationPlayer = true;
            movementInput_ScriptRef.needToStepAppliedRootMotion = true;
            
            // Movement work:
            movementInput_ScriptRef.desiredMoveDirection = (posToGoTo - this.transform.localPosition).normalized;
            // setting here ^^^^^ so its one value that doesn't change each frame

            // clean up done in MovementInput.cs when close enough or timedOut
            // movementInput_ScriptRef.stTime_AppRtMotion = Time.time;
        }
        else
        {
            print("Note - Can't apply Root transform to Target because too far!!");
        }
    }
}
