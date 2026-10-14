using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AttackTargetSystem : MonoBehaviour
{
    List<GameObject> targsList = new List<GameObject>();
    public string strToLookFor = ""; // set in editor
    
    // could have a variable for closestTarg and update it or GetClosestTarget() function or both?
    
    //public float maxConsiderationRange; // not act used. SphereCollider itself handles by default kinda
    //void Start()
    //{
    //    maxConsiderationRange = this.gameObject.GetComponent<SphereCollider>().radius;
    //}



    // ---- //
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(strToLookFor)) // to exclude registering every trigger interaction to some extent
        {
            GameObject otherGameObj = other.gameObject.transform.parent.gameObject;
            if (otherGameObj.GetComponent<HealthSystem>().GetHealth() == 0) { return; } // if dead, skip adding

            if (!targsList.Contains(otherGameObj)) // if it's not already in the list
            {
                print("Added potential target!");
                targsList.Add(otherGameObj);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        GameObject otherGameObj = other.gameObject;
        if (targsList.Contains(otherGameObj)) // if in list of targs, make sure to remove it
        {
            print("Removed potential target!");
            targsList.Remove(otherGameObj);
        }
    }

    public GameObject GetClosestAliveTarget() // can be null as case of unfound
    {
        GameObject closestTarg = null;
        float distOfLastClosest = 0.0f;
        foreach (GameObject targ in targsList)
        {
            if (targ.GetComponent<HealthSystem>().GetHealth() == 0) {
                // could maybe do a sub list of dead enemy, then after the foreach loop auto remove dead ones
                // instead of just relying on the OnTriggerExit to remove them generally
                continue;
            } // if targ dead, skip checking

            float distOfThis = (targ.transform.localPosition - this.gameObject.transform.localPosition).sqrMagnitude;
            if (distOfThis <= distOfLastClosest || distOfLastClosest == 0.0f)
            {
                distOfLastClosest = distOfThis;
                closestTarg = targ;
            }
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
