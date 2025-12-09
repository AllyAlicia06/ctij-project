using UnityEditor.Rendering;
using UnityEngine;

[CreateAssetMenu(fileName = "DogData", menuName = "Dogs&Cats/DogData")]
public class DogData : ScriptableObject
{
    public Sprite sprite;
    
    [Header("Gameplay")]
    public ElementType elementType;
    public float maxHealth = 10f;
    public float moveSpeed = 5f;

    public int baseDamage = 1;
}
