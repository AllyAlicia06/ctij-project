using UnityEngine;

public class SpawnPoisonTrail : MonoBehaviour
{
    [SerializeField] private GameObject poisonTrailPrefab;
    [SerializeField] private float spawnEvery = 0.25f;
    
    private float nextSpawnTime;

    private void OnEnable()
    {
        nextSpawnTime = Time.time;
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (poisonTrailPrefab == null) return;

        if (Time.time >= nextSpawnTime)
        {
            nextSpawnTime = Time.time + spawnEvery;
            Instantiate(poisonTrailPrefab, transform.position, Quaternion.identity);
        }
    }
}
