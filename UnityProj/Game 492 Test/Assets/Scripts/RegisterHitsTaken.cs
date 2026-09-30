using Unity.VisualScripting;
using UnityEngine;

public class RegisterHitsTaken : MonoBehaviour
{
    HealthSystem healthSystemRef;
    string currentTag = "";
    public string strToAllow = "";


    private void Start()
    {
        currentTag = this.gameObject.tag;
        healthSystemRef = GetComponent<HealthSystem>();
    }

    private void OnTriggerEnter(Collider other)
    {
        print("OnTriggerEntered: " + other.tag + "| Allowed: " + strToAllow);
        if (other.CompareTag(strToAllow))
        {
            print("took hit!");
            healthSystemRef.AddOrSubToHealth(-5); // need to change thing to be data-driven
        }
    }
}
