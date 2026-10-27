using UnityEngine;
using static Hitbox;

public class CombatCharacter : MonoBehaviour
{
    enum LifeState
    {
        None,
        Alive,
        Dying,
        Dead
    };

    public CombatTeam team;


    CombatCharacter targetEnemy;

    LifeState lifeState = LifeState.None;
    int currentHP;
    public int maxHP = 1000;

    float secondsDying;

    Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        SetLifeState(LifeState.Alive);
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

        if(lifeState == LifeState.Dying)
        {
            secondsDying += Time.deltaTime;
            if(secondsDying > 0.75f)
            {
                SetLifeState(LifeState.Dead);
            }
        }
    }

    void FindPreferredTarget()
    {
        targetEnemy = null;
        float closestDistance = float.MaxValue;

        CombatCharacter[] combatants = FindObjectsByType<CombatCharacter>(FindObjectsSortMode.None);
        foreach (CombatCharacter combatant in combatants)
        {
            if (combatant.team != team && !combatant.IsDead())
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

    public float GetPreferredTargetDistance()
    {
        if (targetEnemy != null)
        {
            Vector3 vectorToEnemy = targetEnemy.transform.position - transform.position;
            return vectorToEnemy.magnitude;
        }

        return 0.0f;
    }

    public void InflictDamage(int damageAmount)
    {
        currentHP -= damageAmount;
        if(currentHP <= 0)
        {
            SetLifeState(LifeState.Dying);
        }

        if (animator != null)
        {
            animator.SetTrigger(
                IsDying() ?
                "dead" :
                "takeHit"
                );
        }
    }

    public bool IsDying()
    {
        return lifeState == LifeState.Dying;
    }

    public bool IsDead()
    {
        return lifeState == LifeState.Dead;
    }

    public bool CanBeHit()
    {
        EnemyStateController enemyStateController = GetComponent<EnemyStateController>();
        if (enemyStateController != null)
        {
            return enemyStateController.CanBeHit() && !IsDead();
        }
        return !IsDead();
    }

    void SetLifeState(LifeState inState)
    {
        if(lifeState != inState)
        {
            lifeState = inState;

            switch(lifeState)
            {
                case LifeState.Alive:
                    currentHP = maxHP;
                    break;
                case LifeState.Dying:
                    currentHP = 0;
                    secondsDying = 0.0f;
                    break;
            }
        }
    }
}
