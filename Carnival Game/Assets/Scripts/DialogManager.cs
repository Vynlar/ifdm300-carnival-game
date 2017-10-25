using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager : MonoBehaviour {

    public static DialogManager instance;
	// Use this for initialization
	void Start () {
		if(instance == null)
        {
            instance = this;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
