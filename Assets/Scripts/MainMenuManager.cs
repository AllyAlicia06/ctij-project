using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [Header("Main Menu Objects")]
    public GameObject mainMenu;
    public GameObject infoMenu;

    private bool isOpen = false;
    
    [Header("Info Pages")]
    public GameObject infoPage1;
    public GameObject infoPage2;

    private int pageIndex = 0;

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

        if (isOpen)
        {
            ShowPage(0);
        }
    }

    public void NextPage()
    {
        Debug.Log("Next Page");
        ShowPage(1);
    }

    public void PreviousPage()
    {
        ShowPage(0);
    }

    private void ShowPage(int page)
    {
        pageIndex = Mathf.Clamp(pageIndex, 0, 1);
        
        if(infoPage1 != null) infoPage1.SetActive(page == 0);
        if(infoPage2 != null) infoPage2.SetActive(page == 1);
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
