using UnityEngine;
using UnityEngine.UI;

public class WaveProgressUI : MonoBehaviour
{
    public static WaveProgressUI Instance {get; private set;}

    [Header("UI References")]
    public Slider waveSlider;
    public Text waveNameLabel;
    public Text waveCountLabel;

    private int totalEnemies;

    private void Awake()
    {
        Instance = this;
    }

    public void Setup(WaveConfig wave)
    {
        totalEnemies = 0;
        foreach (var entry in wave.waveEntries)
            totalEnemies += entry.spawnCount;

        if (waveSlider != null)
        {
            waveSlider.minValue = 0;
            waveSlider.maxValue = totalEnemies;
            waveSlider.value = 0;
        }
        
        if(waveSlider == null)
            waveNameLabel.text = wave.waveName;

        if (waveCountLabel != null)
            waveCountLabel.text = $"0 / {totalEnemies}";
    }

    public void OnDogSpawned(int spawned, int total)
    {
        if(waveSlider != null)
            waveSlider.value = spawned;
        
        if (waveCountLabel != null)
            waveCountLabel.text = $"{spawned} / {totalEnemies}";
    }

    public void OnDogDied(int died, int total)
    {
        if (waveSlider != null)
            waveSlider.value = died;
        
        if (waveCountLabel != null)
            waveCountLabel.text = $"{died} / {totalEnemies}";
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
