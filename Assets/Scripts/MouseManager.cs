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

    private void MoveLeft()
    {
        Vector3 delta = Vector3.left * moveSpeed * Time.deltaTime; //face soarecele sa se miste in directie
        transform.Translate(delta, Space.World);
    }

    private void MoveDown()
    {
        Vector3 delta = Vector3.down * fallSpeed * Time.deltaTime;
        transform.Translate(delta, Space.World);
    }

    private void CheckEndOfLane()
    {
        if (transform.position.x <= laneEndX)
        {
            Destroy(gameObject);
        }
    }

    private void CheckStormEnd()
    {
        if (transform.position.x <= laneEndY)
        {
            Destroy(gameObject);
        }
    }

    private void OnMouseDown()
    {
        Collect();
    }

    private void Collect()
    {
        GameManager.Instance?.AddCoins(coinValue);
        Destroy(gameObject);
    }
}
