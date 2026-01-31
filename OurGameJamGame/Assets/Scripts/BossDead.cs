using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDead : MonoBehaviour
{
    public GameObject WinUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnDestroy() {
        WinUI.SetActive(true);//
    }
}
