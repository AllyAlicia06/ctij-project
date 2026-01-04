using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float speed;
    private float damage;
    private Rigidbody2D rb;

    [SerializeField] private float lifeTime = 10f; 
    [SerializeField] private bool pierce = false;
    
    private List<Dog> dogsHit = new List<Dog>();

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
    }
    
    public void Init(float projectileSpeed, float projectileDamage)
    {
        speed = projectileSpeed;
        damage = projectileDamage;
        
        rb.linearVelocity = Vector2.right * speed;

        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        /*Dog dog = other.GetComponent<Dog>();
        if (dog == null) dog = other.GetComponentInParent<Dog>();
        if (dog == null) return;

        dog.TakeDamage(damage);
        Destroy(gameObject);*/
        Dog dog = other.GetComponent<Dog>() ?? other.GetComponentInParent<Dog>();
        if (dog == null) return;
        
        if (dogsHit.Contains(dog))
            return;

        dogsHit.Add(dog);

        dog.TakeDamage(damage);
        
        if (!pierce)
            Destroy(gameObject);
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
