using System.Collections;
using UnityEngine;

public class TestDog : MonoBehaviour
{
    private DogSpawner spawner;

    private void Start()
    {
        spawner = FindObjectOfType<DogSpawner>();
        StartCoroutine(DieAfterSeconds(3f));
    }

    private IEnumerator DieAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        if (spawner != null)
        {
            spawner.OnDogDied();
            Debug.Log("TestDog: I died and notified spawner.");
        }

        Destroy(gameObject);
    }
}