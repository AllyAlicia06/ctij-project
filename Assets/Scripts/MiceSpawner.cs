using System.Collections;
using UnityEngine;
//used for the mice to spawn -> in scene

public class MiceSpawner : MonoBehaviour
{
    private GameManager gameManager;
    
    [SerializeField] 
    private GameObject[] micePrefabs;
    [SerializeField] 
    private Transform[] spawnPoints;

    [SerializeField] private Transform[] stormSpawnPoints;
    
    [Header("Spawn Rates (seconds between spawns)")]
    [SerializeField] private float pregameInterval = 0.6f; // faster
    [SerializeField] private float playingInterval = 2.0f; // slower
    [SerializeField] private float otherStatesInterval = -1f; // < 0 = don't spawn
    [SerializeField] private float stormInterval = 0.2f;

    private Coroutine spawnRoutine;
    private float currentInterval = -1f;
    private Transform[] activeSpawnPoints;

    private void OnDisable()
    {
        if (gameManager != null)
            gameManager.OnGameStateChanged -= HandleGameStateChanged;

        StopSpawning();
    }

    private void HandleGameStateChanged(GameState state) => ApplyState(state);

    private void ApplyState(GameState state)
    {
        switch (state)
        {
            case GameState.Pregame:
                currentInterval = pregameInterval;
                activeSpawnPoints = spawnPoints;
                break;

            case GameState.Playing:
                currentInterval = playingInterval;
                activeSpawnPoints = spawnPoints;
                break;

            case GameState.Storm:
                currentInterval = stormInterval;
                activeSpawnPoints = (stormSpawnPoints != null && stormSpawnPoints.Length > 0)
                    ? stormSpawnPoints
                    : spawnPoints; // fallback
                break;

            default:
                currentInterval = otherStatesInterval;
                activeSpawnPoints = null;
                break;
        }
        
        if (currentInterval < 0f || activeSpawnPoints == null || activeSpawnPoints.Length == 0)
            StopSpawning();
        else
            RestartSpawning();
    }

    private void RestartSpawning()
    {
        StopSpawning();
        spawnRoutine = StartCoroutine(SpawnLoop());
    }

    private void StopSpawning()
    {
        if (spawnRoutine == null) return;
        StopCoroutine(spawnRoutine);
        spawnRoutine = null;
    }

    private IEnumerator SpawnLoop()
    {
        while (true)
        {
            // interval might change on state switch; loop uses latest value each time
            yield return new WaitForSeconds(currentInterval);
            SpawnOne();
        }
    }

    private void SpawnOne()
    {
        if (activeSpawnPoints == null || activeSpawnPoints.Length == 0) return;
        if (micePrefabs == null || micePrefabs.Length == 0) return;

        Transform p = activeSpawnPoints[Random.Range(0, activeSpawnPoints.Length)];
        GameObject prefab = micePrefabs[Random.Range(0, micePrefabs.Length)];
        Instantiate(prefab, p.position, p.rotation);
    }
        
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameManager.Instance;
        if (gameManager == null)
            gameManager = FindFirstObjectByType<GameManager>();
        
        if (gameManager == null)
        {
            Debug.Log("No game manager found 2");
            
            enabled = false;
            return;
        }
        
        gameManager.OnGameStateChanged += HandleGameStateChanged;
        ApplyState(gameManager.currentGameState);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}


