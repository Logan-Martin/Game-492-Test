using System.Collections.Generic;
using UnityEngine;

public class EnemyTurnSyst : MonoBehaviour
{
    [SerializeField] private float globalEnemyAttackAttemptCooldown = 2f;
    private float tSinceLastAttackAttempt = 0f;
    private bool enemyAttackingInProgress = false;

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
                // if locked on, have priority???
                        // note Plr has AttackTargetSystem.cs but that's different than lockedOn
                //float localScore = enemyList.IndexOf(item);


                float localScore = enemyStateSyst_Ref.GetCurrDist_SqrMag_ToPlr();
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


    // Update is called once per frame
    void Update()
    {
        if (enemyList.Count >= 1)
        {
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
