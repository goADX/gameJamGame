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

    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("FirstLevel");
    }


    public void quit()
    {
        Application.Quit();
    }
}
