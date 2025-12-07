using System.Collections;
using UnityEngine;

public class HideAfterDelay : MonoBehaviour
{
    [SerializeField]
    private float delaySeconds = 5f;

    private Coroutine hideRoutine;

    private void OnEnable()
    {
        hideRoutine = StartCoroutine(HideRoutine());
    }

    private void OnDisable()
    {
        if (hideRoutine != null)
        {
            StopCoroutine(hideRoutine);
            hideRoutine = null;
        }
    }

    private IEnumerator HideRoutine()
    {
        yield return new WaitForSeconds(delaySeconds);
        
        gameObject.SetActive(false);
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
