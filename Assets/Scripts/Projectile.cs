using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float speed;
    private float damage;
    private Rigidbody2D rb;

    [SerializeField] private float lifeTime = 5f; // safety auto-destroy

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
    }

    // Called by the cat right after Instantiate
    public void Init(float projectileSpeed, float projectileDamage)
    {
        speed = projectileSpeed;
        damage = projectileDamage;

        // Move to the right (+X)
        rb.linearVelocity = Vector2.right * speed;

        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Hit dog (Dog component on same object or parent)
        Dog dog = other.GetComponent<Dog>();
        if (dog == null) dog = other.GetComponentInParent<Dog>();
        if (dog == null) return;

       // dog.TakeDamage(damage);
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
