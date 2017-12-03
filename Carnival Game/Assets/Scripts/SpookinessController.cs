using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpookinessController : MonoBehaviour {
    private Animator animator;

    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();	
	}

    public void makeSpooky()
    {
        animator.SetBool("isSpooky", true);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
