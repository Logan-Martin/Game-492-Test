using UnityEngine;
using static Hitbox;

public class Hurtbox : MonoBehaviour
{
    public CombatTeam team;

    private void OnTriggerEnter(Collider other)
    {
        Hitbox incomingHitbox = other.GetComponent<Hitbox>();
        if (incomingHitbox != null)
        {
            if (incomingHitbox.team != team)
            {
                Debug.Log("I (" + gameObject.name + ") was hit by " + other.gameObject.name);
            }
        }
    }
}
