using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class CatPlacement : MonoBehaviour
{
    [SerializeField] private Camera camera;
    [SerializeField] private LayerMask tileLayer;
   
    private CatData selectedCat;
    
    [Header("Block placement if clicking these")]
    [SerializeField] private LayerMask blockPlacementLayer;
    
    private void Awake()
    {
        if(camera == null)
            camera = Camera.main;
    }

    public void SelectCat(CatData catData)
    {
        if (catData == null || catData.catPrefab == null)
        {
            Debug.LogError("SelectCat: CatData or catPrefab is null.");
            selectedCat = null;
            return;
        }

        selectedCat = catData;
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Mouse.current == null) return;

        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            TryRemoveCatUnderMouse();
            return;
        }
        
        if (selectedCat==null) return;
        if (!Mouse.current.leftButton.wasPressedThisFrame) return;

        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject()) return;
        
        Vector2 screenPos = Mouse.current.position.ReadValue();
        
        float depth = camera.orthographic ? 0f : Mathf.Abs(camera.transform.position.z);
        Vector3 mousePos = camera.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, depth));
        mousePos.z = 0f;
        
        if (Physics2D.OverlapPoint(mousePos, blockPlacementLayer) != null) return;
        
        Collider2D col = Physics2D.OverlapPoint(mousePos, tileLayer);

        if (col == null)
        {
            Debug.Log("No tile collider found");
            return;
        }
        
        Tile tile = col.GetComponent<Tile>();

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
        
        if (GameManager.Instance == null)
        {
            Debug.LogError("No GameManager instance found");
            return;
        }

        int cost = Mathf.Max(0, selectedCat.cost);
        
        if (!GameManager.Instance.SpentCoins(cost))
        {
            Debug.Log("Not enough coins to buy this cat");
            selectedCat = null;
            return;
        }
        
        if (tile.TryPlace(selectedCat.catPrefab, out GameObject placed))
        {
            if (placed != null)
                placed.transform.localScale = selectedCat.catPrefab.transform.localScale;

            selectedCat = null;
        }

        else
        {
            GameManager.Instance.AddCoins(cost);
        }

    }

    private void TryRemoveCatUnderMouse()
    {
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject()) return;
        
        Vector2 screenPos = Mouse.current.position.ReadValue();
        float depth = camera.orthographic ? 0f : Mathf.Abs(camera.transform.position.z);
        Vector3 mousePos = camera.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, depth));
        mousePos.z = 0f;
        
        Collider2D col = Physics2D.OverlapPoint(mousePos, tileLayer);
        if (col == null) return;
        
        Tile tile = col.GetComponent<Tile>();
        if (tile == null) return;

        if (!tile.isOccupied) return;
        
        tile.ClearPlaced();
    }
}
