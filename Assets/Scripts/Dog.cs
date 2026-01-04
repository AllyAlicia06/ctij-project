using Unity.VisualScripting;
using UnityEngine;

public class Dog : MonoBehaviour
{
    [Header("DogData")]
    [SerializeField] DogData dogData;
    
    [Header("X End Position")] 
    [SerializeField]private float laneEndX = -10f;

    private float moveDirection = -1f;
    private float currentHealth;
    private float currentMoveSpeed;
    private SpriteRenderer spriteRenderer;
    private DogSpawner spawner;
    
    public DogData Data => dogData;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (dogData != null)
        {
            Initialize(dogData, moveDirection);
        }
    }

    public void Initialize(DogData data, float direction)
    {
        dogData = data;
        moveDirection = Mathf.Sign(direction);
        
        currentHealth = dogData.maxHealth;
        currentMoveSpeed = Mathf.Abs(dogData.moveSpeed) * moveDirection;

        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        if (spriteRenderer != null)
        {
            spriteRenderer.sprite = dogData.sprite;
        }
    }

    public void SetSpawner(DogSpawner dogSpawner)
    {
        spawner = dogSpawner;
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        CheckEndOfLane();
    }

    public void Move()
    {
        Vector3 delta = Vector3.right * currentMoveSpeed * Time.deltaTime;
        transform.Translate(delta, Space.World);
    }

    public void CheckEndOfLane()
    {
        bool reached = (moveDirection < 0f && transform.position.x <= laneEndX) ||  (moveDirection > 0f && transform.position.x >= laneEndX);

        if (!reached) return;

        ReachEndOfLane();
    }

    public void ReachEndOfLane()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.DogReachedEnd();
        }
        
        Die();
    }

    private void Die()
    {
        spawner?.OnDogDied();
        Destroy(gameObject);
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Die();
        }
    }
}
