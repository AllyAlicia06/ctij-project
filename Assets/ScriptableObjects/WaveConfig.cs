using UnityEngine;

[CreateAssetMenu(fileName = "WaveConfig", menuName = "Waves/WaveConfig")]
public class WaveConfig : ScriptableObject
{
    public WaveEntry[] waveEntries;
}
