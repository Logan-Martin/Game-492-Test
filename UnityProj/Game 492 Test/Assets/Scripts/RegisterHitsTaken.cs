using Unity.VisualScripting;
using UnityEngine;

public class RegisterHitsTaken : MonoBehaviour
{
    public HealthSystem healthSystemRef;
    string currentTag = "";
    public string strToAllow = "";


    private void Start()
    {
        currentTag = this.gameObject.tag;
        //healthSystemRef = GetComponent<HealthSystem>();
    }

    private void DoGettingHurtStuff()
    {
        print("took hit!");
        healthSystemRef.AddOrSubToHealth(-5); // need to change thing to be data-driven
    }

    private void OnTriggerEnter(Collider other)
    {
        //print("OnTriggerEntered: " + other.tag + "| Allowed: " + strToAllow);
        if (other.CompareTag(strToAllow) == true)
        {
            AttackSystem ref1 = other.gameObject.GetComponent<AttackSystem>();
            if (ref1 == null) { 
                print("error! can't find AttackSystem");
                return;
            }

            if (ref1.isThisCharCurrAttacking == true)
            {
                DoGettingHurtStuff();
            }
        }
    }

}
