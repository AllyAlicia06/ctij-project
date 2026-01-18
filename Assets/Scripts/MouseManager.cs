using System;
using UnityEngine;
//used for the mice to be able to move
public class MouseManager : MonoBehaviour
{   
    [Header("FromRightToLeft")]
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float laneEndX = -8f;
    
    [SerializeField] private int coinValue = 5;

    [Header("FromTopToBottom")]
    [SerializeField] private float laneEndY = -10f;
    [SerializeField] private float fallSpeed = 2f;
    
    private bool isStorming = false;
    
    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    private AudioClip clip;

    private void Awake()
    {
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
    }

    private void PlayCollectSound()
    {
        if (clip == null && audioSource != null)
            clip = audioSource.clip;

        if (clip == null) return;
        
        var go = new GameObject("CollectSound");
        var src = go.AddComponent<AudioSource>();
        src.spatialBlend = 0f;
        src.volume = 0.5f;
        
        src.PlayOneShot(clip);
        Destroy(go, clip.length + 0.1f);
    }
    
    public void SetStorm(bool value)
    {
        isStorming = value;
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        if (isStorming)
        {
            transform.Translate(Vector3.down * fallSpeed * Time.deltaTime, Space.World);
            if (transform.position.y <= laneEndY) Destroy(gameObject);
        }
        else
        {
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime, Space.World);
            if (transform.position.x <= laneEndX) Destroy(gameObject);
        }
    }

    private void OnMouseDown()
    {
        if (GameManager.Instance == null) return;

        var s = GameManager.Instance.currentGameState;
        if (s == GameState.Paused || s == GameState.Won ||  s == GameState.Lost) return;
        
        //am adaugat liniile de mai sus ca sa nu mai putem colecta soriceii cand jocul e paused sau won/lost
        
        Collect();
    }

    private void Collect()
    {
        PlayCollectSound();
        GameManager.Instance?.AddCoins(coinValue);
        Destroy(gameObject);
    }
}
