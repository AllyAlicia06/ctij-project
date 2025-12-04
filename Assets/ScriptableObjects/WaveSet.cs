using UnityEngine;

[CreateAssetMenu(fileName = "WaveSet", menuName = "Waves/WaveSet")]
public class WaveSet : ScriptableObject
{
    public WaveConfig[] waves;
}
