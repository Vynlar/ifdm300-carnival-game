using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Toggles the GameObject's collider and renderer components.
public class EnableDisableScript : MonoBehaviour {

	public bool disabled;
	private bool changed; 	//Has 'disabled' been changed since SetActive has been updated?
	private Renderer rend;
	private BoxCollider2D co2d;

	// Use this for initialization
	void Start () 
	{
		rend = GetComponent<Renderer>();
		co2d = GetComponent<BoxCollider2D>();
		changed = false;
		if (disabled)
			Disable ();
		else
			Enable ();
	}
	
	// Update is called once per frame
	//If changed, toDisable.SetActive() set accordingly.
	void Update () 
	{
		if (changed) 
		{
			if (disabled) 
			{
				rend.enabled = (false);
				co2d.enabled = (false);
			} 
			else 
			{
				rend.enabled = (true);
				co2d.enabled = (true);
			}

			changed = false;
		}
	}

	public void Enable()
	{
		disabled = false;
		changed = true;
	}
		
	public void Disable()
	{
		disabled = true;
		changed = true;
	}
}
