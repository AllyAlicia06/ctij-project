using UnityEngine;

[CreateAssetMenu(fileName = "WaveConfig", menuName = "Waves/WaveConfig")]
public class WaveConfig : ScriptableObject
{
    //public string waveName;
    public WaveEntry[] waveEntries;
}
