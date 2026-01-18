using System.Collections;
using UnityEngine;

public class StickyBomb : MonoBehaviour
{
    [SerializeField] private LayerMask dogLayer;
    [SerializeField] private float fuse = 2f;

    [Header("Explosion effects")]
    [SerializeField] private float stunDuration = 2f;
    [SerializeField] private float slowRadius = 1.5f;
    [SerializeField] private float slowMultiplier = 0.5f;
    [SerializeField] private float slowDuration = 2f;

    private Rigidbody2D rb;
    private bool stuck = false;
    private Dog stuckDog;
    
    [SerializeField] private ElementType elementType = ElementType.Earth;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (stuck) return;

        Dog dog = other.GetComponent<Dog>() ?? other.GetComponentInParent<Dog>();
        if (dog == null) return;
        
        if(dog.ElementType == elementType) return;

        stuck = true;
        stuckDog = dog;

        if (rb != null) rb.linearVelocity = Vector2.zero;
        transform.SetParent(dog.transform, true);

        StartCoroutine(FuseRoutine());
    }

    private IEnumerator FuseRoutine()
    {
        yield return new WaitForSeconds(fuse);

        Vector2 pos = transform.position;

        // stun the attached dog
        if (stuckDog != null)
        {
            var st = stuckDog.GetComponent<DogStatus>();
            if (st != null) st.ApplyStun(stunDuration);
        }

        // slow nearby dogs
        Collider2D[] hits = Physics2D.OverlapCircleAll(pos, slowRadius, dogLayer);
        foreach (var h in hits)
        {
            Dog d = h.GetComponent<Dog>() ?? h.GetComponentInParent<Dog>();
            if (d == null) continue;

            if (d.ElementType == elementType) continue;
            
            var st = d.GetComponent<DogStatus>();
            if (st != null) st.ApplySlow(slowMultiplier, slowDuration);
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
