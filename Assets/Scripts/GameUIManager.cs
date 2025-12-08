using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUIManager : MonoBehaviour
{
    [Header("Menus")]
    public GameObject pregameMenu;
    public GameObject hudMenu;
    public GameObject pauseMenu;
    public GameObject gameWonMenu;
    public GameObject gameOverMenu;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnGameStateChanged += this.OnGameStateChanged;
            
            OnGameStateChanged(GameManager.Instance.currentGameState);
        }
    }

    private void OnDestroy()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnGameStateChanged -= this.OnGameStateChanged;
        }
    }

    private void OnGameStateChanged(GameState state)
    {
        Debug.Log(state);
        
        if(pregameMenu != null) pregameMenu.SetActive(false);
        if(hudMenu != null) hudMenu.SetActive(true);
        if(pauseMenu != null) pauseMenu.SetActive(false);
        if(gameOverMenu != null) gameOverMenu.SetActive(false);
        if(gameWonMenu != null) gameWonMenu.SetActive(false);

        switch (state)
        {
            case GameState.Pregame:
                if(pregameMenu != null) pregameMenu.SetActive(true);
                break;
            
            case GameState.Playing:
                if(hudMenu != null) hudMenu.SetActive(true);
                break;
            
            case GameState.Paused:
                if(hudMenu != null) hudMenu.SetActive(false); //aici vedem daca true sau false
                if(pauseMenu != null) pauseMenu.SetActive(true);
                break;
            
            case GameState.Won:
                if(hudMenu != null) hudMenu.SetActive(false);
                if(gameWonMenu != null) gameWonMenu.SetActive(true);
                break;
            
            case GameState.Lost:
                if(hudMenu != null) hudMenu.SetActive(false);
                if(gameOverMenu != null) gameOverMenu.SetActive(true);
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
