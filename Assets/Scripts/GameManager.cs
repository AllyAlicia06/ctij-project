using System;
using UnityEngine;

public enum GameState
{
    Pregame,
    Playing,
    Storm,
    Paused,
    Won,
    Lost
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Wave Settings")] public WaveSet waveSet;
    public DogSpawner dogSpawner;
    public WaveProgressUI waveProgressUI;

    public GameState currentGameState { get; private set; } = GameState.Pregame;

    public event Action<GameState> OnGameStateChanged;

    private int currentWaveIndex = 0;

    [Header("Coins")] [SerializeField] private int defaultCoins = 0;
    public int Coins { get; private set;}
    public event Action<int> OnCoinsChanged; //creeaza o actiune pe care o apelez cand se intampla ceva (cand apasam pe soricei)
    

    private void Awake()
    {
        Debug.Log("GameManager: Awake");

        if (Instance != null && Instance != this)
        {
            Debug.Log("GameManager: duplicate, destroying");

            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("GameManager: Start");

        //StartGame();
        Time.timeScale = 1f;
        Coins = defaultCoins;
        OnCoinsChanged?.Invoke(Coins);
        SetGameState(GameState.Pregame);
    }

    public void AddCoins(int amount)
    {
        if (amount <= 0)
            return;
        Coins += amount;
        OnCoinsChanged?.Invoke(Coins);
    }

    public bool SpentCoins(int amount)
    {
        if (amount <= 0)
            return true;
        if (Coins < amount)
            return false;
        Coins -= amount;
        OnCoinsChanged?.Invoke(Coins);
        return true;
    }
    
    private void SetGameState(GameState newState)
    {
        if (currentGameState == newState) return;

        currentGameState = newState;
        OnGameStateChanged?.Invoke(currentGameState);
    }

    /*public void StartGame()
    {
        Debug.Log("GameManager: StartGame called");

        currentGameState = GameState.Pregame;

        if (waveSet == null || waveSet.waves == null || waveSet.waves.Length == 0)
        {
            Debug.LogError("GameManager: WaveSet is not configured in the Inspector");
            return;
        }

        if (dogSpawner == null)
        {
            Debug.LogError("GameManager: DogSpawner is not configured in the Inspector.");
            return;
        }

        SetGameState(GameState.Playing);
        StartWave(0);
    }*/

    public void StartGameFromPregame()
    {
        if (currentGameState != GameState.Pregame) return;
        
        if (waveSet == null || waveSet.waves == null || waveSet.waves.Length == 0)
        {
            Debug.LogError("GameManager: WaveSet is not configured in the Inspector");
            return;
        }

        if (dogSpawner == null)
        {
            Debug.LogError("GameManager: DogSpawner is not configured in the Inspector.");
            return;
        }

        SetGameState(GameState.Playing);
        StartWave(0);
    }

    private void StartWave(int waveIndex)
    {
        currentWaveIndex = waveIndex;
        var wave = waveSet.waves[currentWaveIndex];

        Debug.Log($"GameManager: StartWave({waveIndex})");

        waveProgressUI?.Setup(wave);
        dogSpawner.StartWave(wave);
    }

    public void OnWaveCompleted()
    {
        Debug.Log("GameManager: Wave COMPLETED!");

        int lastWaveIndex = waveSet.waves.Length - 1;

        if (currentWaveIndex == lastWaveIndex - 1)
        {
            StartStormPhase();
            return;
        }

        if (currentWaveIndex >= waveSet.waves.Length - 1)
        {
            WinGame();
            return;
        }
        StartWave(currentWaveIndex + 1);
    }

    private void StartStormPhase()
    {
        Time.timeScale = 1f;
        SetGameState(GameState.Storm);
    }

    public void EndStormAndStartLastWave()
    {
        int lastWaveIndex = waveSet.waves.Length - 1;
        SetGameState(GameState.Playing);
        StartWave(lastWaveIndex);
    }
    
    public void DogReachedEnd()
    {
        if (currentGameState == GameState.Lost || currentGameState == GameState.Won)
            return;
        
        Debug.Log("GameManager: DogReachedEnd, Game Over");
        LoseGame();
    }
    
    public void LoseGame()
    {
        if (currentGameState == GameState.Lost || currentGameState == GameState.Won) return;

        Time.timeScale = 1f;
        SetGameState(GameState.Lost);
        
        //currentGameState = GameState.Lost;
        Debug.Log("Game Over");
    }

    public void WinGame()
    {
        if (currentGameState == GameState.Lost || currentGameState == GameState.Won) return;

        Time.timeScale = 1f;
        SetGameState(GameState.Won);
        
        //currentGameState = GameState.Won;
        Debug.Log("Game Won");
    }

    public void PauseGame()
    {
        if(currentGameState != GameState.Playing) return;
        SetGameState(GameState.Paused);
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        if (currentGameState != GameState.Paused) return;
        SetGameState(GameState.Playing);
        Time.timeScale = 1;
    }

// Update is called once per frame
    void Update()
    {
        
    }
}
