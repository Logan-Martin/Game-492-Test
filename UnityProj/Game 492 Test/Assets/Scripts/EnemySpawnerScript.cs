using UnityEngine;

public class EnemySpawnerScript : MonoBehaviour
{
    [SerializeField] private GameObject prefabObj;
    [SerializeField] private GameObject enemyFolder;

    public void SpawnEnemy()
    {
        GameObject clonedObj = Instantiate(prefabObj, enemyFolder.transform);
        EnemyStateSystem stateSystem = clonedObj.gameObject.GetComponent<EnemyStateSystem>();
        stateSystem.SetEnemyState(EnemyStateSystem.EnemyState.Spawning);
        
        // !!gotta fix all the refs like Plr or things prefab doesn't have by default
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //SpawnEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
