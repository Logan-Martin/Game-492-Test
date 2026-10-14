using UnityEngine;
using static Hitbox;

public class CombatCharacter : MonoBehaviour
{
    public CombatTeam team;


    //TEMP: Public so we can see it in inspector
    public CombatCharacter targetEnemy;


    //TODO: HP Stuff goes here

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FindPreferredTarget();

        if(team == CombatTeam.Player)
        {
            if (targetEnemy != null)
            {
                Vector3 vectorToEnemy = targetEnemy.transform.position - transform.position;
                Debug.DrawLine(transform.position, transform.position + vectorToEnemy, Color.red);
            }
        }
    }

    void FindPreferredTarget()
    {
        float closestDistance = float.MaxValue;
        CombatCharacter[] combatants = FindObjectsByType<CombatCharacter>(FindObjectsSortMode.None);
        foreach (CombatCharacter combatant in combatants)
        {
            if (combatant.team != team)
            {
                Vector3 vectorToEnemy = combatant.transform.position - transform.position;
                float currentDistance = vectorToEnemy.magnitude;

                if (currentDistance < closestDistance)
                {
                    targetEnemy = combatant;
                    closestDistance = currentDistance;
                }
            }
        }
    }

    public Vector3 GetPreferredTargetDirection()
    {
        if (targetEnemy != null)
        {
            Vector3 vectorToEnemy = targetEnemy.transform.position - transform.position;
            vectorToEnemy.Normalize();
            return vectorToEnemy;
        }
        return Vector3.zero;
    }
}
