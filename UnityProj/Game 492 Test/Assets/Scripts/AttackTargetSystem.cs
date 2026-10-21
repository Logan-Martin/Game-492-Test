using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;

public class AttackTargetSystem : MonoBehaviour
{
    public GameObject TargetFolder;
    public string strToLookFor = ""; // set in editor
    public float maxConsiderationRange = 50f; 

    public GameObject GetClosestAliveTarget() // can be null as case of unfound
    {
        GameObject closestTarg = null;
        float distOfLastClosest = 0.0f;
        foreach (Transform targ in TargetFolder.transform)
        {
            if (!targ.gameObject.CompareTag(strToLookFor)) { continue; }

            float healthCheck = targ.gameObject.GetComponent<HealthSystem>().GetHealth();
            //print("Health check: " + healthCheck);
            if (healthCheck == 0) {
                // could maybe do a sub list of dead enemy, then after the foreach loop auto remove dead ones
                // instead of just relying on the OnTriggerExit to remove them generally
                continue;
            } // if targ dead, skip checking

            float distOfThis = (targ.gameObject.transform.localPosition - this.gameObject.transform.localPosition).sqrMagnitude;
            if (distOfThis <= distOfLastClosest || distOfLastClosest == 0.0f)
            {
                distOfLastClosest = distOfThis;
                closestTarg = targ.gameObject;
            }
        }

        if (distOfLastClosest > maxConsiderationRange) {
            print("Warn - closestTarg is not within range!");
            return null;
        }

        if (closestTarg == null)
        {
            print("Warn - closestTarg is null!!!");
        }
        else
        {
            print("Got closest Target!");
        }

        return closestTarg;
    }

}
