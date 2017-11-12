using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Teleport : MonoBehaviour {

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {

	}

    // Teleports the object to this transform's position
    public void TeleportHere(GameObject obj)
    {
        obj.transform.position = this.transform.position;
    }

}
