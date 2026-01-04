using UnityEngine;

public class Tile : MonoBehaviour
{
    private GameObject placed;
    private Collider2D collider;
    public bool isOccupied => placed != null;

    void Awake()
    {
        collider = GetComponent<Collider2D>();
        if(collider == null) 
            collider = GetComponentInChildren<Collider2D>();
    }
    
    public bool TryPlace(GameObject catPrefab, out GameObject goPlaced)
    {
        goPlaced = null;

        if (catPrefab == null) return false;
        
        if (isOccupied) return false;
        
        Vector3 pos = collider != null ? (Vector3)collider.bounds.center : transform.position;
        pos.z = -1f;
        
        goPlaced = Instantiate(catPrefab, pos, Quaternion.identity);
        goPlaced.transform.localScale =catPrefab.transform.localScale;
        placed = goPlaced;
        return true;
    }

    public void ClearPlaced()
    {
        placed = null;
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
