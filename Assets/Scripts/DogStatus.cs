using System.Collections;
using UnityEngine;

public class DogStatus : MonoBehaviour
{
    private Dog dog;
    private Coroutine slowCo;
    private Coroutine stunCo;

    private float speedMultiplier = 1f;
    private bool stunned = false;

    public bool IsStunned => stunned;
    public float SpeedMultiplier => speedMultiplier;

    private void Awake()
    {
        dog = GetComponent<Dog>();
    }

    public void ApplySlow(float multiplier, float duration)
    {
        if (slowCo != null) StopCoroutine(slowCo);
        slowCo = StartCoroutine(SlowRoutine(multiplier, duration));
    }

    private IEnumerator SlowRoutine(float multiplier, float duration)
    {
        speedMultiplier = Mathf.Clamp(multiplier, 0.05f, 1f);
        yield return new WaitForSeconds(duration);
        speedMultiplier = 1f;
        slowCo = null;
    }

    public void ApplyStun(float duration)
    {
        if (stunCo != null) StopCoroutine(stunCo);
        stunCo = StartCoroutine(StunRoutine(duration));
    }

    private IEnumerator StunRoutine(float duration)
    {
        stunned = true;
        yield return new WaitForSeconds(duration);
        stunned = false;
        stunCo = null;
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
