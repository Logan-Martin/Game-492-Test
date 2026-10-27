using UnityEngine;

public class EnemyStateController : MonoBehaviour
{
    enum EnemyControlState
    {
        None,
        Spawning,
        Approaching,
        WaitingForTurn,
        Attacking,
        HitStun,
        Dying,
        Dead
    }

    EnemyControlState enemyControlState = EnemyControlState.None;
    float secondsInCurrentState;

    Animator animator;
    CombatCharacter combatCharacter;

    public float spawnSeconds = 2.0f;
    public ParticleSystem spawnEffect;
    public float desiredApproachDistance = 5.0f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        combatCharacter = GetComponent<CombatCharacter>();
        SetEnemyControlState(EnemyControlState.Spawning);
    }

    // Update is called once per frame
    void Update()
    {
        switch (enemyControlState)
        {
            case EnemyControlState.Spawning:
                if(secondsInCurrentState > spawnSeconds)
                {
                    SetEnemyControlState(EnemyControlState.Approaching);
                }
                break;

            case EnemyControlState.Approaching:
                float distanceToTarget = combatCharacter.GetPreferredTargetDistance();
                if (distanceToTarget > desiredApproachDistance)
                {
                    //TODO: Figure out how to move character algorithmically
                }
                else
                {
                    //TODO: Go to Waiting For Turn State
                }
                break;
        }

        secondsInCurrentState += Time.deltaTime;
    }

    void SetEnemyControlState(EnemyControlState newState)
    {
        if(newState != enemyControlState)
        {
            switch (enemyControlState)
            {
                case EnemyControlState.Spawning:
                    animator.SetBool("spawning", false);
                    spawnEffect.Stop();
                    break;
            }

            enemyControlState = newState;
            secondsInCurrentState = 0.0f;

            switch(enemyControlState)
            {
                case EnemyControlState.Spawning:
                    animator.SetBool("spawning", true);
                    spawnEffect.Play();
                    break;
            }
        }
    }

    public bool CanBeHit()
    {
        return enemyControlState == EnemyControlState.Approaching ||
            enemyControlState == EnemyControlState.WaitingForTurn ||
            enemyControlState == EnemyControlState.Attacking ||
            enemyControlState == EnemyControlState.HitStun;
    }
}
