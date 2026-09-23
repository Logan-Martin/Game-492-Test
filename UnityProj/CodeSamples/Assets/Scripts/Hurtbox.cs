using UnityEngine;
using static Hitbox;

public class Hurtbox : MonoBehaviour
{
    public CombatTeam team;
    public Animator animator;


    private void OnTriggerEnter(Collider other)
    {
        Hitbox incomingHitbox = other.GetComponent<Hitbox>();
        if (incomingHitbox != null)
        {
            if (incomingHitbox.team != team)
            {
                if(incomingHitbox.hitsRemaining > 0)
                {
                    --incomingHitbox.hitsRemaining;
                    Debug.Log("I (" + gameObject.name + ") was hit by " + other.gameObject.name);
                    if (animator != null)
                    {
                        animator.SetTrigger("takeHit");
                    }
                }
            }
        }
    }
}
