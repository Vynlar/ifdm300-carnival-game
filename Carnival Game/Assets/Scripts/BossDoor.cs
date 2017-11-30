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
		
		//Get rid of the glow around the door when the final puzzle is solved.
		if(lockStatus == 3)
		{
			GameObject DoorGlow = GameObject.Find("BossDoorGlow");
			EnableDisableScript eds = (EnableDisableScript) DoorGlow.GetComponent("EnableDisableScript");
			if(eds != null) eds.Disable();
		}
    }
}
