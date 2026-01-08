using UnityEngine;
using UnityEngine.UI;

public class PregameCountdownUI : MonoBehaviour
{
    [Header("Timer Settings")] 
    public float duration = 60f;
    
    [Header("UI")]
    public Text countdownText;

    private float remaining;
    private bool initialized;

    private void Awake()
    {
        remaining = duration;
        initialized = true;
        UpdateMenu();
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance == null) return;
        
        if (GameManager.Instance.currentGameState != GameState.Pregame) return;
        
        remaining -= Time.deltaTime;
        if(remaining < 0f) remaining = 0f;

        UpdateMenu();

        if (remaining <= 0f)
        {
            GameManager.Instance.StartGameFromPregame();
        }
    }

    private void UpdateMenu()
    {
        if (countdownText == null) return;
        
        int seconds = Mathf.CeilToInt(remaining);
        
        countdownText.text = seconds.ToString();
    }
}
