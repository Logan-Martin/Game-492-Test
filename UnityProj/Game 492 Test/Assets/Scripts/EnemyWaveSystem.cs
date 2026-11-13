using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveSystem : MonoBehaviour
{
    public enum EnemyWaveCompletionRequirementType // Not Yet Implemented
    {
        None,
        Timed,
        DefeatAllEnemies,
    }

    public enum EnemyWaveState
    {
        None,
        WaitingToStart,
        InProgress,
        Completed
    }

    // ---- //
    [SerializeField] public static GameObject spawnersFolderContainer;

    public static void SpawnEnemies_Helper(int numOfEnemiesToSpawnPerSpawner)
    {
        foreach (Transform child in spawnersFolderContainer.transform) // Get all spawners
        {
            GameObject childGameObj = child.gameObject;
            for (int i = 0; i < numOfEnemiesToSpawnPerSpawner; i++) // num of enemy to spawn at one spawner at once
            {
                EnemySpawnerScript enemySpawnerScript = childGameObj.GetComponent<EnemySpawnerScript>();
                if (enemySpawnerScript == null) { print("Error, EnemySpawnerScript is null!"); return; }
                
                enemySpawnerScript.SpawnEnemy();
            }
        }
    }

    public class EnemyWave
    {
        public EnemyWaveState currState = EnemyWaveState.None;
        public EnemyWaveCompletionRequirementType completionRequirementType = EnemyWaveCompletionRequirementType.None;
        public int enemiesToSpawnPerSpawner_atOnce = 1;

        // could do a bool for custom timer before end of wave
        public int condsNeeded = 1; // ! or can be set up by Create function based on RequirementType
        public int condsMet = 0;
        // ---- //

        public void CompleteWave()
        {
            currState = EnemyWaveState.Completed;
            print("Wave completed!");
        }

        public void IncrementCondition()
        {
            condsMet++;
            if (condsMet >= condsNeeded)
            {
                condsMet = condsNeeded;
                CompleteWave();
            }
        }

        public void StartWave()
        {
            currState = EnemyWaveState.InProgress;
            SpawnEnemies_Helper(enemiesToSpawnPerSpawner_atOnce);
        }
    }
    public class EnemyWave_Group
    {
        public List<EnemyWave> wavesList = new List<EnemyWave>();

        public void StartWaveSequence()
        {
            wavesList[0].StartWave();
        }
        
    }

    public EnemyWave_Group CreateEnemyWavesGroup_Quick(int numOfWaves, int enemiesToSpawnPerSpawner)
    {
        if (numOfWaves <= 0) { print("Error: numOfWaves must be >= 1"); return null; }

        EnemyWave_Group newWavesGroup = new EnemyWave_Group();
        for (int i = 0; i < numOfWaves; i++)
        {
            EnemyWave enemyWave = new EnemyWave();
            enemyWave.enemiesToSpawnPerSpawner_atOnce = enemiesToSpawnPerSpawner;
            enemyWave.currState = EnemyWaveState.WaitingToStart;
            //enemyWave.completionRequirementState = EnemyWaveCompletionRequirementState.Timed;

            newWavesGroup.wavesList.Add(enemyWave);
        }

        return newWavesGroup;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnersFolderContainer = GameObject.FindGameObjectWithTag("EnemySpawnersContainer");
        if (spawnersFolderContainer == null) { print("Error!: spawnersFolderContainer is null! "); return; }

        EnemyWave_Group enemyWave_Group = CreateEnemyWavesGroup_Quick(1, 2);
        enemyWave_Group.StartWaveSequence();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
