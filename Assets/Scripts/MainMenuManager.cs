using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [Header("Main Menu Objects")]
    public GameObject mainMenu;
    public GameObject infoMenu;

    private bool isOpen = false;

    public void PlayGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Quit Game");
    }

    public void ToggleInfoMenu()
    {
        isOpen = !isOpen;
        
        mainMenu.SetActive(!isOpen);
        infoMenu.SetActive(isOpen);
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
