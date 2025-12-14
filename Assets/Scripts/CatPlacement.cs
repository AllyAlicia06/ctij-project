using UnityEngine;
using UnityEngine.InputSystem;

public class CatPlacement : MonoBehaviour
{
    [SerializeField] private Camera camera;
    [SerializeField] private LayerMask tileLayer;

    private GameObject selectedCatPrefab;

    private void Awake()
    {
        if(camera == null)
            camera = Camera.main;
    }

    public void SelectCat(GameObject catPrefab)
    {
        selectedCatPrefab = catPrefab;
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (selectedCatPrefab == null) return;

        //if (!Input.GetMouseButtonDown(0)) return;
        if (!Mouse.current.leftButton.wasPressedThisFrame) return;
        
        //Vector3 mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 screenPos = Mouse.current.position.ReadValue();
        
        float depth = camera.orthographic ? 0f : Mathf.Abs(camera.transform.position.z);
        Vector3 mousePos = camera.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, depth));
        mousePos.z = 0f;
        
        Collider2D col = Physics2D.OverlapPoint(mousePos, tileLayer);

        if (col == null)
        {
            Debug.Log("No tile collider found");
            return;
        }
        
        Tile tile = col.GetComponent<Tile>();
        //if (tile == null || tile.isOccupied) return;

        if (tile == null)
        {
            Debug.Log("No tile found");
            return;
        }

        if (tile.isOccupied)
        {
            Debug.Log("Tile is occupied");
            return;
        }
        
        if (tile.TryPlace(selectedCatPrefab, out _))
        {
            selectedCatPrefab = null;
        }

    }
}
