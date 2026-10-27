using System.Collections.Generic;
using UnityEngine;

public class EnemyTurnSyst : MonoBehaviour
{
    List<GameObject> enemyList = new List<GameObject>();
    //float cooldownBetweenChoosingNextEnemy = 1f;

    public void AddEnemyToTurnSystemList(GameObject enemy)
    {
        enemyList.Add(enemy);
    }

    public void RemoveEnemyFromTurnSystem(GameObject enemy)
    {
        enemyList.Remove(enemy);
    }

    private GameObject PickEnemyToDoTurn()
    {
        GameObject picked = null;
        float score = 0;
        foreach (var item in enemyList)
        {
            float localScore = enemyList.IndexOf(item);
            if (localScore < score) // doing get first in for now
            {
                picked = item;
                score = localScore;
            }
        }
        return picked;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
