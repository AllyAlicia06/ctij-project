using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour
{
    [Header("Menus")]
    public GameObject pregameMenu;
    public GameObject hudMenu;
    public GameObject pauseMenu;
    public GameObject gameWonMenu;
    public GameObject gameOverMenu;

    [Header("Coins")] [SerializeField] private Text coinsText; 
    
    [Header("StormUI")] public GameObject stormUI;
    
    [SerializeField] private StormPhaseController stormPhaseController;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnGameStateChanged += this.OnGameStateChanged;

            GameManager.Instance.OnCoinsChanged += this.OnCoinsChanged;
            OnCoinsChanged(GameManager.Instance.Coins);
            
            OnGameStateChanged(GameManager.Instance.currentGameState);
        }
    }

    private void OnDestroy()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnGameStateChanged -= this.OnGameStateChanged;
            GameManager.Instance.OnCoinsChanged -= this.OnCoinsChanged;
        }
    }

    private void OnCoinsChanged(int coins)
    {
        if(coinsText != null) 
            coinsText.text = coins.ToString();
    }

    private void OnGameStateChanged(GameState state)
    {
        Debug.Log(state);
        
        pauseMenu?.SetActive(false);
        gameWonMenu?.SetActive(false);
        gameOverMenu?.SetActive(false);
        stormUI?.SetActive(false);

        switch (state)
        {
            case GameState.Pregame:
                if(hudMenu != null) hudMenu.SetActive(true);
                if(pregameMenu != null) pregameMenu.SetActive(true);
                break;
            
            case GameState.Playing:
                if(hudMenu != null) hudMenu.SetActive(true);
                if(pregameMenu != null) pregameMenu.SetActive(false);
                break;
            
            case GameState.Paused:
                if(pauseMenu != null) pauseMenu.SetActive(true);
                if (GameManager.Instance != null && GameManager.Instance.PreviousGameState == GameState.Pregame)
                {
                    if(pregameMenu != null) pregameMenu.SetActive(true);
                    if(hudMenu != null) hudMenu.SetActive(false);
                }
                else
                {
                    if(pregameMenu != null) pregameMenu.SetActive(false);
                    if(hudMenu != null) hudMenu.SetActive(false);
                }
                break;
            
            case GameState.Won:
                if(hudMenu != null) hudMenu.SetActive(false);
                if(gameWonMenu != null) gameWonMenu.SetActive(true);
                break;
            
            case GameState.Lost:
                if(hudMenu != null) hudMenu.SetActive(false);
                if(gameOverMenu != null) gameOverMenu.SetActive(true);
                break;
            
            case GameState.Storm:
                if(hudMenu != null) hudMenu.SetActive(false);
                if(stormUI != null) stormUI.SetActive(true);
                break;
        }
    }

    public void OnPauseButtonClicked()
    {
        GameManager.Instance?.PauseGame();
    }

    public void OnResumeButtonClicked()
    {
        GameManager.Instance?.ResumeGame();
    }

    public void OnMainMenuButtonClicked()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void OnRetryButtonClicked()
    {
        SceneManager.LoadScene("SampleScene");   
    }

    public void OnNextLevelButtonClicked()
    {
        SceneManager.LoadScene("SampleScene");
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
