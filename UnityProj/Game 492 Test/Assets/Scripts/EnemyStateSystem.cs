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
    // Enemy Variables:
    [SerializeField] EnemyTurnSyst enemyTurnSyst_Ref;
    [SerializeField] AttackSystem attackSystem_Ref;

    // ---------- //


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void SetEnemyState(EnemyState newState)
    {
        if (newState != currEnemyState)
        {
            currEnemyState = newState;

            switch (currEnemyState)
            {
                case EnemyState.Spawning:
                    break;
                case EnemyState.ApproachingTarg:
                    // add to potential targ list
                    // dist check then if close enough? // enemyTurnSyst_Ref.AddEnemyToTurnSystemList(this.gameObject);
                    break;
                case EnemyState.WaitingForTurn:
                    // coming from approaching, stop moving?
                    // 
                    break;
                case EnemyState.Attacking:
                    //attackSystem_Ref.DoNormalAttack(); //?
                    break;
                case EnemyState.Stunned:
                    // play stunAnim
                    break;
                case EnemyState.Dying:
                    // play dying anim
                    break;
                case EnemyState.Dead:
                    //Destroy(this.gameObject);
                    break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (currEnemyState)
        {
            case EnemyState.None:
                // go to spawning after
                break;
            case EnemyState.Spawning:
                // go to approaching target logic
                // after Spawn Anim done playing
                break;
            case EnemyState.ApproachingTarg:
                // go till close enough to targ
                // to then go to Waiting for turn
                // make logic in a seperate script

                // OR if took a hit, go to Hitstun State
                break;
            case EnemyState.WaitingForTurn:
                // turn system needed to choose
                // this called coming from seperate script ?

                // OR if took a hit, go to Hitstun
                break;
            case EnemyState.Attacking:
                // curr in attacking, then need to get out
                // when attackAnim finished

                // OR if took a hit while attacking, go to hitstun
                break;
            case EnemyState.Stunned:                
                // if hitStunAnim finished && health > 0, go back to WaitingForTurn

                // OR if hitStunAnim finished && health <= 0, go to Dying state
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
