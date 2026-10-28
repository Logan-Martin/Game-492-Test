using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class RegisterHitsTaken : MonoBehaviour
{
    public HealthSystem healthSystemRef;
    string currentTag = "";
    public string strToAllow = "";
    // --- //
    public GameObject hitVFX_Prefab_Ref;
    // ----- //


    private void Start()
    {
        currentTag = this.gameObject.tag;
        print("TEST!");
    }

    private void DoGettingHurtStuff(Collider other)
    {
        if (healthSystemRef.GetHealth() > 0) // if alive check
        {
            print("took hit!");

            Transform transform_PARENT = other.gameObject.transform;
            Transform transformToSet = other.gameObject.transform;
            GameObject hitVFX_Clone = Instantiate(hitVFX_Prefab_Ref, transform_PARENT.position, Quaternion.identity);

            healthSystemRef.AddOrSubToHealth(-25); // need to change thing to be data-driven
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //print(other);
        print("OnTriggerEntered: " + other.tag + "| Allowed: " + strToAllow);
        if (other.CompareTag(strToAllow) == true)
        {
            DoGettingHurtStuff(other);


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
