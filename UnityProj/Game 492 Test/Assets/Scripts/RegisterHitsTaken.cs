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
        print("OnTriggerEntered: " + other.tag + "| Allowed: " + strToAllow);
        if (other.CompareTag(strToAllow) == true)
        {
            if (other.tag == "Enemy")
            {
                // then the enemy is touching plr
                // so we want to check enemy's EnemyMoveEventCatcher
                EnemyMoveEventCatcher ref1 = other.gameObject.GetComponent<EnemyMoveEventCatcher>();
                if (ref1 == null) { print("error! can't find EnemyMoveEventCatcher"); }

                if (ref1.isThisCharCurrAttacking == true)
                {
                    DoGettingHurtStuff();
                }

                ref1 = null;
            }
            else if (other.tag == "Player" || other.tag == "PlrAttackHitbox")
            {
                // then the plr is touching enemy
                PlrAttackSystem ref1 = other.gameObject.GetComponent<PlrAttackSystem>();
                if (ref1 == null) { print("error! can't find PlrAttackSystem"); }

                if (ref1.isThisCharCurrAttacking == true)
                {
                    DoGettingHurtStuff();
                }

                ref1 = null;
            }
            else
            {
                print("issue checking if thing touching is actually attacking!");
            }
        }
    }
}
