using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class RegisterHitsTaken : MonoBehaviour
{
    public HealthSystem healthSystemRef;
    string currentTag = "";
    public string strToAllow = "";


    private void Start()
    {
        currentTag = this.gameObject.tag;
        print("TEST!");
    }

    private void DoGettingHurtStuff()
    {
        print("took hit!");
        healthSystemRef.AddOrSubToHealth(-50); // need to change thing to be data-driven
    }

    private void OnTriggerEnter(Collider other)
    {
        //print(other);
        print("OnTriggerEntered: " + other.tag + "| Allowed: " + strToAllow);
        if (other.CompareTag(strToAllow) == true)
        {
            DoGettingHurtStuff();


            //AttackSystem ref1 = other.gameObject.GetComponent<AttackSystem>();
            //if (ref1 == null) { 
            //    print("error! can't find AttackSystem");
            //    return;
            //}

            //if (ref1.isThisCharCurrAttacking == true)
            //{
            //DoGettingHurtStuff();
            //}
        }
    }

}
