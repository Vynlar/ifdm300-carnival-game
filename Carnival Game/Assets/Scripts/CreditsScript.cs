using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class CreditsScript : MonoBehaviour {
    public static int movespeed = 1;
    public Vector3 userDirection = Vector3.right;
    public double timer = 0;
    public double endTime = 263.1;

    // Update is called once per frame
    public void Update()
    {
        transform.Translate(userDirection * movespeed * Time.deltaTime);
        if(timer >= endTime)
        {
            SceneManager.LoadScene("MainMenu");
        }
        timer += Time.deltaTime;
    }

}
