using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour {
    public bool isStart;
    public bool isQuit;
    private void OnMouseUp()
    {
        if (isStart)
        {
            SceneManager.LoadScene("CutScene");
        }
        if (isQuit)
        {
            Application.Quit();
        }
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
