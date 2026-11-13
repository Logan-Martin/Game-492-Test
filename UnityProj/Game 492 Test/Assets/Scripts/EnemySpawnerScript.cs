using System;
using UnityEngine;

public class EnemySpawnerScript : MonoBehaviour
{
    [SerializeField] private GameObject prefabObj;
    [SerializeField] private GameObject enemyFolder;
    [SerializeField] private bool spawnOnStart = false;

    public void SpawnEnemy()
    {
        GameObject clonedObj = Instantiate(prefabObj, enemyFolder.transform);
        clonedObj.transform.position = this.gameObject.transform.position;

        EnemyStateSystem stateSystem = clonedObj.gameObject.GetComponent<EnemyStateSystem>();
        stateSystem.SetEnemyState(EnemyStateSystem.EnemyState.Spawning);        
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyFolder = GameObject.FindGameObjectWithTag("MainEnemyFolderContainer");
        if (enemyFolder == null)
        {
            print("Error w/ initialization of refrences!");
            return;
        }

        if (spawnOnStart)
        {
            SpawnEnemy();
        }
    }
}
