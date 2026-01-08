using UnityEngine;

public class FreezeOnHit : MonoBehaviour
{
    [SerializeField] private float slowMultiplier=0.5f;
    [SerializeField] private float duration=2.0f;
    
    [SerializeField] private ElementType elementType = ElementType.Frost;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Dog dog = other.GetComponent<Dog>()?? other.GetComponentInChildren<Dog>();
        if (dog==null) return;

        if (dog.ElementType == elementType) return;
        
        var status = dog.GetComponent<DogStatus>();
        if(status!=null) status.ApplySlow(slowMultiplier, duration);
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
