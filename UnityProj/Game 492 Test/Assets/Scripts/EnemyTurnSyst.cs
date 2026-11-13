using System.Collections.Generic;
using UnityEngine;

public class EnemyTurnSyst : MonoBehaviour
{
    private GameObject plrRef;

    [SerializeField] private float globalEnemyAttackAttemptCooldown = 2f;
    private float tSinceLastAttackAttempt = 0f;
    //private bool enemyAttackingInProgress = false;
    // -- Score Vars: -- //
    private float maxDist = 50f;
    private float maxTime = 20f;
    private float DistWeight = 0.7f;
    private float TimeWeight = 0.3f;
    // ----------------  //

    List<GameObject> enemyList = new List<GameObject>();
    //float cooldownBetweenChoosingNextEnemy = 1f;

    public void AddEnemyToTurnSystemList(GameObject enemy)
    {
        if (!enemyList.Contains(enemy)) {
            enemyList.Add(enemy);
        }
    }

    public void RemoveEnemyFromTurnSystem(GameObject enemy)
    {
        bool check = enemyList.Remove(enemy);
        if (check == false) {
            print("error! failed to remove enemy from turn list?!");
        }
    }

    private GameObject PickEnemyToDoTurn()
    {
        GameObject picked = null;
        float score = 0;
        // distance
        // attackedRecently
        // ?cooldown 

        foreach (var item in enemyList)
        {
            EnemyStateSystem enemyStateSyst_Ref = item.GetComponent<EnemyStateSystem>();
            EnemyStateSystem.EnemyState currState = enemyStateSyst_Ref.GetCurrEnemyState();
            if (currState == EnemyStateSystem.EnemyState.WaitingForTurn) // bc can be Approaching, WaitingForTurn, Attacking, Stunned
            {
                // Time Score Compute: //
                AttackSystem attackSystemRef = item.GetComponent<AttackSystem>();
                float tLastAttack = (Time.time - attackSystemRef.tSinceLastPerfAttack);
                float tSinceLastAttack_Normalized = (tLastAttack / maxTime);
                float tSinceLastAttack_Normalized_Clamped = Mathf.Clamp(tSinceLastAttack_Normalized, 0, 1);
                // ---- //


                // Dist. Score Compute: //
                float distSqr = Mathf.Sqrt(enemyStateSyst_Ref.GetCurrDist_SqrMag_ToPlr());
                float distSqr_Normalized = (1 - (distSqr / maxDist));
                float distSqr_Normalized_Clamped = Mathf.Clamp(distSqr_Normalized, 0, 1);
                // ---- //


                float localScore = ((tSinceLastAttack_Normalized_Clamped * TimeWeight) 
                                  + (distSqr_Normalized_Clamped * DistWeight));

                print("localScore: " + localScore);
                if (score != 0)
                {
                    if (localScore < score) // whoever closest 
                    {
                        picked = item;
                        score = localScore;
                    }
                }
                else // if 0, 1st case, so set init
                {
                    picked = item;
                    score = localScore;
                }
            }
            else
            {
                // edge case?
            }
        }
        return picked;
    }


    private void Start()
    {
        plrRef = GameObject.FindGameObjectWithTag("Player");
        if (plrRef == null)
        {
            print("Error in set up of refs!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyList.Count >= 1)
        {
            if (plrRef.GetComponent<HealthSystem>().GetHealth() == 0)
            {
                // player is dead, drop attacking
                return;
            }

            float temp = Time.time;
            if (((temp - tSinceLastAttackAttempt) >= globalEnemyAttackAttemptCooldown)) {
                //if (enemyAttackingInProgress == false)
                //{
                    //enemyAttackingInProgress = true;
                //}
                tSinceLastAttackAttempt = temp;

                GameObject picked = PickEnemyToDoTurn();
                if (picked != null)
                {
                    picked.GetComponent<EnemyStateSystem>().SetEnemyState(EnemyStateSystem.EnemyState.Attacking);
                }
            }
        }


        // try signal attack every X seconds // attackRate
            // want to see enemies going from attacking to waiting w/ score system 
            // Notes: see how enemies react to plr in bayonetta
                // attacks made for dodging, note that feedback to plr 
    }
}
