using System.Collections;
using UnityEngine;

public class DogSpawner : MonoBehaviour
{
    public Transform[] laneSpawnPoints;
    
    //
    public float moveDirection = -1f;
    //
    
    private bool isSpawning = false;
    
    private WaveConfig currentWave;
    private int totalToSpawn;
    private int spawned;
    private int died;

    public void StartWave(WaveConfig wave)
    {
        //un fail-safe in cazul in care sunt wave-uri anterioare inca in desfasurare
        if (isSpawning)
        {
            StopAllCoroutines();
            isSpawning = false;
        }
        
        currentWave = wave;
        totalToSpawn = 0;
        spawned = 0;
        died = 0;

        foreach (var entry in wave.waveEntries)
            totalToSpawn += entry.spawnCount;

        WaveProgressUI.Instance?.Setup(wave);
        StartCoroutine(SpawnRoutine());
        
        Debug.Log($"DogSpawner: starting wave {wave.waveName}, totalToSpawn = {totalToSpawn}");
    }

    private IEnumerator SpawnRoutine()
    {
        isSpawning = true;

        foreach (var entry in currentWave.waveEntries)
        {
            for (int i = 0; i < entry.spawnCount; i++)
            {
                SpawnDog(entry);
                spawned++;

                WaveProgressUI.Instance?.OnDogSpawned(spawned, totalToSpawn);
                yield return new WaitForSeconds(entry.spawnInterval);
            }
        }
        
        isSpawning = false;
    }

    private void SpawnDog(WaveEntry entry)
    {
        if (laneSpawnPoints == null || laneSpawnPoints.Length == 0)
        {
            Debug.Log("laneSpawnPoints not assigned");
            return;
        }
        
        //asta alege un lane random din cele pe care le avem
        var lane = laneSpawnPoints[Random.Range(0, laneSpawnPoints.Length)];
        
        //asta legit instantiaza clonele de caini
        var dogObj = Instantiate(entry.dogPrefab, lane.position, Quaternion.identity);
        
        var dog = dogObj.GetComponent<Dog>();
        if (dog != null)
        {
            if (entry.dogData != null)
            {
                dog.Initialize(entry.dogData, moveDirection);
            }
            
            dog.SetSpawner(this);
        }
        
        Debug.Log($"DogSpawner: spawned dog at {lane.name}");
    }

    public void OnDogDied()
    {
        died++;
        WaveProgressUI.Instance?.OnDogDied(died, totalToSpawn);

        if (died >= totalToSpawn)
        {
            GameManager.Instance.OnWaveCompleted();
        }
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
