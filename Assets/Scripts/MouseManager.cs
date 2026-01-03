using System;
using UnityEngine;
//used for the mice to be able to move
public class MouseManager : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float laneEndX = -8f;
    [SerializeField] private int coinValue = 5;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        CheckEndOfLane();
    }

    private void Move()
    {
        Vector3 delta = Vector3.left * moveSpeed * Time.deltaTime; //face soarecele sa se miste in directie
        transform.Translate(delta, Space.World);
    }

    private void CheckEndOfLane()
    {
        if (transform.position.x <= laneEndX)
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
