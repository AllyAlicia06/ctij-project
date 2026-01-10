using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSceneManager : MonoBehaviour
{
    [SerializeField] private GameObject thankYou;
    [SerializeField] private GameObject ourNames;
    
    private float thankYouDuration = 3f;

    private void Awake()
    {
        Time.timeScale = 1f;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(thankYou != null) thankYou.SetActive(true);
        if(ourNames != null) ourNames.SetActive(false);

        StartCoroutine(SwitchCoroutine());
    }

    private IEnumerator SwitchCoroutine()
    {
        yield return new WaitForSeconds(thankYouDuration);
        
        if(thankYou != null) thankYou.SetActive(false);
        if(ourNames != null) ourNames.SetActive(true);
    }
    
    public void OnMainMenuButtonClicked()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
