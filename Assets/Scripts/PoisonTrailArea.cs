using UnityEngine;

public class PoisonTrailArea : MonoBehaviour
{
    [SerializeField] private float lifeTime = 2.5f;

    [Header("Damage over time while inside")] 
    [SerializeField] private float dps = 6f;
    [SerializeField] private float tick = 0.25f;

    private float nextTickTime;
    
    [SerializeField] private ElementType elementType = ElementType.Poison;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, lifeTime);
        nextTickTime = Time.time;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (Time.time < nextTickTime) return;
        nextTickTime = Time.time + tick;

        Dog dog = other.GetComponent<Dog>() ?? other.GetComponentInParent<Dog>();
        if (dog == null) return;

        dog.TakeDamage(dps * tick, elementType);
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
