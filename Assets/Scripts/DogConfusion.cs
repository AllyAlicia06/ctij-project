using UnityEngine;

public class DogConfusion : MonoBehaviour
{
    private float yTolerance = 0.25f;

    [Header("Confusion Attack")] 
    [SerializeField] private float hitDamage = 8f;
    [SerializeField] private float hitCooldown = 0.6f;

    private Dog dog;
    private bool confused;
    private float confusedUntil;
    private float nextHitTime;
    
    private float speedMultiplier = 1f;
    public float SpeedMultiplier => speedMultiplier;

    private void Awake()
    {
        dog = GetComponent<Dog>();
        speedMultiplier = 1f;
    }

    public void ApplyConfusion(float duration)
    {
        confused = true;
        confusedUntil = Time.time + duration;
        speedMultiplier = -1f;
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!confused) return;

        if (Time.time >= confusedUntil)
        {
            confused = false;
            speedMultiplier = 1f;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!confused) return;
        if (Time.time < nextHitTime) return;
        
        Dog otherDog = other.gameObject.GetComponent<Dog>() ?? other.GetComponentInParent<Dog>();
        if (otherDog == null || otherDog == dog) return;

        if (Mathf.Abs(otherDog.transform.position.y - transform.position.y) > yTolerance) return;
        
        nextHitTime = Time.time + hitCooldown;
        otherDog.TakeDamage(hitDamage);
    }
}
