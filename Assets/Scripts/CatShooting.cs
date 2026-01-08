using UnityEngine;

public class CatShooting : MonoBehaviour
{
    [SerializeField] private CatData catData;

    [Header("Lane detection (no index)")]
    [SerializeField] private LayerMask dogLayer;    
    [SerializeField] private float yTolerance = 0.25f;
    [SerializeField] private float scanRange = 50f;

    [Header("Fire point (optional)")]
    [SerializeField] private Transform firePoint;

    private float nextShootTime;
    private int shotsFired = 0;

    private void Awake()
    {
        if (firePoint == null) firePoint = transform;
    }

    public void Initialize(CatData data)
    {
        catData = data;
    }

    private void Update()
    {
        if (catData == null) return;
        if (catData.projectilePrefab == null) return;
        
        if (GameManager.Instance != null && GameManager.Instance.currentGameState != GameState.Playing)
            return;

        if (Time.time < nextShootTime) return;
        if (!HasDogInMyLane()) return;

        Shoot();
        nextShootTime = Time.time + Mathf.Max(0.01f, catData.cooldown);
    }

    private bool HasDogInMyLane()
    {
        Vector2 origin = firePoint.position;
        
        Vector2 center = origin + Vector2.right * (scanRange * 0.5f);
        Vector2 size = new Vector2(scanRange, yTolerance * 2f);

        Collider2D[] hits = Physics2D.OverlapBoxAll(center, size, 0f, dogLayer);
        if (hits == null || hits.Length == 0) return false;

        float myY = origin.y;

        for (int i = 0; i < hits.Length; i++)
        {
            var c = hits[i];
            if (c == null) continue;

            float dogY = c.bounds.center.y;

            if (Mathf.Abs(dogY - myY) <= yTolerance && c.bounds.center.x > origin.x)
                return true;
        }

        return false;
    }

    private int normalShotsSinceSpecial = 0;
    private void Shoot()
    {bool fireSpecial =
            catData.specialShotCount > 0 &&
            catData.specialShotPrefab != null &&
            normalShotsSinceSpecial >= catData.specialShotCount;

        if (fireSpecial)
        {
            SpawnProjectile(catData.specialShotPrefab, catData.specialShotSpeed, catData.projectileDamage);
            normalShotsSinceSpecial = 0;
        }
        else
        {
            SpawnProjectile(catData.projectilePrefab, catData.projectileSpeed, catData.damage);
            normalShotsSinceSpecial++;
        }
        
    }

    private void SpawnProjectile(GameObject prefab, float speed, float damage)
    {
        GameObject go = Instantiate(prefab, firePoint.position, Quaternion.identity);
        
        Projectile proj = go.GetComponent<Projectile>();
        if (proj != null)
            proj.Init(speed, damage);
        else
            Debug.LogError("Projectile prefab is missing Projectile script.");
    }

    private void OnDrawGizmosSelected()
    {
        if (firePoint == null) return;

        Vector2 origin = firePoint.position;
        Vector2 center = origin + Vector2.right * (scanRange * 0.5f);
        Vector2 size = new Vector2(scanRange, yTolerance * 2f);

        Gizmos.DrawWireCube(center, size);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
}
