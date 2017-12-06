﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class CutScene : MonoBehaviour {
    public float endTime = 12.0f;
    float timer = 0.0f;
    public string scene;
    void Start()
    {
        
    }

    void Update()
    {
        if(timer >= endTime)
        {
            SceneManager.LoadScene(scene);
        }
        timer += Time.deltaTime;
    }

}
