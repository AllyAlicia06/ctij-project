using UnityEditor;
using UnityEngine;

public enum GameState
{
    Pregame,
    Playing,
    Won,
    Lost
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {get; private set;}

    [Header("Wave Settings")] 
    public WaveSet waveSet;
    public DogSpawner dogSpawner;
    public WaveProgressUI waveProgressUI;
    
    public GameState currentGameState {get; private set;} =  GameState.Pregame;
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
        
        StartGame();
    }

    public void StartGame()
    {
        Debug.Log("GameManager: StartGame called");
        
        currentGameState = GameState.Pregame;

        if (waveSet == null || waveSet.waves == null || waveSet.waves.Length == 0)
        {
            Debug.LogError("GameManager: WaveSet nu este configurat sau nu are wave-uri.");
            return;
        }
        
        if (dogSpawner == null)
        {
            Debug.LogError("GameManager: DogSpawner nu este setat în Inspector.");
            return;
        }
        
        StartWave(0);
    }

    private void StartWave(int waveIndex)
    {
        currentWaveIndex = waveIndex;
        var wave = waveSet.waves[currentWaveIndex];

        Debug.Log($"GameManager: StartWave({waveIndex}) - {wave.waveName}");
        
        //waveProgressUI?.Setup(wave);
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
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
