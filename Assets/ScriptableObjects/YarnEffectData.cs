using UnityEngine;

[CreateAssetMenu(fileName = "YarnEffectData", menuName = "Yarn/YarnEffectData")]
public class YarnEffectData : ScriptableObject
{
    public string effectName;
    public ElementType elementType;

    public float damage;
    public float aoeRadius;
    public float slowAmount;
    public float duration;

    public bool stuns;
    public bool isBuff;
    public GameObject effectPrefab;
}
