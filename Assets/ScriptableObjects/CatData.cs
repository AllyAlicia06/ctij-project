using UnityEngine;

[CreateAssetMenu(fileName = "CatData", menuName = "DogsCats/CatData")]
public class CatData : ScriptableObject
{
    public Sprite sprite;
    
    [Header("Cat Placement")]
    public GameObject catPrefab;
    public int cost = 0;
    
    [Header("Combat")]
    public ElementType elementType;
    public float damage = 20f;
    public float cooldown = 1f;
    
    [Header("Yarn Projectile")]
    public GameObject projectilePrefab;
    public float projectileSpeed = 5f;
    
    [Header("Special Shot")]
    public int specialShotCount = 5;
    public GameObject specialShotPrefab;
    public float specialShotSpeed = 5f;
    public float projectileDamage = 100f;
}
