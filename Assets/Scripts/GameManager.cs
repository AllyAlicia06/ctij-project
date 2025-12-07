using System;
using UnityEditor;
using UnityEngine;

public enum GameState
{
    Pregame,
    Playing,
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
        SetGameState(GameState.Pregame);
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

        Debug.Log($"GameManager: StartWave({waveIndex}) - {wave.waveName}");

        waveProgressUI?.Setup(wave);
        dogSpawner.StartWave(wave);
    }

    public void OnWaveCompleted()
    {
        Debug.Log("GameManager: Wave COMPLETED!");

        if (currentWaveIndex >= waveSet.waves.Length - 1)
        {
            WinGame();
        }
        else
        {
            StartWave(currentWaveIndex + 1);
        }
    }

    public void LoseGame()
    {
        if (currentGameState == GameState.Lost || currentGameState == GameState.Won) return;

        currentGameState = GameState.Lost;
        Debug.Log("Game Over");
    }

    public void WinGame()
    {
        if (currentGameState == GameState.Lost || currentGameState == GameState.Won) return;

        currentGameState = GameState.Won;
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
