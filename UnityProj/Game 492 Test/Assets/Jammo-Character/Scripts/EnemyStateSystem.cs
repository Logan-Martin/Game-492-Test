using UnityEngine;

public class EnemyStateSystem : MonoBehaviour
{
    enum EnemyState
    {
        None,
        Spawning,
        ApproachingTarg,
        WaitingForTurn,
        Attacking,
        Stunned,
        Dying,
        Dead,
    }

    EnemyState currEnemyState = EnemyState.None ;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void SetEnemyState(EnemyState newState)
    {
        if (newState != currEnemyState)
        {
            currEnemyState = newState;
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (currEnemyState)
        {
            case EnemyState.None:
                // go to spawning
                break;
            case EnemyState.Spawning:
                // go to approaching target logic
                break;
            case EnemyState.ApproachingTarg:
                // go till close enough to targ
                // to then go to Waiting for turn
                break;
            case EnemyState.WaitingForTurn:
                // turn system needed to choose
                break;
            case EnemyState.Attacking:
                // curr in attacking, then need to get out
                break;
            case EnemyState.Stunned:
                // timer or smth on stun to get out of it
                break;
            case EnemyState.Dying:
                // then transitions to dead after anims & vfx
                break;
            case EnemyState.Dead:
                // destroy?
                break;
        }
    }
}
