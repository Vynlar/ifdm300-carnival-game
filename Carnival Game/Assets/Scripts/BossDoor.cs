using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDoor : MonoBehaviour {

    private int lockStatus = 0;

    public Dialogue dialogue;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void incrementStatus()
    {
        lockStatus++;
        dialogue.SetDialogState("stage" + lockStatus);
    }
}
