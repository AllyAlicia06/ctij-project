using UnityEngine;
using UnityEngine.UI;

public class PregameCountdownUI : MonoBehaviour
{
    [Header("Timer Settings")] 
    public float duration = 60f;
    
    [Header("UI")]
    public Text countdownText;

    private float remaining;
    private bool running = false;

    private void OnEnable()
    {
        remaining = duration;
        running = true;
        UpdateMenu();
    }

    private void OnDisable()
    {
        running = false;
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!running) return;
        if (GameManager.Instance == null) return;

        if (GameManager.Instance.currentGameState != GameState.Pregame)
        {
            running = false;
            return;
        }
        
        remaining -= Time.deltaTime;
        if(remaining < 0f) remaining = 0f;

        UpdateMenu();

        if (remaining <= 0f)
        {
            running = false;
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
