using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class EnemyWaveSystem : MonoBehaviour
{
    public enum EnemyWaveCompletionRequirementType // Not Yet Implemented
    {
        None,
        Timed, // lends itself to endless mode of totalToSpawn until timer runs out
        DefeatAllEnemies, // can have timer but transitioning to next wave based on defeating enemies spawned
    }

    public enum EnemyWave_KeepPopulationSpawningMethod
    {
        None, // default for base case of spawning X enemies from X spawners
        EnemyCountToKill, // for wave 3 ex. use case
        TimeBased, // for wave wave 4 ex. use case
    }

    public enum EnemyWaveState
    {
        None,
        WaitingToStart,
        InProgress,
        Completed
    }

    [System.Serializable]
    public class EnemyWave
    {
        public Action OnWaveCompleted; // Note: EnemyWaveGroup listens

        public GameObject[] waveSpawners_ForWave = new GameObject[0]; // can be NULL / empty! refer to EnemyWave_Group's waveSpawners_Main list
        private List<GameObject> enemiesSpawnedByThisWave = new List<GameObject>();
        //private List<>

        [NonSerialized] public EnemyWaveState currState = EnemyWaveState.None;
        public EnemyWaveCompletionRequirementType completionRequirementType = EnemyWaveCompletionRequirementType.None;
        public int enemiesToSpawnPerSpawner_atOnce = 1;

        // could do a bool for custom timer before end of wave
        [NonSerialized] public int condsNeeded = 1; // ! or can be set up by Create function based on RequirementType
        [NonSerialized] public int condsMet = 0;
        // ---- //

        public EnemyWave_KeepPopulationSpawningMethod enemyWave_KeepPopulationSpawningMethod = EnemyWave_KeepPopulationSpawningMethod.None;

        [Space]
        public int totalPopulationAllowedAtOnce = 0; // default to none for now // could be amount from spawners?
        private int currPopulation = 0;

        public int totalEnemiesToSpawnThisWave = 0; // default to none for now // could be enemyCount based on Spawners
        private int numOfEnemiesSpawnedThisWave = 0;

        public float timeInSecondsToKeepSpawningEnemies = 0f; // for wave 4 use case
        private float timeLeftToKeepSpawningEnemies = 0f;
        // -------------- //
        public void SetEnemyWaveState(EnemyWaveState newState)
        {
            switch(newState)
            {
                case EnemyWaveState.None:
                    break;
                case EnemyWaveState.WaitingToStart:
                    SetUpConditionsForWave();
                    break;
                case EnemyWaveState.InProgress:
                    SetUpConditionsForWave(); // for init starting one?
                    StartWave();
                    break;
                case EnemyWaveState.Completed:
                    CompleteWave();
                    break;
            }
            currState = newState;
        }

        // -------------- //


        public void Handle_EnemyForWaveKilled_Event(GameObject enemyKilled_Obj)
        { // count do the check on the correct wave and stuff here?
            if (this.completionRequirementType != EnemyWaveCompletionRequirementType.DefeatAllEnemies)
            {
                print("Wrong Completion Req. ?"); return;
            }

            bool check = enemiesSpawnedByThisWave.Remove(enemyKilled_Obj);
            if (check == false) { print("Failed to remove killed enemy from GameObj list for Wave!"); return; }

            this.currPopulation = enemiesSpawnedByThisWave.Count;
            enemyKilled_Obj.GetComponent<EnemyStateSystem>().OnEnemyDeath -= Handle_EnemyForWaveKilled_Event;
            IncrementCondition();

            if (this.enemyWave_KeepPopulationSpawningMethod == EnemyWave_KeepPopulationSpawningMethod.None)
            {
                //print("Dont need to track population");
                return;
            }
        }

        public void CompleteWave()
        {
            print("Wave completed!");

            OnWaveCompleted?.Invoke();
        }

        public void IncrementCondition()
        {
            condsMet++;
            print("Incrementing Condition for Wave! | " + condsMet + " / " + condsNeeded);

            if (condsMet >= condsNeeded)
            {
                condsMet = condsNeeded;
                SetEnemyWaveState(EnemyWaveState.Completed);
            }
        }

        public List<GameObject> SpawnEnemies_Helper(int numOfEnemiesToSpawnPerSpawner)
        {
            List<GameObject> spawnedEnemyList = new List<GameObject>();

            GameObject[] local_spawnersList = new GameObject[0];
            local_spawnersList = waveSpawners_ForWave;
            if (local_spawnersList.Length == 0)
            {
                print("Error: not using local_spawnersList bc it's empty. Haven't implemented using Group list yet.");
                return spawnedEnemyList;
            }


            foreach (GameObject child in local_spawnersList) // Get all spawners for this wave
            {
                for (int i = 0; i < numOfEnemiesToSpawnPerSpawner; i++) // num of enemy to spawn at one spawner at once
                {
                    EnemySpawnerScript enemySpawnerScript = child.GetComponent<EnemySpawnerScript>();
                    if (enemySpawnerScript == null) { print("Error, EnemySpawnerScript is null! Trying to cont."); continue; }


                    GameObject enemy = enemySpawnerScript.SpawnEnemy();
                    if (enemy == null) { print("Error getting Enemy GameObj from SpawnEnemy() call. Trying to cont."); continue; }
                    spawnedEnemyList.Add(enemy);
                }
            }

            return spawnedEnemyList;
        }

        public void SetUpConditionsForWave()
        {
            if (completionRequirementType == EnemyWaveCompletionRequirementType.DefeatAllEnemies)
            {
                int check = waveSpawners_ForWave.Length * enemiesToSpawnPerSpawner_atOnce;
                if (check == 0)
                {
                    Debug.Log("ERROR?!");
                }
                Debug.Log(check);
                totalEnemiesToSpawnThisWave = check;
                condsNeeded = totalEnemiesToSpawnThisWave;

                Debug.Log("Set up Wave Conditions!");
                //if (enemyWave_KeepPopulationSpawningMethod != EnemyWave_KeepPopulationSpawningMethod.None)
                //{
                //    condsNeeded++; 
                //}
            }

        }

        public void StartWave()
        {
            List<GameObject> enemiesSpawned = new List<GameObject>();
            enemiesSpawned = SpawnEnemies_Helper(enemiesToSpawnPerSpawner_atOnce);
            if (enemiesSpawned.Count == 0) { print("Error: enemiesSpawned is 0?"); return; }

            enemiesSpawnedByThisWave.AddRange(enemiesSpawned);
            this.currPopulation = enemiesSpawnedByThisWave.Count;

            foreach (GameObject enemy in enemiesSpawned)
            {
                enemy.GetComponent<EnemyStateSystem>().OnEnemyDeath += Handle_EnemyForWaveKilled_Event;
            }
        }
    }

    [System.Serializable]
    public class EnemyWave_Group
    {
        public string name = "";
        public GameObject[] waveSpawners_Main;
        public List<EnemyWave> wavesList = new List<EnemyWave>();
        public bool startWaveSequence_OnGameStart = false;
        public float cooldownBetweenWaves = 2f; // in seconds
        private int currWaveIndex = 0;

        // ==== From Google AI Search: ====== //
        // -- Prompts: --
        // 1st: 'Unity invoke thing waitForSeconds to wait' + Code
        
        // 2nd: 'can i just do the IEnumerator StartCurrentWave function in the EnemyWaveSystem Script
              // and pass the current wave?'
        
        // 3rd: i think i like including the MonoBehavior better.
             // what are my options there and what is NEEDED for the EnemyWave_Group class
        // ============== //
        [NonSerialized] public MonoBehaviour runner; // NEEDED: Reference to run coroutines

        // NEEDED: Pass 'this' from EnemyWaveSystem when starting
        public void StartWaveSequence(MonoBehaviour monoRunner)
        {
            runner = monoRunner;
            runner.StartCoroutine(StartCurrentWave(cooldownBetweenWaves));
        }
        public IEnumerator StartCurrentWave(float delay)
        {
            EnemyWave currWave = wavesList[currWaveIndex];
            currWave.OnWaveCompleted += TryToGoToNextWaveInList;
            Debug.Log("Got new Wave & connected event. Waiting cooldown.");

            yield return new WaitForSeconds(delay);

            Debug.Log("Starting wave #" + (currWaveIndex + 1));
            currWave.SetEnemyWaveState(EnemyWaveState.InProgress);
        }
        // ================================== //

        private void TryToGoToNextWaveInList()
        {
            wavesList[currWaveIndex].OnWaveCompleted -= TryToGoToNextWaveInList; // unsub after call
            
            currWaveIndex++;
            if (currWaveIndex < wavesList.Count) // if within wavesList count
            {
                runner.StartCoroutine(StartCurrentWave(cooldownBetweenWaves)); // continue // From AI
            }
            else
            {
                print("All waves completed!");
            }
        }

    }

    // ---- //
    [SerializeField] public static GameObject spawnersFolderContainer;
    [SerializeField] public List<EnemyWave_Group> EnemyWaveGroups_SetInEditor;

    public EnemyWave_Group CreateEnemyWavesGroup_Quick(string name, int numOfWaves, int enemiesToSpawnPerSpawner)
    {
        if (numOfWaves <= 0) { print("Error: numOfWaves must be >= 1"); return null; }

        EnemyWave_Group newWavesGroup = new EnemyWave_Group();
        for (int i = 0; i < numOfWaves; i++)
        {
            EnemyWave enemyWave = new EnemyWave();
            enemyWave.enemiesToSpawnPerSpawner_atOnce = enemiesToSpawnPerSpawner;
            enemyWave.SetEnemyWaveState(EnemyWaveState.WaitingToStart);
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

        //EnemyWave_Group enemyWave_Group = CreateEnemyWavesGroup_Quick("WaveGroup1", 1, 2);
        //enemyWave_Group.StartWaveSequence();

        if (EnemyWaveGroups_SetInEditor.Count == 0) { print("No Wave Groups set in Editor!"); return; }
        foreach (EnemyWave_Group waveGroup in EnemyWaveGroups_SetInEditor)
        {
            if (waveGroup.startWaveSequence_OnGameStart == false) { continue; }
            waveGroup.StartWaveSequence(this);
        }
    }
}
