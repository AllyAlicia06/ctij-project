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
    
    [Header("Spawn Rates (seconds between spawns)")]
    [SerializeField] private float pregameInterval = 0.6f; // faster
    [SerializeField] private float playingInterval = 2.0f; // slower
    [SerializeField] private float otherStatesInterval = -1f; // < 0 = don't spawn

    private Coroutine spawnRoutine;
    private float currentInterval = -1f;

  /*  private void OnEnable()
    {
        if (gameManager == null)
            gameManager = GameManager.Instance;

        if (gameManager != null)
            gameManager.OnGameStateChanged += HandleGameStateChanged;

        if (gameManager != null)
            ApplyState(gameManager.currentGameState);
        
        if (gameManager == null)
            Debug.Log("No game manager found");
    }*/

    private void OnDisable()
    {
        if (gameManager != null)
            gameManager.OnGameStateChanged -= HandleGameStateChanged;

        StopSpawning();
    }

    private void HandleGameStateChanged(GameState state) => ApplyState(state);

    private void ApplyState(GameState state)
    {
        float desiredInterval = state switch
        {
            GameState.Pregame => pregameInterval,
            GameState.Playing => playingInterval,
            _ => otherStatesInterval
        };

        // If nothing changed, do nothing
        if (Mathf.Approximately(desiredInterval, currentInterval)) return;

        currentInterval = desiredInterval;

        if (currentInterval < 0f)
        {
            StopSpawning();
        }
        else
        {
            RestartSpawning();
        }
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
        if (spawnPoints == null || spawnPoints.Length == 0) return;
        if (micePrefabs == null || micePrefabs.Length == 0) return;

        Transform p = spawnPoints[Random.Range(0, spawnPoints.Length)];
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


