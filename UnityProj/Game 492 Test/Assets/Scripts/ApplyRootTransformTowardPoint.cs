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

        float temp = (posToGoTo - this.transform.position).sqrMagnitude;
        if (temp < maxDistToPush) // allow
        {
            movementInput_ScriptRef.blockRotationPlayer = true;
            movementInput_ScriptRef.needToStepAppliedRootMotion = true; // starts in Update()

            // Movement work:
            Vector3 vectorToEnemy = (posToGoTo - this.transform.position);
            if (vectorToEnemy == Vector3.zero)
            {
                // this means no targ basically
                movementInput_ScriptRef.SPEED_AppRtMotion = 5f;
                //movementInput_ScriptRef.desiredMoveDirection = posToGoTo.normalized;
            }
            else
            {
                movementInput_ScriptRef.SPEED_AppRtMotion = 10f;
                movementInput_ScriptRef.desiredMoveDirection = vectorToEnemy.normalized;
            }
            //Debug.DrawLine(this.transform.position, this.transform.position + vectorToEnemy, Color.red);
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
