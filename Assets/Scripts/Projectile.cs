using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float speed;
    private float damage;
    private Rigidbody2D rb;

    [SerializeField] private float lifeTime = 10f; 
    [SerializeField] private bool pierce = false;
    [SerializeField] private bool dealDamageOnHit = true;
    [SerializeField] private bool destroyOnHit = true;
    
    private List<Dog> dogsHit = new List<Dog>();
    
    [SerializeField] private ElementType elementType =  ElementType.None;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
    }
    
    public void Init(float projectileSpeed, float projectileDamage, ElementType element)
    {
        speed = projectileSpeed;
        damage = projectileDamage;
        elementType = element;
        
        rb.linearVelocity = Vector2.right * speed;

        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Dog dog = other.GetComponent<Dog>() ?? other.GetComponentInParent<Dog>();
        if (dog == null) return;
        
        if (dogsHit.Contains(dog))
            return;

        dogsHit.Add(dog);
        
        if (dealDamageOnHit) dog.TakeDamage(damage,elementType);
        if (destroyOnHit && !pierce) Destroy(gameObject); 
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
