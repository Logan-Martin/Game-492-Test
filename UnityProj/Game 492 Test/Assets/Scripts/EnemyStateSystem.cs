using UnityEngine;
using UnityEngine.InputSystem.XR;

public class EnemyStateSystem : MonoBehaviour
{
    public enum EnemyState
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

    private EnemyState currEnemyState = EnemyState.None ;

    [SerializeField] private GameObject plrRef;

    // Enemy Variables:
    [SerializeField] private HealthSystem healthSyst_Ref;
    [SerializeField] private EnemyTurnSyst enemyTurnSyst_Ref;
    [SerializeField] private AttackSystem attackSystem_Ref;
    [SerializeField] private Animator animator;
    [SerializeField] private MovementInput movementInputScript;


    [SerializeField] public CharacterController controller;

    private float distToPlr = 0;
    private float distToChangeToWaitingForTurnState = 6f;

    Vector3 vectDirToTarg_Normalized = Vector3.zero;
    //
    // ---------- //

    public void SetEnemyState(EnemyState newState)
    {
        if (newState != currEnemyState)
        {
            currEnemyState = newState;

            switch (currEnemyState)
            {
                case EnemyState.Spawning:
                    // play spawn anim
                    //animator.SetTrigger("PlaySpawnAnim");
                    break;
                case EnemyState.ApproachingTarg:
                    // add to potential targ list
                    enemyTurnSyst_Ref.AddEnemyToTurnSystemList(this.gameObject);
                    break;
                case EnemyState.WaitingForTurn:
                    // coming from approaching, stop moving?
                    
                    break;
                case EnemyState.Attacking:
                    movementInputScript.AttackFunc(); // more powerful to choose which attack from combo
                    //attackSystem_Ref.DoNormalAttack(); //?
                    break;
                case EnemyState.Stunned:
                    // RegisterHitsTaken -> HealthSystem -> Play Anim w/ Trigger -> Set StunnedState
                    break;
                case EnemyState.Dying:
                    // RegisterHitsTaken -> HealthSystem -> Play Anim w/ Trigger -> Set Dying
                    break;
                case EnemyState.Dead:
                    //Destroy(this.gameObject);
                    break;
            }
        }
    }

    // Animation Events:
    public void PlayCharDeathAnim()
    {
        SetEnemyState(EnemyState.Dying);
    }
    public void PlayCharTakingPunchAnim()
    {
        SetEnemyState(EnemyState.Stunned);
    }
    public void AttackAnim_Leaving()
    {
        SetEnemyState(EnemyState.WaitingForTurn);
    }
    public void SpawnAnimDonePlaying_Trigger()
    {
        SetEnemyState(EnemyState.ApproachingTarg);
    }
    // --------------- //
    private void Start()
    {
        SetEnemyState(EnemyState.Spawning);
    }

    // Update is called once per frame
    void Update()
    {
        if (currEnemyState != EnemyState.Dying && currEnemyState != EnemyState.Dead && currEnemyState != EnemyState.None)
        {
            vectDirToTarg_Normalized = (plrRef.transform.position - this.transform.position).normalized;
            distToPlr = (plrRef.transform.position - this.transform.position).sqrMagnitude;
        }


        switch (currEnemyState)
        {
            case EnemyState.None:
                break;
            case EnemyState.Spawning:
                break;
            case EnemyState.ApproachingTarg:
                // go till close enough to targ
                // to then go to Waiting for turn
                if (distToPlr <= distToChangeToWaitingForTurnState)
                {
                    SetEnemyState(EnemyState.WaitingForTurn);
                }
                else
                {
                    movementInputScript.InputX = vectDirToTarg_Normalized.x;
                    movementInputScript.InputZ = vectDirToTarg_Normalized.z;
                }
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
