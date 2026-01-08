using UnityEngine;

public class ConfusionOnHit : MonoBehaviour
{
    [SerializeField] private float duration = 2f;
    
    [SerializeField] private ElementType elementType = ElementType.Dark;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Dog dog = other.GetComponent<Dog>() ?? other.GetComponentInParent<Dog>();
        if (dog == null) return;

        if(dog.ElementType == elementType) return;
        
        DogConfusion confusion =
            other.GetComponent<DogConfusion>() ?? other.GetComponentInParent<DogConfusion>() ??
            dog.GetComponent<DogConfusion>() ?? dog.GetComponentInParent<DogConfusion>();
        
        if (confusion != null)
            confusion.ApplyConfusion(duration);
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
