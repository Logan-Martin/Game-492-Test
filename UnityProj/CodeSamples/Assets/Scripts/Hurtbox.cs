using UnityEngine;
using static Hitbox;

public class Hurtbox : MonoBehaviour
{
    public CombatTeam team;         //TODO: This replicates data in CombatCharacter. Refactor.
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

                    if (incomingHitbox.hitEffectPrefab != null)
                    {
                        GameObject hitEffect = GameObject.Instantiate(incomingHitbox.hitEffectPrefab, other.gameObject.transform.position, Quaternion.identity);
                        Destroy(hitEffect, 1.0f);
                    }

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
