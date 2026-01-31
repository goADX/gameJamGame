using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILogicManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //
    public void GoToMainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("PlayScene");
    }


    public void quit()
    {
        Application.Quit();
    }
}
