using Unity.VisualScripting;
using UnityEngine;

public class FireBurstOnHit : MonoBehaviour
{
    [SerializeField] private LayerMask dogLayer;
    [SerializeField] private float radius = 1.5f;
    [SerializeField] private float yTolerance = 0.25f;
    [SerializeField] private float aoeDamage = 15f;

    private bool exploded = false;
    
    [SerializeField] private ElementType elementType = ElementType.Fire;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (exploded) return;

        Dog first = other.GetComponent<Dog>() ?? other.GetComponentInParent<Dog>();
        if (first == null) return;

        exploded = true;

        Vector2 center = transform.position;
        Vector2 size = new Vector2(radius * 2f, yTolerance * 2f);
        
        Collider2D[] hits = Physics2D.OverlapBoxAll(center, size, 0f, dogLayer);
        foreach (var h in hits)
        {
            Dog d = h.GetComponent<Dog>() ?? h.GetComponentInParent<Dog>();
            if (d == null) continue;

            d.TakeDamage(aoeDamage, elementType);
        }

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
