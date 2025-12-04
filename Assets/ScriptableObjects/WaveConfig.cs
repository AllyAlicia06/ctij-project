using UnityEngine;

[System.Serializable]
public class WaveEntry
{
    public DogData dogData;
    public GameObject dogPrefab;
    public int spawnCount;
    public float spawnInterval;
}

[CreateAssetMenu(fileName = "WaveConfig", menuName = "Waves/WaveConfig")]
public class WaveConfig : ScriptableObject
{
    public string waveName;
    public WaveEntry[] waveEntries;
}
